using TractorMarket.Data;
using TractorMarket.Entities;
using System.Collections.Generic;
using System.Linq;

namespace TractorMarket.Services;

public class TractorService
{
    private readonly DataContext _context;

    public TractorService(DataContext context)
    {
        _context = context;
    }

    public List<Tractor> GetTractorsForCustomers()
    {
        return _context.Tractors
            .Where(tractor => tractor.Stock > 0)
            .ToList();
    }

    public List<Tractor> GetFilteredTractorsForCustomers(int min_eur_in, int max_eur_in, int min_kmh_in, int max_kmh_in, int min_ps_in, int max_ps_in, int min_year_in, int max_year_in)
    {
        return _context.Tractors
            .Where(tractor => tractor.Stock > 0 && min_eur_in <= tractor.Price && max_eur_in >= tractor.Price && min_kmh_in <= tractor.Velocity && max_kmh_in >= tractor.Velocity && min_ps_in <= tractor.Horsepower && max_ps_in >= tractor.Horsepower && min_year_in <= tractor.Vintage && max_year_in >= tractor.Vintage)
            .ToList();
    }


    public List<Tractor> GetTractorsForAdmin()
    {
        return _context.Tractors
            .ToList();
    }

    public void UpdateTractor(Tractor tractor)
    {
        _context.Tractors.Update(tractor);
        _context.SaveChanges();
    }
}
