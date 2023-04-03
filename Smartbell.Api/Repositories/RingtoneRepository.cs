namespace Smartbell.Api.Repositories
{
    public class RingtoneRepository : GenericRepository<Ringtone>, IRingtoneRepository
    {
        public RingtoneRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
