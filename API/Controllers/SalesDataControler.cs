﻿using API.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesDataControler : ControllerBase
    {
        private List<SalesData> _sales = new List<SalesData>();
        public SalesDataControler()
        {
            _sales = ReadSalesData();
        }

        private List<SalesData> ReadSalesData()
        {
            var list = new List<SalesData>();
            using (var package = new ExcelPackage(new FileInfo(@"Data\sample-xlsx-file-for-testing.xlsx")))
            {
                var worksheet = package.Workbook.Worksheets[0];

                var rows = worksheet.Dimension.End.Row;
                var columns = worksheet.Dimension.End.Column;

                for (int row = 2; row <= rows; row++)
                {
                    list.Add(new SalesData
                    {
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
                        MonthNumber = int.Parse(worksheet.Cells[row, 14].Text),
                        MonthName = worksheet.Cells[row, 15].Text,
                        Year = int.Parse(worksheet.Cells[row, 16].Text)

                    });
                }
            }
            return list;
        }



        [HttpGet]
        public IEnumerable<SalesData> Get()
        {
            return _sales;
        }

        [HttpGet("Segment/")]
        public IEnumerable<SalesData> GetSegment(string segment)
        {
            return _sales.Where(s => s.Segment.ToLower() == segment.ToLower());
        }

        [HttpGet("Country/")]
        public IEnumerable<SalesData> GetCountry(string country)
        {
            return _sales.Where(s => s.Country.ToLower() == country.ToLower());
        }

        [HttpGet("Product/")]
        public IEnumerable<SalesData> GetProduct(string product)
        {
            return _sales.Where(s => s.Product.ToLower() == product.ToLower());
        }

        [HttpGet("Report/")]
        public IEnumerable<Report> CreateReport()
        {
            var report = new List<Report>();

            var countries = _sales.Select(s => s.Country).Distinct();
            var segments = _sales.Select(s => s.Segment).Distinct();

            foreach (var country in countries)
            {
                foreach (var segment in segments)
                {
                    report.Add(new Report { Country = country, Segment = segment, UnitsSold = _sales.Where(s => (s.Country == country) && (s.Segment == segment)).Sum(s => s.UnitsSold) });
                }
            }

            return report;
        }

        [HttpPost("AddToSales")]
        public void AddSales(SalesData salesData)
        {
            _sales.Add(salesData);
        }
    }
}
