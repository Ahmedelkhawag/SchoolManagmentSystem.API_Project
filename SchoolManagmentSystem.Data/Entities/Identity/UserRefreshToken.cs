using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagmentSystem.Data.Entities.Identity
{
    public class UserRefreshToken
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("User")]
        [InverseProperty("UserRefreshTokens")]
        public int UserId { get; set; }
        public string? JwtId { get; set; }
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
        public bool IsUsed { get; set; }
        public bool IsRevoked { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DateTime AddTime { get; set; }
        public virtual ApplicationUser? User { get; set; }

    }
}
