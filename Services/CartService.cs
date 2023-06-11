using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using TractorMarket.Entities;
using TractorMarket.Helpers;
using TractorMarket.Models;

namespace TractorMarket.Services;

public class CartService
{
    private readonly UserService _userService;
    private readonly TractorService _tractorService;

    public CartService(UserService userService, TractorService tractorService)
    {
        _userService = userService;
        _tractorService = tractorService;
    }

    public void Checkout(User currentUser)
    {
        if (currentUser.IsAdmin)
        {
            AddNewlyBoughtTractorsToStock(currentUser.Cart);
            RemoveBudgetFromAdmin(currentUser);
            currentUser.Cart.Clear();
            return;
        }

        PaySeller(currentUser);
        RemoveSoldTractorsFromStock(currentUser.Cart);
        currentUser.Cart.Clear();
    }

    public static double GetTotalPrice(DeepObservableCollection<CartItem> cart)
    {
        double sum = 0;
        foreach (CartItem cartItem in cart)
        {
            double tractorPrice = cartItem.Tractor.Price;
            double cartItemPrice = tractorPrice * cartItem.Quantity;

            sum += cartItemPrice;
        }

        return sum;
    }

    public static double GetTotalAdminPrice(DeepObservableCollection<CartItem> cart)
    {
        double sum = 0;
        foreach (CartItem cartItem in cart)
        {
            double tractorPrice = cartItem.Tractor.AdminPrice;
            double cartItemPrice = tractorPrice * cartItem.Quantity;

            sum += cartItemPrice;
        }

        return sum;
    }

    private void RemoveBudgetFromAdmin(User currentUser)
    {
        double moneyFromSale = GetTotalAdminPrice(currentUser.Cart);
        currentUser.Budget -= moneyFromSale;

        _userService.UpdateUser(currentUser);
    }

    private void AddNewlyBoughtTractorsToStock(DeepObservableCollection<CartItem> cart)
    {
        foreach (CartItem cartItem in cart)
        {
            cartItem.Tractor.Stock += cartItem.Quantity;
            _tractorService.UpdateTractor(cartItem.Tractor);
        }
    }

    private void RemoveSoldTractorsFromStock(DeepObservableCollection<CartItem> cart)
    {
        foreach (CartItem cartItem in cart)
        {
            cartItem.Tractor.Stock -= cartItem.Quantity;
            _tractorService.UpdateTractor(cartItem.Tractor);
        }
    }

    private void PaySeller(User currentUser)
    {
        double moneyFromSale = GetTotalPrice(currentUser.Cart);
        User adminUser = _userService.GetAdmin();

        currentUser.Budget -= moneyFromSale;
        adminUser.Budget += moneyFromSale;

        _userService.UpdateUser(currentUser);
        _userService.UpdateUser(adminUser);
    }

    public static void AddToCart(DeepObservableCollection<CartItem> cart, CartItem cartItem)
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
