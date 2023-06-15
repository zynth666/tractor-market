using CommunityToolkit.Mvvm.ComponentModel;
using TractorMarket.Entities;
using TractorMarket.Services;

namespace TractorMarket.Models;

public partial class CartItem<T> : ObservableObject where T : ItemisableBaseEntity
{
    public T Item { get; set; }

    [ObservableProperty]
    private int _quantity;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Quantity))]
    private double _sum;

    public CartItem(T item, int quantity)
    {
        Item = item;
        Quantity = quantity;

        double price = 0;

        if (UserService.LoggedInUser!.IsAdmin)
        {
            price = item.AdminPrice;
        }
        else
        {
            price = item.Price;
        }

        Sum = price * quantity;
    }

    partial void OnQuantityChanged(int value)
    {
        double price = 0;

        if (UserService.LoggedInUser!.IsAdmin)
        {
            price = Item.AdminPrice;
        }
        else
        {
            price = Item.Price;
        }

        Sum = price * Quantity;
    }

    override public string ToString()
    {
        return $"{Item.Name} - {Quantity}";
    }
}
