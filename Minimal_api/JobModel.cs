using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Minimal_api
{
    public class JobModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }


        [BsonElement("organization")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string organization { get; set; } = null!;

        public string position { get; set; } = null!;

        [BsonRepresentation(BsonType.ObjectId)]
        public string category { get; set; } = null!;

        public string jobLevel { get; set; } = null!;

        public string employmentType { get; set; } = null!;

        public string[] skills { get; set; } = null!;
        public decimal salary { get; set; }

        public string overview { get; set; } = null!;
        public string requirements { get; set; } = null!;

        public string[] keywords { get; set; } = null!;

        public DateTime? createdAt { get; set; } = null;

        public DateTime? updatedAt { get; set; } = null;

        public decimal __v { get; set; }
    }
}
