using FunkoProject.Web.Models;
using Microsoft.AspNetCore.Components;

namespace FunkoProject.Web.Components.FigureComponents
{
    public class FigureCardBase : ComponentBase
    {
        [Parameter]
        public Figure figure { get; set; }
    }
}
