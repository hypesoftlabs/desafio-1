using MediatR;


namespace ShopAPI.Application.Commands
{
    public class UpdateStorageCommand : IRequest<bool>
    {
        public string ProductId { get; set; }

        public int Quantity { get; set; }
    }
}
