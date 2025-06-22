using FunkoProject.Web.Services;
using Microsoft.AspNetCore.Components;

namespace FunkoProject.Web.Shared;

public class NavMenuBase : LayoutComponentBase
{
    private bool _collapseNavMenu = false;

    protected string NavMenuCssClass => "collapse show";

    public void ToggleNavMenu()
    {
        // Nic nie robimy, menu zawsze otwarte
    }
}
