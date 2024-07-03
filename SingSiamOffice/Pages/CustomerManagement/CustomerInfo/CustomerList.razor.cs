using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using SingSiamOffice.Authentication;
using SingSiamOffice.Manage;
using SingSiamOffice.Models;
using System;
using static SingSiamOffice.Pages.CustomerManagement.CustomerInfo.AddCustomer;

namespace SingSiamOffice.Pages.CustomerManagement.CustomerInfo
{
    public partial class CustomerList
    {
        [Inject]
        IJSRuntime JSRuntime { get; set; }
        [Inject]
        NavigationManager navigationManager { get; set; }
        [Inject]
        Manage.Managements managements { get; set; }
        [Inject] AuthenticationStateProvider authStateProvider { get; set; }    
        [Inject] AuthenticationStateProvider AuthenticationStateProvider { get; set; }
       
        //[CascadingParameter]
        //private Task<AuthenticationState> authenticationStateTasks { get; set; }
        //AuthenticationState auth;
        private string role { get; set; } = "employee";

        bool isOpen;

        bool _expanded = false;

        private void OnExpandCollapseClick()
        {
            _expanded = !_expanded;
        }


        private string value1;
        private string[] states =
        {
        "Alabama", "Alaska", "American Samoa", "Arizona",
        "Arkansas", "California", "Colorado", "Connecticut",

    };


        private List<Promise> lst_Promises = new List<Promise>();


        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                var authenticationState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
                var user = authenticationState.User;


                if (user.Identity.IsAuthenticated)
                {
                    list_customer = await managements.GetCustomerbyBranch(b_id);
                }
                await JSRuntime.InvokeVoidAsync("sideBar");
            }
        }
        protected override async void OnInitialized()
        {
            
                list_customer = await managements.GetCustomerbyBranch(b_id);
            
        }


        private string[] search_category = {
            "เลขบัตรประจำตัวประชาชน",
            "ชื่อ - นามสกุล",
            "เบอร์โทร",

};


        private async Task deleteContract(int Contract)
        {
            var confirm = await JSRuntime.InvokeAsync<bool>("deleteContract");
            if (confirm)
            {

                await JSRuntime.InvokeVoidAsync("deleteContractSuccess");
                await Task.Delay(100);

                navigationManager.NavigateTo("/customerlist");

            }
            else
            {
                //await JSRuntime.InvokeVoidAsync("alert_error");
            }
        }


        private void addCustomer(int brach_id)
        {
            navigationManager.NavigateTo("/add-customer/" + brach_id.ToString()) ;
        }
        private void editCustomer(int cid)
        {
            navigationManager.NavigateTo("/edit-customer/"+b_id.ToString()+"/"+ cid.ToString());
        }

        private void addContract(int cus_id,int branch_id)
        {
            navigationManager.NavigateTo($"/add-contract/{cus_id}/{branch_id}");
        }
        private void viewContract(int cus_id,int promise_id,int branch_id)
        {
            navigationManager.NavigateTo($"/view-contract/{branch_id}/{cus_id}/{promise_id}");
        }

        private void goBlacklist(int brach_id)
        {
            navigationManager.NavigateTo("/blacklist_list");
        }
        private void goDeptConlection(int brach_id)
        {
            navigationManager.NavigateTo("/deptcollection-list");
        }
        private void goCustomerDept(int cus_id)
        {
            navigationManager.NavigateTo("/customerdept/");
        }
        private void goPayment(int cus_id, int promise_id, int branch_id)
        {
            navigationManager.NavigateTo($"/paymentlist/{branch_id}/{cus_id}/{promise_id}");
        }
    }
}
