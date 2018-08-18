using MongoDB.Driver;
using pollseidon.facade.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace pollseidon.facade.DAC
{
    public class Dac : IDac
    {
        private readonly IMongoClient client;
        private readonly IMongoDatabase database;
        private readonly IMongoCollection<Topic> TopicCollection;

        public Dac()
        {
            client = new MongoClient("mongodb://pollseidon:rP7avOyOpCMftuxuyA2APY4EAVWaOJyiexBkDD83uWvZvVTcnDTP8Z00rl2sahks7ql3cjTuZ2DALLnnR1dpxA==@pollseidon.documents.azure.com:10255/?ssl=true&replicaSet=globaldb&maxIdleTimeMS=150000&minPoolSize=2");
            database = client.GetDatabase("pollseidon");

            TopicCollection = database.GetCollection<Topic>("Topic");
        }

        public void CreateTopic(Topic newTopic)
        {
            TopicCollection.InsertOne(newTopic);
        }

        public Topic GetTopic(Expression<Func<Topic, bool>> expression)
        {
            return TopicCollection.Find(expression).FirstOrDefault();
        }

        public IEnumerable<Topic> GetTopicList(Expression<Func<Topic, bool>> expression)
        {
            return TopicCollection.Find(expression).ToList();
        }

        public void UpdateTopic(Topic topic)
        {
            TopicCollection.ReplaceOne(x => x.id == topic.id, topic);
        }
    }
}
