namespace ChargerLotLib.Models
{
    public static class Lots
    {
        
        public static readonly Lot ParkingGarage = new Lot
        {
            Id = "IMF",
            Name = "Parking Garage",
            Spaces = 200
        };
    }
    public struct Lot
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public int Spaces { get; set; }
    }
}