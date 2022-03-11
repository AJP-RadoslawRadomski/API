using OfficeOpenXml;

namespace API.Data
{
    public class SalesData
    {
        public string Segment { get; set; }
        public string Country { get; set; }
        public string Product { get; set; }
        public string DiscountBand { get; set; }
        public double UnitsSold { get; set; }
        public double MnfPrice { get; set; }
        public double SalePrice { get; set; }
        public double GrossSales { get; set; }
        public double Discount { get; set; }
        public double Sales { get; set; }
        public double COGS { get; set; }
        public double Profit { get; set; }
        public DateTime Date { get; set; }

        public int MonthNumber { get; set; }
        public string MonthName { get; set; }
        public int Year { get; set; }
    }
}
