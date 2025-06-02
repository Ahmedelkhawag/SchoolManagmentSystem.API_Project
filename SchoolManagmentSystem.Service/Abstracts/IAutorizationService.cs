namespace SchoolManagmentSystem.Service.Abstracts
{
    public interface IAutorizationService
    {
        Task<string> AddRoleAsync(string roleName);
        Task<bool> IsRoleExist(string roleName);
    }
}
