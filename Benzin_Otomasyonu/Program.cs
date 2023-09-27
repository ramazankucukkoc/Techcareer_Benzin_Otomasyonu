using System;
using Benzin_Otomasyonu.Entities;
using Benzin_Otomasyonu.Services;

namespace Benzin_Otomasyonu
{
    internal class Program
    {
        static ProductService productService;
        static void Main(string[] args)
        {

            productService =new ProductService();
            int yanlisGirisSayısı = 0;
            Console.WriteLine("Email Adresinizi Giriniz");
            string email =Console.ReadLine();
            Console.WriteLine("Şifrenizi Giriniz");
            string password = Console.ReadLine();
            if (IsLogin(email,password) == true)
            {
                Console.WriteLine("1.Market işlemleri için 1 Tuşlayın");
                Console.WriteLine("2.Petrol (Pompa) işlemleri için 2 Tuşlayın");
                int secim1 = int.Parse(Console.ReadLine());
                switch (secim1)
                {
                    case 1:
                        while (true)
                        {
                            Console.WriteLine("Lütfen Yapmak istediğiniz işlemi giriniz:");
                            Console.WriteLine("1- Ürünler ");
                            Console.WriteLine("2- Ürün Satışı ");
                            Console.WriteLine("3-Çıkış");

                            Console.WriteLine("Seçiminiz Giriniz ");
                            string secim2 = Console.ReadLine();

                            switch (secim2)
                            {
                                case "1":
                                    Console.WriteLine("Alış Sayfasına Hoşgeldiniz..");

                                    foreach (var product in productService.GetAll())
                                    {
                                        Console.WriteLine($"{product.Id}-{product.Name}- {product.Price}-{product.CreatedDate}-{product.Quantity}-{product.TotalPrice = product.TotalPriceCal()}");
                                    }

                                    break;
                                case "2":
                                    Console.WriteLine("Satış Sayfasına Hoşgeldiniz..");
                                    productService.AddExcel(productService.GetAll());

                                    foreach (var product in productService.GetAll())
                                    {
                                        Console.WriteLine($"{product.Id}-{product.Name}- {product.Price}-{product.CreatedDate}-{product.Quantity}-{product.TotalPrice=product.TotalPriceCal()}");
                                    }
                                   
                                    Console.WriteLine("Ürünlerde birinin ismini yazabilirsin");
                                    string urunAdi = Console.ReadLine();
                                    Console.WriteLine("Ürünlerden Kaç adet alacagınızı Yazabilirsiniz");
                                    int quantity = int.Parse(Console.ReadLine());
                                    Product product1 = productService.StockReduction(urunAdi, quantity);
                                    Console.WriteLine($"{product1.Name} ürün {quantity} adet satıldı toplam hesap ={product1.TotalPrice=quantity*product1.Price}");
                                    break;
                                case "4":
                                    return;

                                default:
                                    Console.WriteLine("Geçersiz Seçim");
                                    break;
                            }
                        }
                    case 2:

                        break;
                    default:
                        break;
                }
            }
            else
            {
                yanlisGirisSayısı++;
                Console.WriteLine($"Şifre veya Email adresiniz Yanlış lütfen tekrar deneyiniz {(3 - yanlisGirisSayısı)} hakkınız kaldı. DİKKAT !!!!!!");
            }
            if (yanlisGirisSayısı == 3)
            {
                Console.WriteLine("Sistem bloke oldu bilgi işlemle iletişime geçin.");
                return;
            }

        }
        static bool IsLogin(string email,string password)
        {
            if (email.Trim().ToLower() == "benzin@gmail.com" && password =="benzin")
            {
                Console.WriteLine("Login işlem başarılı.");
                return true;
            }
            Console.WriteLine("Login işlem başarısız.Email veya password yanlış");
            return false;
        }
    }
}
