﻿using System;
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
    }
}