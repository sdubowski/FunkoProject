using System.ComponentModel.DataAnnotations;

namespace FunkoProject.Web.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Imię jest wymagane")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Nazwisko jest wymagane")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Mail jest wymagane")]
        [EmailAddress(ErrorMessage = "Nie poprawny adres email")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Hasło jest wymagane")]
        [MinLength(6, ErrorMessage = "Hasło musi mieć co najmniej 6 znaków")]
        public string Password { get; set; }
        
        [Compare("Password", ErrorMessage = "Hasła nie są zgodne")]
        public string ConfirmPassword { get; set; }
        public string Nationality { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int RoleId { get; set; }
    }
}
