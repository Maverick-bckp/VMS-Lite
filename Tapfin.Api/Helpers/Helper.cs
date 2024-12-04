using OfficeOpenXml;

namespace Tapfin.Api.Helpers
{
    public class Helper
    {
        public List<T> ReadExcelFile<T>(IFormFile file, Func<ExcelRange, T> mapFunction)
        {
            var list = new List<T>();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // Set EPPlus license context

            using (var stream = new MemoryStream())
            {
                file.CopyTo(stream);

                using (var package = new ExcelPackage(stream))
                {
                    var worksheet = package.Workbook.Worksheets[0];
                    var rowCount = worksheet.Dimension.Rows;

                    for (int row = 2; row <= rowCount; row++) // Assuming row 1 is the header
                    {
                        var range = worksheet.Cells[row, 1, row, worksheet.Dimension.Columns];
                        var item = mapFunction(range);
                        list.Add(item);
                    }
                }
            }

            return list;
        }
    }
}
