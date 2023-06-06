﻿using System.Collections.ObjectModel;
using TractorMarket.Entities;
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

    public void Checkout(ObservableCollection<CartItem> cart, User currentUser)
    {
        PaySeller(currentUser);
        RemoveSoldTractorsFromStock(cart);
        cart.Clear();
    }

    public long GetTotalPrice(ObservableCollection<CartItem> cart)
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

    public void RemoveSoldTractorsFromStock(ObservableCollection<CartItem> cart)
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
}
