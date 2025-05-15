namespace SchoolManagmentSystem.Core.Features.User.Queries.Result
{
    public class GetUsersListQueryResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Country { get; set; }
        public string? Address { get; set; }
    }
}
