namespace FunkoProject.Web.Models
{
    public class UserFigure
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual User OwningUserId { get; set; }
    }
}
