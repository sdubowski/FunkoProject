using FunkoProject.Web.Components.FigureComponents;
using FunkoProject.Web.Models;
using FunkoProject.Web.Services;
using Microsoft.AspNetCore.Components;

namespace FunkoProject.Web.Components
{
    public class UserFiguresBase : ComponentBase
    {
        [Inject]
        protected IFiguresService figuresService {  get; set; }
        [Parameter]
        public List<Figure> Figures { get; set; }
        public UserFiguresBase() { }
        protected override async Task OnInitializedAsync()
        {
            base.OnInitializedAsync();
        }
    }
}
