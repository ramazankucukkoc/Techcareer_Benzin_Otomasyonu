using System.Collections.Generic;
using System.Linq;
using Techcareer_Benzin_Otomasyonu.Entities;
using Techcareer_Benzin_Otomasyonu.FileManager;

namespace Techcareer_Benzin_Otomasyonu.Services
{
    public class ProductService
    {
        private List<Product> _products;
        private ExcelFileManager<Product> _excelFileManager;
        public ProductService(string filePath, string sheetName)
        {
            _products = new List<Product>();
            _excelFileManager = new ExcelFileManager<Product>(filePath, sheetName);
        }
        public List<Product> GetProducts()
        {
            return _excelFileManager.ReadDataFromExcel();
        }
        public Product GetProductById(int id)
        {
            List<Product> products = _excelFileManager.ReadDataFromExcel();
            return products.FirstOrDefault(p => p.Id == id);
        }
        public Product GetProductByName(string productName)
        {
            List<Product> products = _excelFileManager.ReadDataFromExcel();
            return products.FirstOrDefault(p => p.Name.ToLower().Trim() == productName.ToLower().Trim());
        }
        public void CreateProduct(Product product)
        {
            _products.Add(product);
            _excelFileManager.WriteDataToExcel(_products);
        }
        public void DeleteProduct(int id)
        {
            List<Product> products =_excelFileManager.ReadDataFromExcel();
            products.Remove(GetProductById(id));
            _excelFileManager.WriteDataToExcel(products);
        }
    }
}
