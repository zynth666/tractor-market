using System.Collections.Generic;

namespace app.Entities;

public class Tractor
{
    public int Id { get; set; }
    public List<TractorAddon> Addons { get; } = new List<TractorAddon>();
    public string Manufacturer { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public int Horsepower { get; set; }
    public int Velocity { get; set; }
    public int Price { get; set; }
    public int Vintage { get; set; }
    public int Stock { get; set; }
}
