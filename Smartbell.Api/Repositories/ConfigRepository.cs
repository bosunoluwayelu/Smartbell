namespace Smartbell.Api.Repositories
{
    public class ConfigRepository : GenericRepository<Config>, IConfigRepository
    {
        public ConfigRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
