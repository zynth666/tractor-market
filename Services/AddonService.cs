using TractorMarket.Data;
using TractorMarket.Entities;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TractorMarket.Services;
/// <summary>
/// Service for working with TractorAddon data.
/// </summary>
public class AddonService
{
    private readonly DataContext _context;

    public AddonService(DataContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Gets all TractorAddons from the database that are in stock.
    /// </summary>
    /// <returns>A list of TractorAddons.</returns>
    public List<TractorAddon> GetAllAddons()
    {
        return _context.TractorAddons
            .Where(TractorAddon => TractorAddon.Stock > 0)
            .ToList();
    }

    /// <summary>
    /// Gets a filtered list of TractorAddons that are in Stock.
    /// Checks if a list of Manufacturers is contained inside of the Addons Associated Tractors.
    /// </summary>
    /// <param name="ManufacturerFilter">A list of Manufacturer names, i.e. "Claas".</param>
    /// <returns>A list of TractorAddons.</returns>
    public List<TractorAddon> GetFilteredAddons(List<string> ManufacturerFilter) 
    {
        var filteredAddons = _context.TractorAddons
            .AsEnumerable()
            .Where(addons => addons.AssociatedTractors.Intersect(ManufacturerFilter).Count() == ManufacturerFilter.Count)
            .Where(addons => addons.Stock > 0)
            .ToList();
            
        return filteredAddons;
    }

}
