using TractorMarket.Entities;

namespace TractorMarket.Models;

public class CartItem
{
    public Tractor Tractor { get; set; }
    public int Quantity { get; set; }

    public CartItem(Tractor tractor, int quantity) 
    {
        Tractor = tractor;
        Quantity = quantity;
    }

    override public string ToString()
    {
        return $"{Tractor.Type} - {Quantity}";
    }
}
