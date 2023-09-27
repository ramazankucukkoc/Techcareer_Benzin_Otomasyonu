using System;
using System.Collections.Generic;
using Techcareer_Benzin_Otomasyonu.Entities;
using Techcareer_Benzin_Otomasyonu.FileManager;

namespace Techcareer_Benzin_Otomasyonu.Services
{
    public class SaleService
    {
        private List<Sale> _sales;
        private ProductService _productService;
        private ExcelFileManager<Sale> _excelFileManager;

        public SaleService(ProductService productService, string filePath, string sheetName)
        {
            _sales = new List<Sale>();
            _productService = productService;
            _excelFileManager = new ExcelFileManager<Sale>(filePath, sheetName);
        }
        public bool SellProduct(string productName,int quantity)
        {
            Product product =_productService.GetProductByName(productName);
            if (product == null)
            {
                return false;
            }
            _sales = _excelFileManager.ReadDataFromExcel();
            _sales.Add(new Sale { ProductName = product.Name, Quantity = quantity, TotalPrice = product.Price * quantity, SaleDate = DateTime.Now });
            _excelFileManager.WriteDataToExcel(_sales);
            return true;
        }
        public List<Sale> GetSales()
        {
            return _excelFileManager.ReadDataFromExcel();
        }
    }
}
