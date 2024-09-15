using FunkoProject.Web.Models;
using Microsoft.AspNetCore.Components;

namespace FunkoProject.Web.Components.Modals;

public partial class UserEditModal : ComponentBase
{
    [Parameter] public User User { get; set; }
    [Parameter] public EventCallback OnSave { get; set; }
    [Parameter] public EventCallback OnCancel { get; set; }

    private async Task HandleSave()
    {
        if (OnSave.HasDelegate)
        {
            await OnSave.InvokeAsync(null);
        }
    }

    private async Task HandleCancel()
    {
        if (OnCancel.HasDelegate)
        {
            await OnCancel.InvokeAsync(null);
        }
    }
}