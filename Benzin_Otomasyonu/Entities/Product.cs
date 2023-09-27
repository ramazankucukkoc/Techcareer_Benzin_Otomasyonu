using System;
using System.Collections.Generic;

namespace Benzin_Otomasyonu.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; } 
        public DateTime CreatedDate { get; set; }
        public int Quantity { get; set; }//Adet
        public decimal TotalPrice { get; set; }

        public decimal TotalPriceCal()
        {
            return Price*Quantity;
        }

    }
}
