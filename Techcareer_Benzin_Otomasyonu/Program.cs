using System;
using Techcareer_Benzin_Otomasyonu.Entities;
using Techcareer_Benzin_Otomasyonu.Services;

namespace Techcareer_Benzin_Otomasyonu
{
    internal class Program
    {
        static ProductService productService;
        static SaleService saleService;

        static void Main(string[] args)
        {
            string path = "C:\\Users\\Ramazan\\OneDrive\\Masaüstü\\Odevler.xlsx";
            productService=new ProductService(path,"products");
            saleService = new SaleService(productService, path, "sales");

            productService.CreateProduct(new Product() { Id = 1, Name = "Su", Price = 5 });
            productService.CreateProduct(new Product() { Id = 2, Name = "Kahve", Price = 12 });
            productService.CreateProduct(new Product() { Id = 3, Name = "Sandviç", Price = 25 });
            productService.CreateProduct(new Product() { Id = 4, Name = "Çikolata", Price = 10 });

            while (true)
            {
                Console.Clear();
                Console.WriteLine("1. Ürün Satış");
                Console.WriteLine("2. Market");
                Console.WriteLine("0. Çıkış");
                Console.Write("Bir sayı giriniz: ");

                ReadUserChoice(out int choice);
                switch (choice)
                {

                    case 1:
                        do
                        {
                            SaleProduct();

                            Console.WriteLine("Yeni ürün için 'Enter' basınız yada bitirmek için '0' giriniz'");
                            // Fiş için ayrı bir nesne oluşturulup ayrı bir excelde toplam alışverişin fişi yazılabilir

                        } while ((Console.ReadLine() == "0") ? false : true);
                        break;
                    case 2:
                        bool isWorking = true;
                        while (isWorking)
                        {
                            Console.Clear();
                            Console.WriteLine("1. Product Excel");
                            Console.WriteLine("2. Sales Excel");
                            Console.WriteLine("0. Çıkış");
                            Console.Write("Bir sayı giriniz: ");
                            ReadUserChoice(out choice);

                            switch (choice)
                            {
                                case 1:
                                    Console.Clear();
                                    foreach (Product product in productService.GetProducts())
                                    {
                                        Console.WriteLine($"Id: {product.Id}, Product Name: {product.Name}, Product Price: {product.Price}");
                                    }
                                    Console.WriteLine("Press any key");
                                    Console.ReadKey();
                                    break;
                                case 2:
                                    Console.Clear();
                                    foreach (Sale sale in saleService.GetSales())
                                    {
                                        Console.WriteLine($"Product Name: {sale.ProductName}, Quantity: {sale.Quantity}, Total Price: {sale.TotalPrice}, Date: {sale.SaleDate}");
                                    }
                                    Console.WriteLine("Press any key");
                                    Console.ReadKey();
                                    break;
                                case 0:
                                    isWorking = false;
                                    break;
                            }
                        }
                        break;
                    case 0:
                        return;
                }
            }
        }
        private static void SaleProduct()
        {
            Console.Clear();
            foreach (Product product in productService.GetProducts())
            {
                Console.WriteLine($"Product ={product.Name} , Price ={product.Price}");
            }
            Console.Write("Ürün Adını Giriniz");
            ReadProductName(out string name);

            Console.Write("Ürün Sayısı Giriniz");
            ReadQuantity(out int quantity);

            bool isSuccess = saleService.SellProduct(name, quantity);
        }
        private static void ReadProductName(out string productName)
        {
            productName = Console.ReadLine();
            productName.Trim().ToLower();

        }
        private static void ReadQuantity(out int quantity)
        {
            if (!int.TryParse(Console.ReadLine(), out quantity))
                Console.WriteLine("Invalid Value");
        }
        private static void ReadUserChoice(out int choice)
        {
            if(!int.TryParse(Console.ReadLine(),out choice))
                Console.WriteLine("Invalid value.");
        }
    }
}
