using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Util;
using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;
using WriteResult = Google.Cloud.Firestore.WriteResult;

namespace ChargerLotLib.Handlers
{
    /**
     * Implements singleton structure
     */
    public class FirestoreHandler
    {
        private static FirestoreHandler _instance;
        private readonly FirestoreDb _db;
        private string Collection { get; set; }

        /// <summary>
        /// Use this method to get the FirestoreHandler instance
        /// </summary>
        /// <returns>An instance of the Firestore Handler</returns>
        public static FirestoreHandler GetInstance(string collection)
        {
            if (_instance == null)
            {
                _instance = new FirestoreHandler();
            }
            _instance.Collection = collection;
            return _instance;
        }
        
        private FirestoreHandler()
        {
            
            _db = FirestoreDb.Create("chargerlot",
                new FirestoreClientBuilder()
                {
                    CredentialsPath = "chargerlot-service-key.json"
                }.Build()
            );
            Console.WriteLine("Created Cloud Firestore client with project ID: chargerlot");
        }

        /// <summary>
        /// Adds a new document to the Handler's collection asyncly
        /// This will generate a random id for the document
        /// </summary>
        /// <param name="data">The data to add to the handler as a Dictionary object</param>
        /// <returns>The task that will attempt to add</returns>
        public Task<DocumentReference> AddData(Dictionary<string, object> data)
        {
            return _db.Collection(Collection).AddAsync(data);
        }
        
        /// <summary>
        /// Adds a new document to the Handler's collection asyncly
        /// This will generate a random id for the document
        /// </summary>
        /// <param name="data">An object of the same type as the data T</param>
        /// <typeparam name="T">The type of the data to add\n
        /// Must have annotation FirestoreData</typeparam>
        /// <returns>The task that will attempt the addition</returns>
        /// <exception cref="ArgumentException">Thrown if the argument passed does not have the
        /// FirestoreData attribute</exception>
        public Task<DocumentReference> AddData<T>(T data)
        {
            if (data.GetType().GetCustomAttribute<FirestoreDataAttribute>() == null)
            {
                throw new ArgumentException("This type does not support Firestore Data!",
                    nameof(data));
            }
            return _db.Collection(Collection).AddAsync(data);
        }
        
        /// <summary>
        /// Adds/Sets a document to the Handler's collection asyncly
        /// This function overwrites any previous data
        /// </summary>
        /// <param name="data">An object of the same type as the data T</param>
        /// <param name="document">The name of the document to set</param>
        /// <typeparam name="T">The type of the data to add\n
        /// Must have annotation FirestoreData</typeparam>
        /// <returns>The task that will attempt the addition</returns>
        /// <exception cref="ArgumentException">Thrown if the argument passed does not have the
        /// FirestoreData attribute</exception>
        public Task<WriteResult> SetData<T>(T data, string document)
        {
            if (data.GetType().GetCustomAttribute<FirestoreDataAttribute>() == null)
            {
                throw new ArgumentException("This type does not support Firestore Data!",
                    nameof(data));
            }
            var docRef = _db.Collection(Collection).Document(document);
            return docRef.SetAsync(data);
        }
        
        /// <summary>
        /// Adds/Sets a document to the Handler's collection asyncly
        /// This function overwrites any previous data in the document
        /// </summary>
        /// <param name="data">The data to add to the handler as a Dictionary object</param>
        /// <param name="document">The id of the document to set</param>
        /// <returns>The task that will attempt to set</returns>
        public Task<WriteResult> SetData(Dictionary<string, object> data, string document)
        {
            var docRef = _db.Collection(Collection).Document(document);
            return docRef.SetAsync(data);
        }

        /// <summary>
        /// Retrieves the data at the specified document
        /// </summary>
        /// <param name="name">The document to retrieve</param>
        /// <returns>A dictionary object containing the data</returns>
        public async Task<Dictionary<string, object>> GetData(string name)
        {
            var snapshot = await _db.Collection(Collection).GetSnapshotAsync();
            foreach (var document in snapshot.Documents)
            {
                Console.WriteLine("User: {0}", document.Id);
            }
            return snapshot.First(documentSnapshot => documentSnapshot.Id.Equals(name)).ToDictionary();
        }

        /// <summary>
        /// If none of the functions provided in the handler suit the use-case, this allows direct access to the
        /// database context
        /// </summary>
        /// <returns>The database context</returns>
        public FirestoreDb GetDb()
        {
            return _db;
        }
    }
}