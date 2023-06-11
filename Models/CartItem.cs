using CommunityToolkit.Mvvm.ComponentModel;
using TractorMarket.Entities;
using TractorMarket.Services;

namespace TractorMarket.Models;

public partial class CartItem : ObservableObject
{
    public Tractor Tractor { get; set; }

    [ObservableProperty]
    private int _quantity;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Quantity))]
    private double _sum;

    public CartItem(Tractor tractor, int quantity)
    {
        Tractor = tractor;
        Quantity = quantity;

        double tractorPrice = 0;

        if (UserService.LoggedInUser!.IsAdmin)
        {
            tractorPrice = tractor.AdminPrice;
        }
        else
        {
            tractorPrice = tractor.Price;
        }

        Sum = tractorPrice * quantity;
    }

    partial void OnQuantityChanged(int value)
    {
        double tractorPrice = 0;

        if (UserService.LoggedInUser!.IsAdmin)
        {
            tractorPrice = Tractor.AdminPrice;
        }
        else
        {
            tractorPrice = Tractor.Price;
        }

        Sum = tractorPrice * Quantity;
    }

    override public string ToString()
    {
        return $"{Tractor.Type} - {Quantity}";
    }
}
