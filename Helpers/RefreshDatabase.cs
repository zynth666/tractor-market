using app.Data;
using app.Entities;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace TractorMarket.Helpers;

public class RefreshDatabase
{
    private readonly DataContext _context;

    public RefreshDatabase(DataContext context)
    {
        _context = context;
    }
    public void Execute()
    {
        _context.Database.EnsureCreated();

        ClearDatabase();
        FillDatabase<Tractor>("Tractors.json");
        FillDatabase<User>("Users.json");
        FillTractorAddons();
    }

    private void FillTractorAddons()
    {
        List<TractorAddon> tractorAddons = GetStartDataFromJsonFile<TractorAddon>("TractorAddons.json");

        foreach (TractorAddon addon in tractorAddons)
        {
            List<Tractor> tractors = _context.Tractors
                .Where(s => addon.AssociatedTractors.Contains(s.Manufacturer))
                .ToList();

            addon.Tractors.AddRange(tractors);

            _context.TractorAddons.Add(addon);
        }

        _context.SaveChanges();
    }

    private void FillDatabase<T>(string jsonFileName) where T : class
    {
        List<T> entities = GetStartDataFromJsonFile<T>(jsonFileName);

        foreach (T entity in entities)
        {
            _context.Set<T>().Add(entity);
        }

        _context.SaveChanges();
    }

    private List<T> GetStartDataFromJsonFile<T>(string jsonFileName) where T : class
    {
        string jsonPath = Path.Combine("Data", jsonFileName);
        string json = File.ReadAllText(jsonPath);
        return JsonSerializer.Deserialize<List<T>>(json)!;
    }

    private void ClearDatabase()
    {
        _context.Users.RemoveRange(_context.Users);
        _context.Tractors.RemoveRange(_context.Tractors);
        _context.TractorAddons.RemoveRange(_context.TractorAddons);

        _context.SaveChanges();
    }
}
