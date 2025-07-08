using FunkoProject.Web.Models;
using FunkoProject.Web.Models.ViewModels;
using FunkoProject.Web.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Linq;
using System.Linq.Expressions;

namespace FunkoProject.Web.Pages
{
    public class AddFigurePageBase : ComponentBase
    {
        public UserFigureViewModel userFigure = new UserFigureViewModel();
        private User user = new User();
        public string? successMessage;
        protected string? uploadError;
        protected string? imagePreviewUrl;

        [Inject]
        private IUserService _userService {  get; set; }
        [Inject]
        private IFiguresService _figuresService { get; set; }

        protected override async void OnInitialized()
        {
            user = await _userService.GetUserAsync();
        }

        public void HandleValidSubmit()
        {
            var figuretoAdd = userFigure;
            figuretoAdd.UserId = user.Id;
            _figuresService.RegisterFigureForUser(figuretoAdd);
            successMessage = "Form submitted successfully!";
        }

        public async Task OnInputFileChange(InputFileChangeEventArgs e)
        {
            var file = e.File;

            if (file.Size > 2 * 1024 * 1024)
            {
                uploadError = "Plik jest zbyt duży (max 2 MB).";
                userFigure.Image = null;
                imagePreviewUrl = null;
                return;
            }

            var extension = Path.GetExtension(file.Name).ToLowerInvariant();
            if (!new[] { ".jpg", ".jpeg", ".png" }.Contains(extension))
            {
                uploadError = "Dozwolone formaty to: .jpg, .jpeg, .png.";
                userFigure.Image = null;
                imagePreviewUrl = null;
                return;
            }

            uploadError = null;
            userFigure.Image = file;

            using var stream = file.OpenReadStream(2 * 1024 * 1024);
            using var ms = new MemoryStream();
            await stream.CopyToAsync(ms);
            var base64 = Convert.ToBase64String(ms.ToArray());
            imagePreviewUrl = $"data:{file.ContentType};base64,{base64}";
        }

        private User GetCurrentUser()
        {
            return new User { /* Populate current user data here */ };
        }

    }
}
