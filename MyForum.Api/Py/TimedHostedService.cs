
using MyForum.DAL;
using MyForum.DAL.ReptileDataInterface;
using MyForum.DAL.ReptileDataRepository;
using MyForum.Model;
using MyForum.Model.ReptlieModel;
using System.Diagnostics;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
			ExecuteBooksPy();
			ExecuteLecturePy();
			ExecuteSchoolNotificationPy();
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
		#region Books
		public async void ExecuteBooksPy()
		{
			string pythonInterpreter = configuration.GetSection("pythonInterpreter").Value!;

			// 设置Python脚本的路径
			string baseDirectory = AppDomain.CurrentDomain.BaseDirectory + "Py\\";
			// 创建文件的绝对路径
			string absolutePath = Path.Combine(baseDirectory, configuration.GetSection("booksPythonFilename").Value!);

			ProcessStartInfo startInfo = new ProcessStartInfo(pythonInterpreter) {
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
					if (string.IsNullOrEmpty(result))
					{
						await Console.Out.WriteLineAsync("没有读到数据");
						return;
					}
					List<Book> datas = new List<Book>();
					List<Book> updateData = new List<Book>();
					string[] strs = result.Split('\n');
					using (var scope = _scopeFactory.CreateScope())
					{
						IBooksRepository booksRepository = scope.ServiceProvider.GetService<IBooksRepository>()!;
						for (int i = 0; i < strs.Length; i++)
						{
							if (strs[i].Contains("停止遍历"))
								break;
							Book data = new Book { title = GetBookTitle(strs[i]), Clicks = GetBookClicks(strs[i]) };
							if (booksRepository.FindByTitle(data.title) == null)
							{
								datas.Add(data);
							} else
							{
								updateData.Add(data);
							}
						}
						for (int i = 0; i < updateData.Count; i++)
						{
							booksRepository.UpdateEntity(updateData[i]);
						}
						await booksRepository.AddRangeEntityAsync(datas);
						int count = await booksRepository.SaveChangesAsync();
						Console.WriteLine($"Book:{count}条数据更新");
					}

				}
			}
		}
		#endregion

		#region SchoolNotification
		public async void ExecuteSchoolNotificationPy()
		{
			string pythonInterpreter = configuration.GetSection("pythonInterpreter").Value!;

			// 设置Python脚本的路径
			string baseDirectory = AppDomain.CurrentDomain.BaseDirectory + "Py\\";
			// 创建文件的绝对路径
			string absolutePath = Path.Combine(baseDirectory, configuration.GetSection("schoolNotificationPythonFilename").Value!);

			ProcessStartInfo startInfo = new ProcessStartInfo(pythonInterpreter) {
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
					result = result.Replace("'", "\"");
					if (string.IsNullOrEmpty(result))
					{
						await Console.Out.WriteLineAsync("没有读到数据");
						return;
					}
					List<SchoolNotification> datas = new List<SchoolNotification>();

					using (var scope = _scopeFactory.CreateScope())
					{
						ISchoolNotificationRepository schoolNotificationRepository = scope.ServiceProvider.GetService<ISchoolNotificationRepository>()!;

						try
						{
							datas = JsonSerializer.Deserialize<List<SchoolNotification>>(result);
						} catch (Exception ex)
						{
							Console.WriteLine(ex.Message.ToString());
						}
						List<SchoolNotification> updateData = new List<SchoolNotification>();
						for (int i = 0; i < datas.Count; i++)
						{
							if (schoolNotificationRepository.FindByTitle(datas[i].title) != null)
							{
								updateData.Add(datas[i]);
								datas.Remove(datas[i]);
							}
						}
						for (int i = 0; i < updateData.Count; i++)
						{
							schoolNotificationRepository.UpdateEntity(updateData[i]);
						}
						await schoolNotificationRepository.AddRangeEntityAsync(datas);
						int count = await schoolNotificationRepository.SaveChangesAsync();
						Console.WriteLine($"schoolNotification:{count}条数据更新");
					}

				}
			}
		} 
		#endregion

		#region Lecture
		public async void ExecuteLecturePy()
		{
			string pythonInterpreter = configuration.GetSection("pythonInterpreter").Value!;

			// 设置Python脚本的路径
			string baseDirectory = AppDomain.CurrentDomain.BaseDirectory + "Py\\";
			// 创建文件的绝对路径
			string absolutePath = Path.Combine(baseDirectory, configuration.GetSection("lecturePythonFilename").Value!);

			ProcessStartInfo startInfo = new ProcessStartInfo(pythonInterpreter) {
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
					result = result.Replace("'", "\"");
					if (string.IsNullOrEmpty(result))
					{
						await Console.Out.WriteLineAsync("没有读到数据");
						return;
					}
					List<Lecture> datas = new List<Lecture>();

					using (var scope = _scopeFactory.CreateScope())
					{
						ILectureRepository lectureRepository = scope.ServiceProvider.GetService<ILectureRepository>()!;

						try
						{
							datas = JsonSerializer.Deserialize<List<Lecture>>(result);
						} catch (Exception ex)
						{
							Console.WriteLine(ex.Message.ToString());
						}
						List<Lecture> updateData = new List<Lecture>();
						for(int i = 0;i< datas.Count; i++)
						{
							if (lectureRepository.FindByTitle(datas[i].title) != null)
							{
								updateData.Add(datas[i]);
								datas.Remove(datas[i]);
							}
						}
						for(int i=0;i< updateData.Count; i++)
						{
							lectureRepository.UpdateEntity(updateData[i]);
						}

						await lectureRepository.AddRangeEntityAsync(datas);
						int count = await lectureRepository.SaveChangesAsync();
						Console.WriteLine($"Lectrue:{count}条数据更新");
					}

				}
			}
		} 
		#endregion
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

