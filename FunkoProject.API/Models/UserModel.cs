using FunkoProject.Data.Entities;

namespace FunkoProject.Models;

public class UserModel
{
    public int Id { get; set; }
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? Nationality { get; set; }
    public int RoleId { get; set; }
    public Role? Role {get;set;}
    public List<UserModel> Friends {get;set;} = new();
}