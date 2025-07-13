using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using SchoolManagmentSystem.Data.Entities.Identity;
using SchoolManagmentSystem.Infrastructure.InfrastructureBases.Abstract;
using SchoolManagmentSystem.Service.Abstracts;

namespace SchoolManagmentSystem.Service.Implmentations
{
    public class UserService : IUserService
    {
        #region Fields
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailService _emailService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IGenericRepositoryAsync<ApplicationUser> _userRepository;


        #endregion


        #region ctor

        public UserService(UserManager<ApplicationUser> userManager, IEmailService emailService, IHttpContextAccessor httpContextAccessor, IGenericRepositoryAsync<ApplicationUser> userRepository)
        {
            _userManager = userManager;
            _emailService = emailService;
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
        }

        #endregion


        #region implementations

        public async Task<string> RegisterUserAync(ApplicationUser user, string password)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User cannot be null");
            }
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Password cannot be null or empty", nameof(password));
            }

            using (var transaction = _userRepository.BeginTransaction())
            {
                try
                {

                    var result = await _userManager.CreateAsync(user, password);

                    if (!result.Succeeded)
                    {
                        throw new InvalidOperationException(string.Join(", ", result.Errors.Select(e => e.Description)));
                    }

                    await _userManager.AddToRoleAsync(user, "User");
                    await transaction.CommitAsync();
                }

                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception($"An error occurred while registering the user: {ex.Message}", ex);

                }
            }
            try
            {
                var confirmationCode = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var confirmationLink = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}/Api/V1/Authentication/ConfirmEmail?userId={user.Id}&code={Uri.EscapeDataString(confirmationCode)}";
                var emailContent = $"Please confirm your email by clicking this link: <a href='{confirmationLink}'>Confirm Email</a>";

                await _emailService.SendEmailAsync(user.Email, "Elkhawaga", emailContent, "Confirm your email");

                return $"User {user.UserName} registered successfully. Please check your email to confirm your account.";
            }
            catch (Exception ex)
            {

                return $"User {user.UserName} registered successfully, but we failed to send the confirmation email. Please try the 'Resend Email' feature.";
            }
        }
        #endregion
    }
}
