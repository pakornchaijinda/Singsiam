using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;
using MudBlazor;

namespace SingSiamOffice.Pages.BranchManagement
{
    public partial class Branch_Info
    {
        [Inject]
        NavigationManager navigationManager { get; set; }
        [Inject]
        IJSRuntime JSRuntime { get; set; }

        private string role { get; set; } = "admin";
        public string province { get; set; }
        public string branch_name { get; set; }
        public string branch_code { get; set; }
        public string branch_account { get; set; }
        public string branch_bank { get; set; }
        public string branch_tel { get; set; }
        public string branch_description { get; set; }
        public string branch_map { get; set; }
        public string branch_address { get; set; }

        private bool transferEmployee;
        private int transferEmployee_id;
        private void TransferEmployee(int id)
        {
            transferEmployee_id = id;
            transferEmployee = true;
        }
        async void move()
        {
            transferEmployee = false;
            if (await Move_em())
            {
             
                list_em = db.Logins.Where(s => s.BranchId == b_id).ToList();

                await JSRuntime.InvokeVoidAsync("confirm");
                await Task.Delay(100);

              
            }
            else
            {
                await JSRuntime.InvokeVoidAsync("alert_error");
            }

          
        }
        async Task<bool> Move_em()
        {
            try
            {
                var to_edit = db.Logins.Where(s => s.Id == transferEmployee_id).FirstOrDefault();
                to_edit.BranchId = selectBranch.Id;
                db.Entry(to_edit).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }

        }
        private DialogOptions dialogOptions = new() { MaxWidth = MaxWidth.Small, FullWidth = true, CloseButton = true };


        private string branchs;
        private string[] branchlists =
        {
        "1001 | สาขาเชียงใหม่", "1002 | สาขาลำพูน", "1003 | สาขาดอนเมือง", "1004 | สาขาตลาดไทย",


    };

       


        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                
                await JSRuntime.InvokeVoidAsync("sideBar");
            }
        }


        private void viewBranch(int brach_id)
        {
           
            navigationManager.NavigateTo("/customerlist/"+ brach_id.ToString());
        }

    }
}
