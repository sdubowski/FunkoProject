using System.ComponentModel.DataAnnotations;

namespace FunkoProject.Web.Models.ViewModels
{
    public class UserFigureViewModel
    {
        [Required(ErrorMessage = "Nazwa jest wymagane")]
        public string FigureName { get; set; }
        [Required(ErrorMessage = "Opis jest wymagane")]
        public string Description { get; set; }
        public int UserId { get; set; }
    }
}
