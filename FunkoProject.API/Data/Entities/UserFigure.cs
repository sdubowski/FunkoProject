namespace FunkoProject.Data.Entities
{
    public class UserFigure
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public virtual User OwningUser { get; set; }
    }
}
