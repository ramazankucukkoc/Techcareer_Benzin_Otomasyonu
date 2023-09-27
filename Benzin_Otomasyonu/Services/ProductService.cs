using Benzin_Otomasyonu.Constants;
using Benzin_Otomasyonu.Entities;
using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Benzin_Otomasyonu.Services
{
    public class ProductService
    {
        List<Product> _products;

        public ProductService()
        {
            _products = new List<Product>
            {
                new Product{Id=1,  Name="Bardak", Price=15,CreatedDate=DateTime.Now,Quantity=10},
                new Product{Id=2, Name="Kamera", Price=500,CreatedDate=DateTime.Now,Quantity = 20},
                new Product{Id=3, Name="Çikolata", Price=15,CreatedDate=DateTime.Now,Quantity=30},
                new Product{Id=4, Name="Bisküvi", Price=150, CreatedDate = DateTime.Now,Quantity=40},
                new Product{Id=5, Name="Kola", Price=85,CreatedDate=DateTime.Now,Quantity = 10}
            };

            using (var workbook = CreateOrGetWorkbook())
            {
                var worksheet = CreateOrGetWorksheet(workbook);
                worksheet.Cell(1, 1).Value = "Id";
                worksheet.Cell(1, 2).Value = "Name";
                worksheet.Cell(1, 3).Value = "Price";
                worksheet.Cell(1, 4).Value = "CreatedDate";
                worksheet.Cell(1, 5).Value = "Quantity";
                worksheet.Cell(1, 6).Value = "TotalPrice";
                workbook.SaveAs(Constant.FilePath);
            }
        }

        public Product StockReduction(string productName, int quantity)
        {
            Product product = GetByProductName(productName.ToLower().Trim());
            if (product == null)
            {
                Console.WriteLine($"{productName} adında ürün yoktur. ");
            }
            product.Quantity -= quantity;
            product.TotalPrice = product.TotalPriceCal();
          //  AddOneProduct(product.Id);
            return product;
        }
        public void Add(Product product)
        {
            _products.Add(product);
            AddExcel(_products);
            // GetAllExcel();
        }
        public void Remove(int id)
        {
            var product = GetById(id);
            _products.Remove(product);
            AddExcel(_products);
            GetAllExcel();
        }
        public List<Product> GetAll()
        {

            return _products.ToList();
        }
        public bool ExcelFileExists()
        {
            if (File.Exists(Constant.FilePath))
                return true;

            return false;
        }
        public void DeleteExcelFile()
        {
            File.Delete(Constant.FilePath);
        }
        public void AddOneProduct(int id)
        {
            using (var workbook = new XLWorkbook(Constant.FilePath))
            {
                var worksheet = workbook.Worksheet(Constant.WorkSheet);

                Product product = _products.FirstOrDefault(p => p.Id == id);

                if (product == null)
                {
                    return;
                }
                worksheet.Cell(product.Id, 1).Value = product.Id;
                worksheet.Cell(product.Id, 2).Value = product.Name;
                worksheet.Cell(product.Id, 3).Value = product.Price;
                worksheet.Cell(product.Id, 4).Value = product.CreatedDate;
                worksheet.Cell(product.Id, 5).Value = product.Quantity;
                product.TotalPrice = product.TotalPriceCal();
                worksheet.Cell(product.Id, 6).Value = product.TotalPrice;
                workbook.SaveAs(Constant.FilePath);
            }
        }

        public void AddExcel(List<Product> products)
        {
            using (var workbook = new XLWorkbook(Constant.FilePath))
            {
                var worksheet = workbook.Worksheet(Constant.WorkSheet);
                for (int i = 0; i < products.Count; i++)
                {
                    var product = products[i];
                    worksheet.Cell(i + 2, 1).Value = product.Id;
                    worksheet.Cell(i + 2, 2).Value = product.Name;
                    worksheet.Cell(i + 2, 3).Value = product.Price;
                    worksheet.Cell(i + 2, 4).Value = product.CreatedDate;
                    worksheet.Cell(i + 2, 5).Value = product.Quantity;
                    product.TotalPrice = product.TotalPriceCal();
                    worksheet.Cell(i + 2, 6).Value = product.TotalPrice;
                    workbook.SaveAs(Constant.FilePath);

                }
            }
        }
        public Product GetByProductName(string productName)
        {
            List<Product> products = GetAllExcel();
            Product product = products.FirstOrDefault(p => p.Name.ToLower().Trim() == productName.ToLower().Trim());
            return product;
        }
        public Product GetById(int id)
        {
            List<Product> products = GetAllExcel();
            var product = products.FirstOrDefault(p => p.Id == id);
            return product;
        }
        public List<Product> GetAllExcel()
        {
            using (var workbook = new XLWorkbook(Constant.FilePath))
            {
                var properties = typeof(Product).GetProperties();
                IXLWorksheet worksheet = workbook.Worksheet(Constant.WorkSheet);
                var rows = worksheet.RowsUsed().Skip(1);
                foreach (var item in rows)
                {
                    Product product = new Product();
                    for (int i = 0; i < properties.Length; i++)
                    {
                        var value = item.Cell(i + 1).Value.ToString();
                        var type = properties[i].PropertyType;
                        var propertyValue = Convert.ChangeType(value, type);
                        properties[i].SetValue(product, propertyValue);
                    }
                    _products.Add(product);

                }
            }
            return _products;
        }
        private IXLWorkbook CreateOrGetWorkbook()
        {
            if (ExcelFileExists())
                return new XLWorkbook(Constant.FilePath);
            return new XLWorkbook();
        }
        private IXLWorksheet CreateOrGetWorksheet(IXLWorkbook workbook)
        {
            if (workbook.Worksheets.Contains(Constant.WorkSheet))
                return workbook.Worksheet(Constant.WorkSheet);
            return workbook.Worksheets.Add(Constant.WorkSheet);
        }
    }
}
