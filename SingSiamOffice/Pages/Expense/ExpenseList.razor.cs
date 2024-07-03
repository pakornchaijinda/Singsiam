using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
using SingSiamOffice.Models;
using Microsoft.EntityFrameworkCore;
namespace SingSiamOffice.Pages.Expense
{
    public partial class ExpenseList
    {
        [Inject]
        IJSRuntime JSRuntime { get; set; }
        [Inject]
        NavigationManager navigationManager { get; set; }


        DateTime? date = null;

        private string role { get; set; } = "employee";

        public int overdueDate { get; set; }

        public string idcard { get; set; }
        public string name { get; set; }
        public string tel { get; set; }
        public string description { get; set; }

        bool isOpen;

        bool _expanded = true;

        private void OnExpandCollapseClick()
        {
            _expanded = !_expanded;
        }



        private bool viewExpense;
        TransactionHistory to_view = new TransactionHistory();
        private async void ViewExpense(int id)
        {

            to_view = db.TransactionHistories.Include(s => s.Subject).Include(s=>s.Login).Where(x => x.TransactionId == id).FirstOrDefault();
            viewExpense = true;

        }


        private DialogOptions dialogOptions = new() { MaxWidth = MaxWidth.Medium, FullWidth = true, CloseButton = true };



        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                
                await JSRuntime.InvokeVoidAsync("sideBar");
         
            }
        }

        private string accountType;
        private string[] acType =
        {
        "Alabama", "Alaska", "American Samoa", "Arizona",
        "Arkansas", "California", "Colorado", "Connecticut",

    };

        private async Task<IEnumerable<SubjectCost>> SearchSubjectCoste(string value)
        {
            // In real life use an asynchronous function for fetching data from an api.
            await Task.Delay(5);

            // if text is null or empty, show complete list
            if (string.IsNullOrEmpty(value))
                return list_subject;
            return list_subject.Where(x => x.SubjectName.Contains(value, StringComparison.InvariantCultureIgnoreCase) || x.SubjectCode.Contains(value, StringComparison.InvariantCultureIgnoreCase));
        }

        private async Task delete(int id)
        {
            var confirm = await JSRuntime.InvokeAsync<bool>("confirmdelete");
            if (confirm)
            {

                await JSRuntime.InvokeVoidAsync("deletesuccess");
                await Task.Delay(100);

                navigationManager.NavigateTo("/expense-list");

            }
            else
            {
                //await JSRuntime.InvokeVoidAsync("delete_error");
            }
        }


        private void AddExpense(int id)
        {
            navigationManager.NavigateTo("/add-expense/"+ id.ToString());
        }
        private void EditExpense(int id )
        {
            navigationManager.NavigateTo("/edit-expense/" + id.ToString());
        }
      

    }
}
