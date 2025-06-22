using FunkoProject.Web.Services;
using Microsoft.AspNetCore.Components;

namespace FunkoProject.Web.Components
{
    public class UserFiguresBase : ComponentBase
    {
        List<Figure> figures = new List<Figure>();
        [Inject]
        private IFiguresService _figuresService { get; set; }
        [Parameter] public int UserId { get; set; }
        protected override async Task OnInitializedAsync()
        {
            var figuresTest = await _figuresService.GetUserFiguresAsync(UserId);
            if (figures == null)
            {
                await base.OnInitializedAsync();
            }
            else
            {
                figures = figuresTest.ToList();
            }
        }
    }
}
