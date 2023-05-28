using app.Data;
using app.Entities;
using System.Collections.Generic;
using System.Linq;

namespace app.Services;

public class UserService
{
    private DataContext _context;

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
