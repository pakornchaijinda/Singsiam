using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;
using MudBlazor;
using SingSiamOffice.Models;

namespace SingSiamOffice.Pages.BlackList
{
    public partial class Blacklist_List
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


        bool isOpen;

        bool _expanded = true;

        private void OnExpandCollapseClick()
        {
            _expanded = !_expanded;
        }


        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                
                await JSRuntime.InvokeVoidAsync("sideBar");
            }
        }


        private string[] search_category = {
            "เลขบัตรประจำตัวประชาชน",
            "ชื่อ - นามสกุล",
            "เบอร์โทร",


};


        private string value1;
        private string[] states =
        {
        "Alabama", "Alaska", "American Samoa", "Arizona",
        "Arkansas", "California", "Colorado", "Connecticut",

    };




     

        private bool viewBlacklist;
        SingSiamOffice.Models.BlackList to_view = new SingSiamOffice.Models.BlackList();
        private void ViewBlacklist(int id)
        {
            viewBlacklist = true;
            to_view = list_black.Where(s => s.BlackId == id).FirstOrDefault();


        }
        // void Submit() => editBlacklist = false;

        private DialogOptions dialogOptions = new() { MaxWidth = MaxWidth.Medium, FullWidth = true, CloseButton = true };


        private void AddBlacklist()
        {
            navigationManager.NavigateTo("/add-blacklist/"+b_id.ToString());
        }
        private void EditBlacklist(int id)
        {
            navigationManager.NavigateTo("/edit-blacklist/"+id.ToString());
        }
        async Task<bool> remove(int id)
        {
            try
            {
                var remove = list_black.Where(s => s.BlackId == id).FirstOrDefault();
                db.Remove(remove);
                await db.SaveChangesAsync();
                return true;
            }catch (Exception ex) { return false;
                 }
        }
        private async Task delete(int id)
        {
            var confirm = await JSRuntime.InvokeAsync<bool>("confirmdelete");
            if (confirm)
            {
                if (await remove(id))
                {
                    await JSRuntime.InvokeVoidAsync("deletesuccess");
                    await Task.Delay(100);
                    list_black = db.BlackLists.Include(s => s.Customer).ToList();

                }
                else
                {
                    await JSRuntime.InvokeVoidAsync("delete_error");
                }
               

            }
            else
            {
                //
            }
        }

    }
}
