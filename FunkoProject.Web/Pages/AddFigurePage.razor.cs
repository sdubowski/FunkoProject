using FunkoProject.Web.Models;
using FunkoProject.Web.Models.ViewModels;
using FunkoProject.Web.Services;
using Microsoft.AspNetCore.Components;
using System.Linq.Expressions;

namespace FunkoProject.Web.Pages
{
    public class AddFigurePageBase : ComponentBase
    {
        public UserFigureViewModel userFigure = new UserFigureViewModel();
        private User user = new User();
        public string? successMessage;

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

        private User GetCurrentUser()
        {
            return new User { /* Populate current user data here */ };
        }

    }
}
