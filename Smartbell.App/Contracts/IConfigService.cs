namespace Smartbell.App.Contracts
{
    public interface IConfigService
    {
        Task<IEnumerable<ConfigResponseDto>> GetAsync();
        Task<ConfigResponseDto> GetByIdAsync(Guid id);
        Task<ConfigResponseDto> CreateAsync(CreateConfigDto entity);
        Task<ConfigResponseDto> UpdateAsync(ConfigResponseDto entity);
        Task<ConfigResponseDto> DeleteAsync(ConfigResponseDto entity);
    }
}
