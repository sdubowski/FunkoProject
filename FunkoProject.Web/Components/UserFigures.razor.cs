using FunkoProject.Web.Services;
using Microsoft.AspNetCore.Components;

namespace FunkoProject.Web.Components
{
    public class UserFiguresBase : ComponentBase
    {
        [Parameter]
        public List<Figure> Figures { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
        }
    }
}
