using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;

namespace SingSiamOffice.Pages.BlackList
{
    public partial class EditBlackList
    {
        [Inject]
        IJSRuntime JSRuntime { get; set; }
        [Inject]
        NavigationManager navigationManager { get; set; }


        private string role { get; set; } = "employee";


        public string idcard { get; set; }
        public string name { get; set; }
        public string tel { get; set; }
        public string description { get; set; }


        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                
                await JSRuntime.InvokeVoidAsync("sideBar");
            }
        }


        async Task<bool> save()
        {
            try
            {

                db.Entry(edit_black).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private async Task submit()
        {
            var confirm = await JSRuntime.InvokeAsync<bool>("confirmSaveData");
            if (confirm)
            {

              
                edit_black.CustomerId = selectCus.BranchId;
               edit_black.CreateTime = DateTime.Now;
             
                if (await save())
                {
                    await JSRuntime.InvokeVoidAsync("confirm");
                    await Task.Delay(100);
                    navigationManager.NavigateTo("/blacklist_list/" + b_id.ToString());

                }
                else
                {
                    await JSRuntime.InvokeVoidAsync("alert_error");
                }

            }
            else
            {
                //await JSRuntime.InvokeVoidAsync("alert_error");
            }
        }

        private void goback()
        {
            navigationManager.NavigateTo("/blacklist_list/" + edit_black.BranchId.ToString()) ;
        }
    }
}
