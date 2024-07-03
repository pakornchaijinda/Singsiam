using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace SingSiamOffice.Pages.CustomerManagement.CustomerDept
{
    public partial class Customerdept
    {
        [Inject]
        NavigationManager navigationManager { get; set; }
        [Inject]
        IJSRuntime JSRuntime { get; set; }


        private string role { get; set; } = "employee";
        public string depts_details { get; set; }
        public double depts_amount { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                
                await JSRuntime.InvokeVoidAsync("sideBar");
            }
        }


        private async Task submit()
        {
            var confirm = await JSRuntime.InvokeAsync<bool>("confirmSaveData");
            if (confirm)
            {

                await JSRuntime.InvokeVoidAsync("confirm");
                await Task.Delay(100);

                navigationManager.NavigateTo("/customerlist");

            }
            else
            {
                //await JSRuntime.InvokeVoidAsync("alert_error");
            }
        }
        private void goBack()
        {
            navigationManager.NavigateTo("/customerlist");
        }
    }
}
