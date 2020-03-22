using System;

namespace ChargerLotLib.Models
{
    public class LotSnapshot
    {
        public int Parked { get; set; }
        public double Full { get; set; }
        public DateTime Timestamp { get; set; }
        public Lot Lot { get; set; }
    }
}