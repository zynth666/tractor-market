using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TractorMarket.Entities;

public class TractorAddon
{
    public int Id { get; set; }
    public List<Tractor> Tractors { get; } = new List<Tractor>();

    [NotMapped]
    public List<string> AssociatedTractors { get; set; } = new List<string>();
    public string Name { get; set; } = string.Empty;
    public int Price { get; set; }
    public int Stock { get; set; }
}
