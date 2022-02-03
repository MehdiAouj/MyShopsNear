using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
namespace MyShopsNear.Models
{
    [BsonIgnoreExtraElements]
    public class Shops
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("name")]
        [Required(ErrorMessage = "name is required")]
        public string Name { get; set; }

        [BsonElement("email")]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [BsonElement("city")]
        [Required(ErrorMessage = "city is required")]
        public string City { get; set; }

        [BsonElement("location")]
        [Required(ErrorMessage = "location is required")]
        public Array[] Location { get; set; }



    }
}
