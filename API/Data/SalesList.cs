using OfficeOpenXml;

namespace API.Data
{
    public static  class SalesList
    {
        private static List<SalesData>? _sales;

        public static List<SalesData> ConstructList()
        {
            if (_sales == null)
            {
                _sales = ReadSalesData();
            }
            return _sales;
        }

        
    }
}
