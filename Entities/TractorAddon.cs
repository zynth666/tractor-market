using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TractorMarket.Entities;

public class TractorAddon : ItemisableBaseEntity
{
    public override int Id { get; set; }
    public List<Tractor> Tractors { get; } = new List<Tractor>();

    [NotMapped]
    public List<string> AssociatedTractors { get; set; } = new List<string>();
    public override string Name { get; set; } = string.Empty;
    public override double Price { get; set; }
    public override int Stock { get; set; }


    [NotMapped]
    public override double AdminPrice
    {
        get
        {
            return Price * 0.65;
        }
    }

    [NotMapped]
    public int SelectedQuantity { get; set; } = 1;
}
