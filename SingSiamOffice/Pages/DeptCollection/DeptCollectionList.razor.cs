using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace SingSiamOffice.Pages.DeptCollection
{
    public partial class DeptCollectionList
    {
        [Inject]
        NavigationManager navigationManager { get; set; }
        [Inject]
        IJSRuntime JSRuntime { get; set; }

        public int overdueDate { get; set; }

        private string role { get; set; } = "employee";

        public bool pay_check { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                
                await JSRuntime.InvokeVoidAsync("sideBar");
              
            }
        }


        bool _expanded = true;

        private void OnExpandCollapseClick()
        {
            _expanded = !_expanded;
        }

    }
}
