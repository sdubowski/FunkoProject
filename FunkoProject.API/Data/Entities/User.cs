namespace FunkoProject.Data.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Nationality { get; set; }
        public string? PasswordHash { get; set; }
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
        public virtual ICollection<UserFigure> UserFigures { get; set; } = new List<UserFigure>();
        public virtual ICollection<UserFriend> Friends { get; set; }
        public virtual ICollection<UserFriend> FriendOf { get; set; }
        public virtual ICollection<Attachment> Attachments { get; set; } = new List<Attachment>();

        public User()
        {
            Friends = new HashSet<UserFriend>();
            FriendOf = new HashSet<UserFriend>();
        }
    }
}
