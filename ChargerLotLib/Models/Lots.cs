using System;
using System.Linq;
using Google.Type;

namespace ChargerLotLib.Models
{
    public static class Lots
    {

        public static readonly Lot ParkingGarage = new Lot
        {
            Id = "IMF",
            Name = "Parking Garage",
            Spaces = 200,
            LatLng = new LatLng{ Latitude = 34.7261875, Longitude = -86.6386875 }
        };

        public static Lot[] LotList = { ParkingGarage };

        public static Lot GetLotByName(string name)
        {
            return LotList.First(lot => string.Equals(name, lot.Name, StringComparison.CurrentCultureIgnoreCase));
        }
    }
}