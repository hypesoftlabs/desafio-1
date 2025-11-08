using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Xml.Linq;

namespace ShopAPI.Domain.Entities
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; private set; } = ObjectId.GenerateNewId().ToString();
        public string Name { get; private set; }
        public string Description { get; private set; }
        public double Price { get; private set; }
        public int Quantity { get; private set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string CategoryId { get; private set; }

        public Product(string name, string description, double preco, string categoryId, int quantity)
        {
           
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("O nome não pode ser vazio.");

            if (preco < 0)
                throw new ArgumentException("O preço deve ser maior que 0.");

            if (string.IsNullOrEmpty(categoryId)) 
                throw new ArgumentException("O ID da categoria é obrigatório.");


            Name = name;
            Description = description;
            Price = preco;
            CategoryId = categoryId;
            Quantity = quantity;

        }

        public void ChangeName(string newName)
        {
            if (string.IsNullOrEmpty(newName))
                throw new ArgumentException("O nome não pode ser nulo ou vazio");

            Name = newName;
        }

        public void ChangeDescription(string newDesc)
        {
            if (string.IsNullOrEmpty(newDesc))
                throw new ArgumentException("A descrição não pode ser nula ou vazia");

            Description = newDesc;

        }


        public void ChangeQuantity(int novaQuantidade)
        {
            if (novaQuantidade < 0)
                throw new ArgumentException("O estoque não pode ser negativo.");

            Quantity = novaQuantidade;
        }

        public void ChangePrice(double newPrice)
        {
            if (newPrice < 0)
                throw new ArgumentException("O preço não pode ser negativo.");

            Price = newPrice;
        }

        public void ChangeCategory(string newCategoryId)
        {
            if (string.IsNullOrEmpty(newCategoryId))
                throw new ArgumentException("O ID da categoria é obrigatório.");

            CategoryId = newCategoryId;
        }


        private Product() { }
    }
}