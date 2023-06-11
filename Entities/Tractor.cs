using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TractorMarket.Entities;

public class Tractor
{
    public int Id { get; set; }
    public List<TractorAddon> Addons { get; } = new List<TractorAddon>();
    public string Manufacturer { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public int Horsepower { get; set; }
    public int Velocity { get; set; }
    public double Price { get; set; }
    public int Vintage { get; set; }
    public int Stock { get; set; }

    [NotMapped]
    public int SelectedQuantity { get; set; } = 1;

    [NotMapped]
    public double AdminPrice
    {
        get
        {
            return Price * 0.65;
        }
    }

    public string GetDisplayName()
    {
        return Type.Replace("_", " ");
    }
}
