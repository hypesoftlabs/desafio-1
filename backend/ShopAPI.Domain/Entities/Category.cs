using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ShopAPI.Domain.Entities
{
    public class Category
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; private set; } = ObjectId.GenerateNewId().ToString();

        public string Name { get; private set; }

      
        public Category(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("O nome da categoria não pode ser vazio.");

            Name = name;
        }

        public void UpdateName(string newName)
        {
            if (string.IsNullOrEmpty(newName))
                throw new ArgumentException("O nome da categoria não pode ser vazio.");

            Name = newName;
        }

        private Category() { }
    }
}