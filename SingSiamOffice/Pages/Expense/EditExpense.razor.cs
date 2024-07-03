using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace SingSiamOffice.Pages.Expense
{
    public partial class EditExpense
    {
        [Inject]
        IJSRuntime JSRuntime { get; set; }
        [Inject]
        NavigationManager navigationManager { get; set; }

        private string role { get; set; } = "employee";

        public int accountNo { get; set; }
        public int deducted { get; set; }
        public string name { get; set; }
        public string amount { get; set; }
        public string description { get; set; }

        private string accountType;
        private string[] acType =
        {
        "Alabama", "Alaska", "American Samoa", "Arizona",
        "Arkansas", "California", "Colorado", "Connecticut",

    };

        private async Task<IEnumerable<string>> SearchAccountType(string value)
        {
            // In real life use an asynchronous function for fetching data from an api.
            await Task.Delay(5);

            // if text is null or empty, show complete list
            if (string.IsNullOrEmpty(value))
                return acType;
            return acType.Where(x => x.Contains(value, StringComparison.InvariantCultureIgnoreCase));
        }

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

                navigationManager.NavigateTo("/expense-list");

            }
            else
            {
                //await JSRuntime.InvokeVoidAsync("alert_error");
            }
        }

        private void goback()
        {
            navigationManager.NavigateTo("/expense-list");
        }
    }
}
