namespace TractorMarket.Models;

public interface Itemisable
{
    public int Id { get; set; }
    public double Price { get; set; }
    public double AdminPrice { get; }
    public string Name { get; set; }
    public int Stock { get; set; }
}
