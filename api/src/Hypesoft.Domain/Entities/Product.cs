namespace Hypesoft.Domain.Entities
{
  public class Product
  {
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Stock { get; set; }

    // Novo campo para categoria
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
  }
}
