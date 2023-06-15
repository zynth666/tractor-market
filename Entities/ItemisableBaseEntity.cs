using TractorMarket.Models;

namespace TractorMarket.Entities;

abstract public class ItemisableBaseEntity : Itemisable
{
    public abstract int Id { get; set; }
    public abstract double Price { get; set; }
    public abstract double AdminPrice { get; }
    public abstract string Name { get; set; }
    public abstract int Stock { get; set; }
}
