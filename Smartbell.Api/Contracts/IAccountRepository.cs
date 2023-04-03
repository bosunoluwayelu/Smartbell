namespace Smartbell.Api.Contracts
{
    public interface IAccountRepository
    {
        Task<APIGenericResponse<AccountResponseDto>> CreateAsync(CreateAccountDto createAccountDto);
        Task<APIGenericResponse<AccountResponseDto>> DeleteAsync(string Id);
    }
}
