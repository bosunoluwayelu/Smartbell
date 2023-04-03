
namespace Smartbell.App.Contracts
{
    public interface IRingtoneService
    {
        Task<IEnumerable<RingtoneResponseDto>> GetAsync();
        Task<RingtoneResponseDto> GetByIdAsync(Guid id);
    }
}
