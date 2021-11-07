using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDbTest.Models
{
    public class Employee: MongoEntity
    {
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("deptName")]

        public string DeptName { get; set; }
    }
}