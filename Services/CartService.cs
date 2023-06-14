using TractorMarket.Data;
using TractorMarket.Entities;
using TractorMarket.Helpers;
using TractorMarket.Models;

namespace TractorMarket.Services;

public class CartService
{
    private readonly UserService _userService;
    private readonly TractorService _tractorService;
    private readonly DataContext _dataContext;

    public CartService(UserService userService, TractorService tractorService, DataContext dataContext)
    {
        _userService = userService;
        _tractorService = tractorService;
        _dataContext = dataContext;
    }

    public void Checkout(User currentUser)
    {
        if (currentUser.IsAdmin)
        {
            AddNewlyBoughtItemsToStock(currentUser.Cart);
            RemoveBudgetFromAdmin(currentUser);
            currentUser.Cart.Clear();
            return;
        }

        PaySeller(currentUser);
        RemoveSoldItemsFromStock(currentUser.Cart);
        currentUser.Cart.Clear();
    }

    public static double GetTotalPrice(DeepObservableCollection<CartItem<ItemisableBaseEntity>> cart)
    {
        double sum = 0;
        foreach (CartItem<ItemisableBaseEntity> cartItem in cart)
        {
            double tractorPrice = cartItem.Item.Price;
            double cartItemPrice = tractorPrice * cartItem.Quantity;

            sum += cartItemPrice;
        }

        return sum;
    }

    public static double GetTotalAdminPrice(DeepObservableCollection<CartItem<ItemisableBaseEntity>> cart)
    {
        double sum = 0;
        foreach (CartItem<ItemisableBaseEntity> cartItem in cart)
        {
            double tractorPrice = cartItem.Item.AdminPrice;
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

    private void AddNewlyBoughtItemsToStock(DeepObservableCollection<CartItem<ItemisableBaseEntity>> cart)
    {
        foreach (CartItem<ItemisableBaseEntity> cartItem in cart)
        {
            cartItem.Item.Stock += cartItem.Quantity;

            if (cartItem.Item.GetType() == typeof(Tractor))
            {
                _dataContext.Set<Tractor>().Update(cartItem.Item as Tractor);
            }
            else
            {
                _dataContext.Set<TractorAddon>().Update(cartItem.Item as TractorAddon);
            }
        }
    }

    private void RemoveSoldItemsFromStock(DeepObservableCollection<CartItem<ItemisableBaseEntity>> cart)
    {
        foreach (CartItem<ItemisableBaseEntity> cartItem in cart)
        {
            cartItem.Item.Stock -= cartItem.Quantity;

            if (cartItem.Item.GetType() == typeof(Tractor))
            {
                _dataContext.Set<Tractor>().Update(cartItem.Item as Tractor);
            }
            else
            {
                _dataContext.Set<TractorAddon>().Update(cartItem.Item as TractorAddon);
            }
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

    public static void AddToCart(DeepObservableCollection<CartItem<ItemisableBaseEntity>> cart, CartItem<ItemisableBaseEntity> cartItem)
    {
        foreach (CartItem<ItemisableBaseEntity> item in cart)
        {
            if (item.Item.Name != cartItem.Item.Name)
            {
                continue;
            }

            int newItemQuantity = item.Quantity + cartItem.Quantity;

            if (newItemQuantity > item.Item.Stock)
            {
                item.Quantity = item.Item.Stock;
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
