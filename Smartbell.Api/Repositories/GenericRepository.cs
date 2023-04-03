


namespace Smartbell.Api.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, IEntity
    {
        private readonly ApplicationDbContext _context;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<T> CreateAsync(T entity)
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
            return await GetByIdAsync(entity.Id);
        }

        public async Task<T> DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<T>> GetAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T> UpdateAsync(T entity)
        {
            //_context.Entry(entity).State = EntityState.Modified;
            _context.Set<T>().Update(entity);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConfigExists(entity.Id))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }

            //return await GetByIdAsync(entity.Id);
            return entity;
        }

        private bool ConfigExists(Guid id)
        {
            //return (_context.Configs?.Any(e => e.Id == id)).GetValueOrDefault();
            return (_context.Set<T>()?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
