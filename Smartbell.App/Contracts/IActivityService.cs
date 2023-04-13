namespace Smartbell.App.Contracts
{
    public interface IActivityService
    {
        Task<IEnumerable<ActivityResponseDto>> GetAsync();
        Task<ActivityResponseDto> GetByIdAsync(Guid id);
        Task<ActivityResponseDto> CreateAsync(CreateActivityDto entity);
        //Task<ActivityResponseDto> UpdateAsync(ConfigResponseDto entity);
        Task<ActivityResponseDto> DeleteAsync(ConfigResponseDto entity);
    }
}
