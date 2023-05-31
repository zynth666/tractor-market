using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using TractorMarket.Models;

namespace TractorMarket.Entities;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public long Budget { get; set; }
    public bool IsAdmin { get; set; }
    
    [NotMapped]
    public List<CartItem> Cart = new();
}
