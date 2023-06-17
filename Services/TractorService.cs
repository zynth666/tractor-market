using TractorMarket.Data;
using TractorMarket.Entities;
using System.Collections.Generic;
using System.Linq;

namespace TractorMarket.Services;

/// <summary>
/// Service for handling Tractor related data and business logic.
/// </summary>
public class TractorService
{

    private readonly DataContext _context;

    public TractorService(DataContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Gets a list of all Tractors that are in stock.
    /// </summary>
    /// <returns>A list of Tractors.</returns>
    public List<Tractor> GetTractorsForCustomers()
    {
        return _context.Tractors
            .Where(tractor => tractor.Stock > 0)
            .ToList();
    }

    /// <summary>
    /// Gets a filtered list of all tractors that are in stock.
    /// </summary>
    /// <param name="min_eur_in">Minimum price.</param>
    /// <param name="max_eur_in">Maximum price.</param>
    /// <param name="min_kmh_in">Minimum velocity.</param>
    /// <param name="max_kmh_in">Maximum velocity.</param>
    /// <param name="min_ps_in">Minimum horsepower.</param>
    /// <param name="max_ps_in">Maximum horsepower.</param>
    /// <param name="min_year_in">Minimum vintage.</param>
    /// <param name="max_year_in">Maximum vintage.</param>
    /// <returns>A filtered list of Tractors.</returns>
    public List<Tractor> GetFilteredTractorsForCustomers(int min_eur_in, int max_eur_in, int min_kmh_in, int max_kmh_in, int min_ps_in, int max_ps_in, int min_year_in, int max_year_in)
    {
        return _context.Tractors
            .Where(tractor => tractor.Stock > 0 && min_eur_in <= tractor.Price && max_eur_in >= tractor.Price && min_kmh_in <= tractor.Velocity && max_kmh_in >= tractor.Velocity && min_ps_in <= tractor.Horsepower && max_ps_in >= tractor.Horsepower && min_year_in <= tractor.Vintage && max_year_in >= tractor.Vintage)
            .ToList();
    }

    public List<Tractor> GetFilteredTractorsForAdmin(int min_eur_in, int max_eur_in, int min_kmh_in, int max_kmh_in, int min_ps_in, int max_ps_in, int min_year_in, int max_year_in)
    {
        return _context.Tractors
            .Where(tractor => min_eur_in <= tractor.Price && max_eur_in >= tractor.Price && min_kmh_in <= tractor.Velocity && max_kmh_in >= tractor.Velocity && min_ps_in <= tractor.Horsepower && max_ps_in >= tractor.Horsepower && min_year_in <= tractor.Vintage && max_year_in >= tractor.Vintage)
            .ToList();
    }



    public List<Tractor> GetTractorsForAdmin()
    {
        return _context.Tractors
            .ToList();
    }

    /// <summary>
    /// Takes a Tractor entity and updates it in the database.
    /// </summary>
    /// <param name="tractor">Tractor to update.</param>
    public void UpdateTractor(Tractor tractor)
    {
        _context.Tractors.Update(tractor);
        _context.SaveChanges();
    }
}
