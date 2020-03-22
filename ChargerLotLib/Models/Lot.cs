using System;
using System.Collections.Generic;
using System.Text;
using Google.Type;

namespace ChargerLotLib.Models
{
    public struct Lot
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public int Spaces { get; set; }
        public LatLng LatLng { get; set; }
    }
}
