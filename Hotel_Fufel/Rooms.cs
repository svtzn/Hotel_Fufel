using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Fufel
{
    public class Room
    {
        public int Id { get; set; }
        public int RoomsAmount { get; set; }
        public decimal PricePerNight { get; set; }
        public string ImagePath { get; set; }
        public bool Smoke { get; set; }
        public bool Balcony { get; set; }
        public bool WiFi { get; set; }
        public string ComfortLevel { get; set; }
        public bool Kitchen { get; set; }
        public bool Shower { get; set; }
        public bool IsFree { get; set; }
    }
}
