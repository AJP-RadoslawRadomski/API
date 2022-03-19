using OfficeOpenXml;

namespace API.Data
{
    public sealed class ListOfSales
    {
        private static readonly Lazy<ListOfSales> Lazy = new(() => new ListOfSales());
        public static ListOfSales Instance { get { return Lazy.Value; } }
        private ListOfSales() { }
        private const string DataFile = @"Data\sample-xlsx-file-for-testing.xlsx";

        public List<SalesData> SalesData => GetSalesData();

        private static  List<SalesData>? _salesData;

        private static List<SalesData> GetSalesData()
        {
            if (_salesData == null)
            {
                _salesData = ReadSalesData();
            }
            return _salesData;
        }

        /// <summary>
        /// Read sales data from Excel file.
        /// </summary>
        /// <returns></returns>
        private static List<SalesData> ReadSalesData()
        {
            var list = new List<SalesData>();
            using (var package = new ExcelPackage(new FileInfo(DataFile)))
            {
                var worksheet = package.Workbook.Worksheets[0];

                var rows = worksheet.Dimension.End.Row;

                for (int row = 2; row <= rows; row++)
                {
                    list.Add(new SalesData
                    {
                        Id = row,
                        Segment = worksheet.Cells[row, 1].Text,
                        Country = worksheet.Cells[row, 2].Text,
                        Product = worksheet.Cells[row, 3].Text,
                        DiscountBand = worksheet.Cells[row, 4].Text,
                        UnitsSold = (double)worksheet.Cells[row, 5].Value,
                        MnfPrice = (double)worksheet.Cells[row, 6].Value,
                        SalePrice = (double)worksheet.Cells[row, 7].Value,
                        GrossSales = (double)worksheet.Cells[row, 8].Value,
                        Discount = (double)worksheet.Cells[row, 9].Value,
                        Sales = (double)worksheet.Cells[row, 10].Value,
                        COGS = (double)worksheet.Cells[row, 11].Value,
                        Profit = (double)worksheet.Cells[row, 12].Value,
                        Date = (DateTime)worksheet.Cells[row, 13].Value,

                    });
                }
            }
            return list;
        }

        /// <summary>
        /// Append sales data to excel file.
        /// </summary>
        public static void AddSalesData(SalesData data)
        {
            using (var package = new ExcelPackage(new FileInfo(DataFile)))
            {
                var worksheet = package.Workbook.Worksheets[0];

                var row = worksheet.Dimension.End.Row + 1;

                worksheet.Cells[row, 1].Value = data.Segment;
                worksheet.Cells[row, 2].Value = data.Country;
                worksheet.Cells[row, 3].Value = data.Product;
                worksheet.Cells[row, 4].Value = data.DiscountBand;
                worksheet.Cells[row, 5].Value = data.UnitsSold;
                worksheet.Cells[row, 6].Value = data.MnfPrice;
                worksheet.Cells[row, 7].Value = data.SalePrice;
                worksheet.Cells[row, 8].Value = data.GrossSales;
                worksheet.Cells[row, 9].Value = data.Discount;
                worksheet.Cells[row, 10].Value = data.Sales;
                worksheet.Cells[row, 11].Value = data.COGS;
                worksheet.Cells[row, 12].Value = data.Profit;
                worksheet.Cells[row, 13].Value = data.Date;

                package.Save();
            }

            _salesData = ReadSalesData();
        }

        /// <summary>
        /// Removes sale data by row number.
        /// Returns <see langword="true"/> if succesful.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool RemoveSalesDataFromExcel(int id)
        {
            using (var package = new ExcelPackage(new FileInfo(DataFile)))
            {
                var worksheet = package.Workbook.Worksheets[0];
                var endRow = worksheet.Dimension.End.Row;
                if (endRow < id) return false;
                worksheet.DeleteRow(id);
                package.Save();

                _salesData = ReadSalesData();
                return true;
            }
        }
    }
}
