using TractorMarket.Data;
using TractorMarket.Entities;
using System.Linq;

namespace TractorMarket.Services;

/// <summary>
/// Service for handling User related business logic, like login and register etc.
/// </summary>
public class UserService
{
    private DataContext _context;
    public static User? LoggedInUser;

    public UserService(DataContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Performs a login with the given credentials.
    /// </summary>
    /// <param name="username">Username.</param>
    /// <param name="password">Password.</param>
    /// <returns>True, if login worked, false otherwise.</returns>
    public bool LoginUser(string username, string password)
    {
        try
        {
            User user = _context.Users.Where(user => user.Name == username).First();

            if (user.Password == password) {
                LoggedInUser = user;
                return true;
            } else {
                return false;
            }
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Performs a register with the given credentials.
    /// Creates a new user and logs that user right in.
    /// </summary>
    /// <param name="username">Username.</param>
    /// <param name="password">Password.</param>
    /// <param name="budget">Initial budget.</param>
    public void RegisterUser(string username, string password, int budget)
    {
        User user = new User();
        user.Name = username;
        user.Password = password;
        user.Budget = budget;
        user.IsAdmin = false;

        _context.Users.Add(user);
        _context.SaveChanges();

        LoggedInUser = user;
    }

    /// <summary>
    /// Gets the admin User.
    /// </summary>
    /// <returns>The admin User.</returns>
    public User GetAdmin()
    {
        return _context.Users
            .Where(user => user.IsAdmin)
            .First();
    }

    /// <summary>
    /// Updates the given User entity in the database.
    /// </summary>
    /// <param name="user">User to update.</param>
    public void UpdateUser(User user)
    {
        _context.Users.Update(user);
        _context.SaveChanges();
    }
}
