using Microsoft.EntityFrameworkCore;
using PubsMvc.Models;

public class TitleRepository : IRepository<Title>
{
    private readonly pubsContext _context;
    public TitleRepository(pubsContext context) => _context = context;

    public async Task<IEnumerable<Title>> GetAllAsync()
        => await _context.Titles.Include(t => t.Pub).ToListAsync();

    public async Task<Title?> GetByIdAsync(object id)
        => await _context.Titles.FindAsync(id);

    public async Task AddAsync(Title entity)
    {
        _context.Titles.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Title entity)
    {
        _context.Titles.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(object id)
    {
        var t = await _context.Titles.FindAsync(id);
        if (t != null)
        {
            _context.Titles.Remove(t);
            await _context.SaveChangesAsync();
        }
    }
}
