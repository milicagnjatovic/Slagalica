using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace WhoKnowsKnows.Entities
{
    public class Question
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public long NumId { get; set; }
        public string Text { get; set; }
        public List<string> Answers { get; set; }
        public string CorrectAnswer { get; set; }
    }
}
