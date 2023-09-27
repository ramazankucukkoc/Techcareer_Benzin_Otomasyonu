using System;

namespace Techcareer_Benzin_Otomasyonu.Entities
{
    public class Sale
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime SaleDate { get; set; }
    }
}
