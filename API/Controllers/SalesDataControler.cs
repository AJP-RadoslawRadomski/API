using API.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesDataControler : ControllerBase
    {
        public SalesDataControler() { }

        [HttpGet]
        public IEnumerable<SalesData> Get()
        {
            return ListOfSales.Instance.SalesData;
        }

        [HttpGet("Segment/")]
        public IEnumerable<SalesData> GetSegment(string segment)
        {
            return ListOfSales.Instance.SalesData.Where(s => string.Compare(s.Segment, segment, true) == 0);
        }

        [HttpGet("Country/")]
        public IEnumerable<SalesData> GetCountry(string country)
        {
            return ListOfSales.Instance.SalesData.Where(s => string.Compare(s.Country, country, true) == 0);
        }

        [HttpGet("Product/")]
        public IEnumerable<SalesData> GetProduct(string product)
        {
            return ListOfSales.Instance.SalesData.Where(s => string.Compare(s.Product, product, true) == 0);
        }

        [HttpGet("Report/")]
        public IEnumerable<Report> CreateReport()
        {
            var report = new List<Report>();

            var groups = ListOfSales.Instance.SalesData.GroupBy(s => new { s.Country, s.Segment }).OrderBy(g => g.Key.Country).ThenBy(g => g.Key.Segment);

            foreach (var group in groups)
            {
                report.Add(new Report
                {
                    Country = group.Key.Country,
                    Segment = group.Key.Segment,
                    UnitsSold = group.Sum(s => s.UnitsSold)
                });
            }

            return report;
        }

        [HttpPost("AddToSales")]
        public void AddSales(SalesData salesData)
        {
            ListOfSales.AddSalesData(salesData);
        }

        [HttpDelete("RemoveSaleData")]
        public bool RemoveSaleData(int id)
        {
            return ListOfSales.RemoveSalesDataFromExcel(id);
        }

        [HttpGet("GetById")]
        public SalesData? GetDataById(int id)
        {
            return ListOfSales.Instance.SalesData.SingleOrDefault(s => s.Id == id);
        }
    }
}
