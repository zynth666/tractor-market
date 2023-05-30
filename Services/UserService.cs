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

    public bool LoginUser(string username_in, string password_in)
    {
        try
        {
            User lookedupuser = _context.Users.Where(user => user.Name == username_in).First();
            Debug.WriteLine(lookedupuser.ToString());

            if(lookedupuser.Password == password_in )
            {
                LoggedInUser = lookedupuser;
                return true;
            }
            else
            {
                Debug.WriteLine("Password wrong");
                return false;
            }
        }
        catch
        {
            Debug.WriteLine("User not found");
            return false;
        }
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
