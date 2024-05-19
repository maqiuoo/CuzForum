
using MyForum.DAL;
using MyForum.DAL.Interface;
using MyForum.Model;
using System.Diagnostics;

namespace MyForum.Api.Py
{
    public class TimedHostedService : IHostedService, IDisposable
    {
        private Timer _timer;
        private readonly IConfiguration configuration;
		private readonly IServiceScopeFactory _scopeFactory;

		public TimedHostedService(IConfiguration configuration, IServiceScopeFactory scopeFactory)
		{
			this.configuration = configuration;
			_scopeFactory = scopeFactory;
		}

		public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromHours(double.Parse(configuration.GetSection("time").Value!)));
            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            // 做一些周期性工作
            ExecutePy();

		}

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
        public async void ExecutePy()
        {
            string pythonInterpreter = configuration.GetSection("pythonInterpreter").Value;

			// 设置Python脚本的路径
			string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
			// 创建文件的绝对路径
			string absolutePath = Path.Combine(baseDirectory, configuration.GetSection("pythonFilePath").Value);



            ProcessStartInfo startInfo = new ProcessStartInfo(pythonInterpreter)
            {
                Arguments = $"\"{absolutePath}\"", // 将脚本路径和参数传给Python解释器
                UseShellExecute = false, // 不使用系统外壳启动进程
                RedirectStandardOutput = true, // 重定向输出，使输出信息可以在C#中获取
                CreateNoWindow = true // 不创建新窗口
            };

            using (Process process = Process.Start(startInfo))
            {
                using (StreamReader reader = process.StandardOutput)
                {
                    string result = reader.ReadToEnd(); // 读取Python脚本的输出信息
                    List<ReptileData> datas = new List<ReptileData>();  
                    string[] strs = result.Split('\n');
					using (var scope = _scopeFactory.CreateScope())
					{
						IReptileDataRepository reptileDataRepository = scope.ServiceProvider.GetService<IReptileDataRepository>()!;
						for (int i = 0; i < strs.Length; i++)
						{
                            if (strs[i].Contains("停止遍历"))
                                break;
							datas.Add(
								new ReptileData { BookTitle = GetBookTitle(strs[i]), Clicks = GetBookClicks(strs[i]) });
						}
                        int count = await reptileDataRepository.AddOrUpdate(datas);
                        await Console.Out.WriteLineAsync(DateTime.Now+$" : {count}条数据更新");
                    }					
                    //Console.WriteLine(result);
                }
            }
        }
        public string GetBookTitle(string str)
        {
			string bookPre = str.Substring(5, str.Length - 5);
			string keyword = " 的";
			int keywordIndex = bookPre.IndexOf(keyword); // 查找关键词的位置
			if (keywordIndex != -1) // 如果找到了关键词
			{
				string bookName = bookPre.Substring(0, keywordIndex); // 提取从开始到关键词开始位置的子字符串	
				return bookName;
            } else
            {
                return "";
            }

		}
		public int GetBookClicks(string str)
		{
            string clicks = str.Split("的点击量为:")[1].Split("\r")[0];
			return int.Parse(clicks);
		}
	}
}
