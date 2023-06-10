using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using TractorMarket.Entities;
using TractorMarket.Helpers;
using TractorMarket.Models;

namespace TractorMarket.Services;

public class CartService
{
    private UserService _userService;
    private TractorService _tractorService;

    public CartService(UserService userService, TractorService tractorService)
    {
        _userService = userService;
        _tractorService = tractorService;
    }

    public void Checkout(DeepObservableCollection<CartItem> cart, User currentUser)
    {
        PaySeller(currentUser);
        RemoveSoldTractorsFromStock(cart);
        cart.Clear();
    }

    public long GetTotalPrice(DeepObservableCollection<CartItem> cart)
    {
        long sum = 0;
        foreach (CartItem cartItem in cart)
        {
            long tractorPrice = cartItem.Tractor.Price;
            long cartItemPrice = tractorPrice * cartItem.Quantity;

            sum += cartItemPrice;
        }

        return sum;
    }

    public void RemoveSoldTractorsFromStock(DeepObservableCollection<CartItem> cart)
    {
        foreach (CartItem cartItem in cart)
        {
            cartItem.Tractor.Stock -= cartItem.Quantity;
            _tractorService.UpdateTractor(cartItem.Tractor);
        }
    }

    public void PaySeller(User currentUser)
    {
        long moneyFromSale = GetTotalPrice(currentUser.Cart);

        currentUser.Budget -= moneyFromSale;
        _userService.GetAdmin().Budget += moneyFromSale;
    }

    public void AddToCart(DeepObservableCollection<CartItem> cart, CartItem cartItem)
    {
        foreach (CartItem item in cart)
        {
            if (item.Tractor.Id != cartItem.Tractor.Id)
            {
                continue;
            }

            int newItemQuantity = item.Quantity + cartItem.Quantity;

            if (newItemQuantity > item.Tractor.Stock)
            {
                item.Quantity = item.Tractor.Stock;
            }
            else
            {
                item.Quantity = newItemQuantity;
            }

            return;
        }

        cart.Add(cartItem);
    }
}
