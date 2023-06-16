using TractorMarket.Data;
using TractorMarket.Entities;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TractorMarket.Services;

public class AddonService
{
    private readonly DataContext _context;

    public AddonService(DataContext context)
    {
        _context = context;
    }

    public List<TractorAddon> GetAllAddons()
    {
        return _context.TractorAddons
            .Where(TractorAddon => TractorAddon.Stock > 0)
            .ToList();
    }

    public List<TractorAddon> GetFilteredAddons(List<string> ManufacturerFilter) 
    {
        var filteredAddons = _context.TractorAddons
            .AsEnumerable()
            .Where(addons => addons.AssociatedTractors.Intersect(ManufacturerFilter).Count() == ManufacturerFilter.Count)
            .ToList();
            
        return filteredAddons;
    }

}
