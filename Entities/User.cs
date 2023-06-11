using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using TractorMarket.Helpers;
using TractorMarket.Models;

namespace TractorMarket.Entities;

public partial class User : ObservableObject
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    
    [ObservableProperty]
    public double _budget;
    public bool IsAdmin { get; set; }
    
    [NotMapped]
    public DeepObservableCollection<CartItem> Cart = new();
}
