using TractorMarket.Data;
using TractorMarket.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace TractorMarket.Services;

public class UserService
{
    private DataContext _context;
    public static User? LoggedInUser;

    public UserService(DataContext context)
    {
        _context = context;
    }

    public List<User> GetUsers()
    {
        return _context.Users
            .Where(user => !user.IsAdmin)
            .ToList();
    }

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

    public User GetAdmin()
    {
        return _context.Users
            .Where(user => user.IsAdmin)
            .First();
    }

    public void UpdateUser(User user)
    {
        _context.Users.Update(user);
        _context.SaveChanges();
    }
}
