using System.Globalization;

namespace API.Data
{
    public class SalesData
    {
        public int Id { get; set; }
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

        public int MonthNumber { get => _monthNumber; }
        public string MonthName { get => _monthName; }
        public int Year { get => _year; }

        private int _year => Date.Year;
        private int _monthNumber => Date.Month;
        private string _monthName => Date.ToString("MMMM", CultureInfo.InvariantCulture);
    }
}
