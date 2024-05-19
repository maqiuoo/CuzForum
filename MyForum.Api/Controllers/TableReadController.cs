using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyForum.Api.Utility;
using MyForum.DAL.Interface;
using MyForum.Model;
using MyForum.Model.DTO;
using System.Drawing;

namespace MyForum.Api.Controllers
{
	[Route("/")]
	[ApiController]
	public class TableReadController : ControllerBase
	{
		private readonly IWebHostEnvironment _environment;
		private readonly ITableReadRepository tableReadRepository;
		private readonly IMapper mapper;

		public TableReadController(IWebHostEnvironment environment, ITableReadRepository tableReadRepository, IMapper mapper)
		{
			_environment = environment;
			this.tableReadRepository = tableReadRepository;
			this.mapper = mapper;
		}
		[HttpPost]
		[Route("upload")]
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
				bool b = await tableReadRepository.AddRange(datas);
				if (!b)
				{
					return BadRequest("添加数据到数据库失败");
				}
				return Ok();
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
	}
}
