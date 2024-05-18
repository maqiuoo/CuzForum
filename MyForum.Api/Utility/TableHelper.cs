using OfficeOpenXml;

namespace MyForum.Api.Utility
{
	public static class TableHelper
	{
		public static ExcelWorksheet ReadTableData(string filePath)
		{
			// If you use EPPlus in a noncommercial context
			// according to the Polyform Noncommercial license:
			ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
			try
			{
				var fileInfo = new FileInfo(filePath);
				ExcelPackage package = new ExcelPackage(fileInfo);
				ExcelWorksheet worksheet = package.Workbook.Worksheets[0]; // 获取第一个工作表
				return worksheet;

			} catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return null;
			}

		}
	}
}
