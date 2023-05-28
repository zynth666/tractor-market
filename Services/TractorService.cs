using app.Data;
using app.Entities;
using System.Collections.Generic;
using System.Linq;

namespace app.Services;

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

    public List<Tractor> GetTractorsForAdmin()
    {
        return _context.Tractors
            .Where(tractor => tractor.Stock == 0)
            .ToList();
    }

    public void UpdateTractor(Tractor tractor)
    {
        _context.Tractors.Update(tractor);
        _context.SaveChanges();
    }
}
