using System;
using Google.Cloud.Firestore;

namespace ChargerLotLib.Models
{
    [FirestoreData]
    public class ParkingReport
    {
        [FirestoreProperty]
        public string Uuid { get; set; }
        [FirestoreProperty]
        public DateTime Timestamp { get; set; }
        [FirestoreProperty]
        public string Lot { get; set; }
        [FirestoreProperty]
        private int _fullnessFactor { get; set; }
        public Fullness FullnessFactor
        {
            get => (Fullness) _fullnessFactor;
            set => _fullnessFactor = (int)value;
        }

        public enum Fullness
        {
            Empty,
            MostlyEmpty,
            Moderate,
            MostlyFull,
            Full
        }
    }
}