using API.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesDataControler : ControllerBase
    {
        public SalesDataControler()
        {

        }

        [HttpGet]
        public IEnumerable<SalesData> Get()
        {
            return ListOfSales.Instance.SalesData;
        }

        [HttpGet("Segment/")]
        public IEnumerable<SalesData> GetSegment(string segment)
        {
            return ListOfSales.Instance.SalesData.Where(s => s.Segment.ToLower() == segment.ToLower());
        }

        [HttpGet("Country/")]
        public IEnumerable<SalesData> GetCountry(string country)
        {
            return ListOfSales.Instance.SalesData.Where(s => s.Country.ToLower() == country.ToLower());
        }

        [HttpGet("Product/")]
        public IEnumerable<SalesData> GetProduct(string product)
        {
            return ListOfSales.Instance.SalesData.Where(s => s.Product.ToLower() == product.ToLower());
        }

        [HttpGet("Report/")]
        public IEnumerable<Report> CreateReport()
        {
            var report = new List<Report>();

            var countries = ListOfSales.Instance.SalesData.Select(s => s.Country).Distinct();
            var segments = ListOfSales.Instance.SalesData.Select(s => s.Segment).Distinct();

            foreach (var country in countries)
            {
                foreach (var segment in segments)
                {
                    report.Add(new Report 
                    { 
                        Country = country, 
                        Segment = segment, 
                        UnitsSold = ListOfSales.Instance.SalesData.Where(s => (s.Country == country) && (s.Segment == segment)).Sum(s => s.UnitsSold) 
                    });
                }
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
