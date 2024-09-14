using FunkoProject.Web.Services;
using Microsoft.AspNetCore.Components;

namespace FunkoProject.Web.Shared;

public class NavMenuBase : LayoutComponentBase
{
    [Inject] private IAuthService _authService { get; set; }

    public async Task Logout()
    {
        await _authService.Logout();
    }

    private bool _collapseNavMenu = true;

    protected string? NavMenuCssClass => _collapseNavMenu ? "collapse" : null;

    public void ToggleNavMenu()
    {
        _collapseNavMenu = !_collapseNavMenu;
    }
}