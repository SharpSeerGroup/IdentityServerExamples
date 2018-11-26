using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharpSeerGroup.Examples.WebApi.Data
{
    public class Mongo
    {
        public MongoClient Client { get; private set; }
        public IMongoDatabase Db { get; set; }


        public Mongo(string connectionString)
        {
            Client = new MongoClient(connectionString);
            Db = Client.GetDatabase("Chat");
            var chatCollection = Db.GetCollection<ChatMessage>("Messages");

        }

        public IMongoCollection<ChatMessage> GetMessagesCollection()
        {
            return Db.GetCollection<ChatMessage>("Messages");
        }
    }
}
