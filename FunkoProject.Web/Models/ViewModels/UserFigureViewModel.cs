using FunkoProject.Web.Models.ViewModels.Attributes;
using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;

namespace FunkoProject.Web.Models.ViewModels
{
    public class UserFigureViewModel
    {
        [Required(ErrorMessage = "Nazwa jest wymagane")]
        public string FigureName { get; set; }
        [Required(ErrorMessage = "Opis jest wymagane")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Seria jest wymagana")]
        public string SeriesName { get; set; }
        [Required(ErrorMessage = "Cena jest wymagana")]
        [Range(0.01, 10000.00, ErrorMessage = "Cena musi być większa od zera i nie większa niż 10 000")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Zdjęcie jest wymagane")]
        [MaxFileSize(2 * 1024 * 1024, ErrorMessage = "Maksymalny rozmiar pliku to 2 MB")]
        [AllowedExtensions(new string[] { ".jpg", ".jpeg", ".png" }, ErrorMessage = "Dozwolone formaty to: .jpg, .jpeg, .png")]
        public IBrowserFile Image { get; set; }
        public int UserId { get; set; }
    }
}
