
namespace Smartbell.App.Contracts
{
    public interface IRingtoneService
    {
        Task<IEnumerable<RingtoneResponseDto>> GetAsync();
        Task<RingtoneResponseDto> GetByIdAsync(Guid id);
        //Task<T> CreateAsync(T entity);
        //Task<T> UpdateAsync(T entity);
        //Task<T> DeleteAsync(T entity);
    }
}
