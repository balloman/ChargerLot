using System;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;

namespace ChargerLotLib.Handlers
{
    /**
     * Implements singleton structure
     */
    public class FirestoreHandler
    {
        private static FirestoreHandler _instance;
        private FirestoreDb _db;
        
        private FirestoreHandler()
        {
            var fb = FirebaseAdmin.FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile("chargerlot-service-key.json")
            });
            _db = FirestoreDb.Create("chargerlot");
            Console.WriteLine("Created Cloud Firestore client with project ID: chargerlot");
        }

        /// <summary>
        /// Use this method to get the FirestoreHandler instance
        /// </summary>
        /// <returns>An instance of the Firestore Handler</returns>
        public static FirestoreHandler GetInstance()
        {
            return _instance ??= new FirestoreHandler();
        }
        
    }
}