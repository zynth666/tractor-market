namespace TractorMarket.Models;

/// <summary>
/// Describe an Entity as Itemisable, so it can be added as a CartItem.
/// </summary>
public interface Itemisable
{
    public int Id { get; set; }
    public double Price { get; set; }
    public double AdminPrice { get; }
    public string Name { get; set; }
    public int Stock { get; set; }
}
