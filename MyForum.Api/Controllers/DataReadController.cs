using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyForum.Api.Utility;
using MyForum.DAL.Interface;
using MyForum.DAL.ReptileDataInterface;
using MyForum.Model;
using MyForum.Model.DTO;
using MyForum.Model.ReptlieModel;
using System.Drawing;

namespace MyForum.Api.Controllers
{
    [Route("/")]
	[ApiController]
	public class DataReadController : ControllerBase
	{
		private readonly IWebHostEnvironment _environment;
		private readonly ITableReadRepository tableReadRepository;
		private readonly IReptileDataRepository<Book> reptileDataRepository;
		private readonly IMapper mapper;

		public DataReadController(IWebHostEnvironment environment, ITableReadRepository tableReadRepository, IMapper mapper, IReptileDataRepository<Book> reptileDataRepository)
		{
			_environment = environment;
			this.tableReadRepository = tableReadRepository;
			this.mapper = mapper;
			this.reptileDataRepository = reptileDataRepository;
		}
		[HttpPost]
		[Route("uploadFile")]
		public async Task<IActionResult> Upload(IFormFile file)
		{
			if (file == null || file.Length == 0)
				return BadRequest("Upload a file please.");

			// 使用 IWebHostEnvironment 构建文件保存路径，以保证文件可以保存在项目目录内
			var uploadsFolderPath = Path.Combine(_environment.ContentRootPath, "Uploads");
			if (!Directory.Exists(uploadsFolderPath))
				Directory.CreateDirectory(uploadsFolderPath);

			var filePath = Path.Combine(uploadsFolderPath, file.FileName);

			using (var stream = new FileStream(filePath, FileMode.Create))
			{
				await file.CopyToAsync(stream);
			}
			List<EmploymentData> datas = new List<EmploymentData>();
			using (var worksheet = TableHelper.ReadTableData(filePath))
			{
				int rowCount = worksheet.Dimension.Rows; // 获取行数
				int ColCount = worksheet.Dimension.Columns; // 获取列数
				string type = file.FileName.Substring(6, 2);
				int year = int.Parse(file.FileName.Substring(0, 4));
				for (int row = 2; row <= rowCount; row++)
				{
					var temp = new EmploymentData { Type = type, Year = year };
					for (int col = 1; col <= ColCount - 1; col++)
					{
						// 读取每一列的值
						if(col == 1)
						{
							temp.Region = worksheet.Cells[row, col].Value.ToString()!;
						}
						if(col == 2)
						{
							temp.Population = int.Parse(worksheet.Cells[row, 2].Value.ToString() ?? "0");
						}						
					}
					datas.Add(temp);
				}
				if (file.FileName.Contains("全国"))
				{
					string[] provinces = ["河北","山西","辽宁","吉林","黑龙江", "江苏", "浙江", "安徽", "福建", "江西", "山东", "河南", "湖北", "湖南"
					,"广东","海南","四川","贵州","云南","陕西","甘肃","青海","台湾","内蒙古","广西","西藏","宁夏","新疆","北京","天津","重庆","上海"
					,"香港","澳门"];
					Dictionary<string, int> hasProvinces = new Dictionary<string, int>();
					for (int i = 0; i < provinces.Length; i++)
					{
						hasProvinces[provinces[i]] = 0;
					}
					datas.ForEach(data => {
						if (provinces.Contains(data.Region))
						{
							hasProvinces[data.Region] = 1;
						}
					});
					foreach (var item in hasProvinces)
					{
						if (item.Value == 0)
						{
							datas.Add(new EmploymentData { Region = item.Key, Population = 0,Type = "全国",Year=year });
						}
					}
					bool b = await tableReadRepository.UpdateAndCreate(datas);
					if (!b)
					{
						return Ok("添加数据到数据库失败或无更新内容");
					}
					return Ok();
				} else if(file.FileName.Contains("省内"))
				{
					string[] cities = ["杭州", "宁波", "温州", "嘉兴", "湖州", "绍兴", "金华", "衢州", "舟山", "台州", "丽水"];
					Dictionary<string, int> hasCities = new Dictionary<string, int>();
					for (int i = 0; i < cities.Length; i++)
					{
						hasCities[cities[i]] = 0;
					}
					datas.ForEach(data => {
						if (cities.Contains(data.Region))
						{
							hasCities[data.Region] = 1;
						}
					});
					foreach (var item in hasCities)
					{
						if (item.Value == 0)
						{
							datas.Add(new EmploymentData { Region = item.Key, Population = 0,Type = "省内",Year=year });
						}
					}
					bool b = await tableReadRepository.UpdateAndCreate(datas);
					if (!b)
					{
						return BadRequest("添加数据到数据库失败");
					}
					return Ok();
				}
				return BadRequest("文件错误");
			}
		}

		[HttpGet]
		[Route("{year}/chinamap")]
		public async Task<ActionResult> GetChinamap(int year)
		{
			List<EmploymentDataDTO> chinamapDatas = new List<EmploymentDataDTO>();
			var empdatas = await tableReadRepository.FindByYear(year,"chinamap");
			foreach(var e in empdatas)
			{
				chinamapDatas.Add(mapper.Map<EmploymentDataDTO>(e));
			}
			return Ok(chinamapDatas);
		}
		[HttpGet]
		[Route("{year}/mobility")]
		public async Task<ActionResult> GetMobility(int year)
		{
			List<EmploymentDataDTO> chinamapDatas = new List<EmploymentDataDTO>();
			var empdatas = await tableReadRepository.FindByYear(year, "mobility");
			foreach (var e in empdatas)
			{
				chinamapDatas.Add(mapper.Map<EmploymentDataDTO>(e));
			}
			return Ok(chinamapDatas);
		}
		[HttpGet]
		[Route("wordcloud/books")]
		public async Task<ActionResult> GetBooks()
		{
			List<ReptileDataDTO> reptileDataDTOs = new List<ReptileDataDTO>();
			var datas = await reptileDataRepository.GetAll();
			foreach(var data in datas)
			{
				reptileDataDTOs.Add(mapper.Map<ReptileDataDTO>(data));
			}
			return Ok(reptileDataDTOs);
		}
	}
}
