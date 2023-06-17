using TractorMarket.Data;
using TractorMarket.Entities;
using TractorMarket.Helpers;
using TractorMarket.Models;

namespace TractorMarket.Services;

/// <summary>
/// Contains business logic for handling shopping cart related actions.
/// </summary>
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

    /// <summary>
    /// Does a checkout of the current users cart and handles admins/normal users accordingly.
    /// </summary>
    /// <param name="currentUser">The currently logged in user.</param>
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

    /// <summary>
    /// Gets the total sum of all item prices in the given cart.
    /// </summary>
    /// <param name="cart">A collection of CartItems.</param>
    /// <returns>The total sum of all prices.</returns>
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

    /// <summary>
    /// Gets the total sum of all item prices for an admin in the given cart.
    /// </summary>
    /// <param name="cart">A collection of CartItems.</param>
    /// <returns>The total sum of all prices.</returns>
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

    /// <summary>
    /// Removes budget from admin's account after doing a checkout.
    /// </summary>
    /// <param name="admin">Admin user.</param>
    private void RemoveBudgetFromAdmin(User admin)
    {
        double moneyFromSale = GetTotalAdminPrice(admin.Cart);
        admin.Budget -= moneyFromSale;

        _userService.UpdateUser(admin);
    }

    /// <summary>
    /// Increases the stock of items that were bought by the admin and updates the values in the database.
    /// </summary>
    /// <param name="cart">Admin shopping cart.</param>
    private void AddNewlyBoughtItemsToStock(DeepObservableCollection<CartItem<ItemisableBaseEntity>> cart)
    {
        foreach (CartItem<ItemisableBaseEntity> cartItem in cart)
        {
            cartItem.Item.Stock += cartItem.Quantity;
            UpdateCartItemByItemType(cartItem);
        }
    }

    /// <summary>
    /// Decreases the stock of items that were bought by a user and updates the values in the database.
    /// </summary>
    /// <param name="cart">User shopping cart.</param>
    private void RemoveSoldItemsFromStock(DeepObservableCollection<CartItem<ItemisableBaseEntity>> cart)
    {
        foreach (CartItem<ItemisableBaseEntity> cartItem in cart)
        {
            cartItem.Item.Stock -= cartItem.Quantity;
            UpdateCartItemByItemType(cartItem);
        }
    }

    /// <summary>
    /// Updates a CartItem's specific Item, i.e. Tractor or TractorAddon.
    /// This is achieved by getting the type at runtime and comapring it to the desired type.
    /// </summary>
    /// <param name="cartItem">The CartItem to update.</param>
    private void UpdateCartItemByItemType(CartItem<ItemisableBaseEntity> cartItem)
    {
        if (cartItem.Item.GetType() == typeof(Tractor))
        {
            _dataContext.Set<Tractor>().Update(cartItem.Item as Tractor);
        }
        else
        {
            _dataContext.Set<TractorAddon>().Update(cartItem.Item as TractorAddon);
        }
    }

    /// <summary>
    /// Transfers the money spent by the user to the admin and updates the values in the database..
    /// </summary>
    /// <param name="currentUser">The currently logged in user.</param>
    private void PaySeller(User currentUser)
    {
        double moneyFromSale = GetTotalPrice(currentUser.Cart);
        User adminUser = _userService.GetAdmin();

        currentUser.Budget -= moneyFromSale;
        adminUser.Budget += moneyFromSale;

        _userService.UpdateUser(currentUser);
        _userService.UpdateUser(adminUser);
    }

    /// <summary>
    /// Adds the given CartItem to the given cart.
    /// If the item that is being added already exists in the cart, the existing item's quantity is increased instead.
    /// </summary>
    /// <param name="cart">A collection of CartItems.</param>
    /// <param name="cartItem">A CartItem.</param>
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
