using Microsoft.AspNetCore.Identity;

namespace Smartbell.Api.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public AccountRepository(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<APIGenericResponse<AccountResponseDto>> CreateAsync(CreateAccountDto createAccountDto)
        {
            var user = new ApplicationUser
            {
                FirstName = createAccountDto.FirstName,
                LastName = createAccountDto.LastName,
                Email = createAccountDto.Email,
                UserName = createAccountDto.Email.Split('@')[0],
                PhoneNumber = createAccountDto?.PhoneNumber,
            };

            var response = await _userManager.CreateAsync(user, createAccountDto?.Password);
            if (response.Succeeded)
            {
                return new APIGenericResponse<AccountResponseDto>
                {
                    Success = true,
                    Data = _mapper.Map<AccountResponseDto>(user),
                    Message = "User created successfully.",
                    Errors = response.Errors.Select(e => e.Description).ToList()
                };
            }
            else
            {
                return new APIGenericResponse<AccountResponseDto>
                {
                    Success = false,
                    Data = _mapper.Map<AccountResponseDto>(user),
                    Message = "Error creating user",
                    Errors = response.Errors.Select(e => e.Description).ToList()
                };
            }
        }

        public async Task<APIGenericResponse<AccountResponseDto>> DeleteAsync(string Id)
        {
            var user = await _userManager.FindByIdAsync(Id);
            if (user != null)
                await _userManager.DeleteAsync(user);

            return new APIGenericResponse<AccountResponseDto>
            {
                Success = true,
                Data = _mapper.Map<AccountResponseDto>(user),
                Message = "Error creating user",
                Errors = null
            };
        }
    }
}
