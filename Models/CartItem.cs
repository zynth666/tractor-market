using CommunityToolkit.Mvvm.ComponentModel;
using TractorMarket.Entities;

namespace TractorMarket.Models;

public partial class CartItem : ObservableObject
{
    public Tractor Tractor { get; set; }

    [ObservableProperty]
    private int _quantity;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Quantity))]
    private long _sum;

    public CartItem(Tractor tractor, int quantity) 
    {
        Tractor = tractor;
        Quantity = quantity;
        Sum = tractor.Price * quantity;
    }

    partial void OnQuantityChanged(int value)
    {
        Sum = Tractor.Price * Quantity;
    }

    override public string ToString()
    {
        return $"{Tractor.Type} - {Quantity}";
    }
}
