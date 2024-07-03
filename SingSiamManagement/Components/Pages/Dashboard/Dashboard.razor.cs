using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace SingSiamManagement.Components.Pages.Dashboard
{
    public partial class Dashboard
    {
        [Inject]
        IJSRuntime JSRuntime { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await JSRuntime.InvokeVoidAsync("toTopScreen");
                await JSRuntime.InvokeVoidAsync("sideBar");
            }
        }
    }
}
