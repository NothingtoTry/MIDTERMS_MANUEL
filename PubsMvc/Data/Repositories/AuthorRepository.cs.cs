using Microsoft.EntityFrameworkCore;
using PubsMvc.Models;

public class AuthorRepository : IRepository<Author>
{
    private readonly pubsContext _context;
    public AuthorRepository(pubsContext context) => _context = context;

    public async Task<IEnumerable<Author>> GetAllAsync()
        => await _context.Authors.ToListAsync();

    public async Task<Author?> GetByIdAsync(object id)
        => await _context.Authors.FindAsync(id);

    public async Task AddAsync(Author entity)
    {
        _context.Authors.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Author entity)
    {
        _context.Authors.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(object id)
    {
        var a = await _context.Authors.FindAsync(id);
        if (a != null)
        {
            _context.Authors.Remove(a);
            await _context.SaveChangesAsync();
        }
    }
}
