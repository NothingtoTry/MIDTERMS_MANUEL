using Microsoft.EntityFrameworkCore;
using PubsMvc.Models;

public class PublisherRepository : IRepository<Publisher>
{
    private readonly pubsContext _context;
    public PublisherRepository(pubsContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Publisher>> GetAllAsync()
        => await _context.Publishers.ToListAsync();

    public async Task<Publisher?> GetByIdAsync(object id)
        => await _context.Publishers.FindAsync(id);

    public async Task AddAsync(Publisher entity)
    {
        _context.Publishers.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Publisher entity)
    {
        _context.Publishers.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(object id)
    {
        var pub = await _context.Publishers.FindAsync(id);
        if (pub != null)
        {
            _context.Publishers.Remove(pub);
            await _context.SaveChangesAsync();
        }
    }
}
