using MediatR;


namespace ShopAPI.Application.Commands
{
    public class EditProductCommand : IRequest<bool>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string CategoryId { get; set; }
        public int Quantity { get; set; }
    }
}
