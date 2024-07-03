using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;
using MudBlazor;
using SingSiamOffice.Models;
using System.Net.Sockets;

namespace SingSiamOffice.Pages.EmployeeManagement
{
    public partial class EmployeeManagementList
    {
        [Inject]
        NavigationManager navigationManager { get; set; }
        [Inject]
        IJSRuntime JSRuntime { get; set; }

        private string role { get; set; } = "admin";

        bool isOpen;

        bool _expanded = true;

        private void OnExpandCollapseClick()
        {
            _expanded = !_expanded;
        }

        private string[] searchCategorys = {
            "รหัสพนักงาน",
            "ตำแหน่ง",
            "รหัสพนักงาน",
            "ชื่อ - นามสกุล",
            "สาขา"
        };


   

        private async Task<IEnumerable<Login>> SearchName(string value)
        {
            // In real life use an asynchronous function for fetching data from an api.
            await Task.Delay(5);

            // if text is null or empty, show complete list
            if (string.IsNullOrEmpty(value))
                return List_employ;
            return List_employ.Where(x => x.Fullname.Contains(value, StringComparison.InvariantCultureIgnoreCase));
        }
        private async Task<IEnumerable<Role>> SearchRole(string value)
        {
            // In real life use an asynchronous function for fetching data from an api.
            await Task.Delay(5);

            // if text is null or empty, show complete list
            if (string.IsNullOrEmpty(value))
                return List_Role;
            return List_Role.Where(x => x.RoleName.Contains(value, StringComparison.InvariantCultureIgnoreCase));
        }
        private async Task<IEnumerable<Branch>> SearchBranch(string value)
        {
            // In real life use an asynchronous function for fetching data from an api.
            await Task.Delay(5);

            // if text is null or empty, show complete list
            if (string.IsNullOrEmpty(value))
                return List_Branch;
            return List_Branch.Where(x => x.BranchName.Contains(value, StringComparison.InvariantCultureIgnoreCase));
        }

        List<Branch> List_Branch = new List<Branch>();
        List<Role> List_Role = new List<Role>();
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
               
                
                await JSRuntime.InvokeVoidAsync("sideBar");
                
            }
        }

        SingsiamdbContext db = new SingsiamdbContext();
        List<Login> List_employ = new List<Login>();
        protected override async Task OnInitializedAsync()
        {
            List_employ = db.Logins.Include(s => s.Branch).AsNoTracking().ToList();
            List_Branch = db.Branches.AsNoTracking().ToList();
            List_Role  = db.Roles.AsNoTracking().ToList();  
        }
        private void addEmployee()
        {
            navigationManager.NavigateTo("/add-employee");
        }
        private void editEmployee(int id)
        {
            navigationManager.NavigateTo("/edit-employee/"+id.ToString());
        }
        private void viewEmployee(int id)
        {
            navigationManager.NavigateTo("/view-employee/"+id.ToString());
        }



        private async Task delete(int id)
        {
            var confirm = await JSRuntime.InvokeAsync<bool>("confirmdelete");
            if (confirm)
            {
                var to_remove = db.Logins.Where(s => s.Id == id).FirstOrDefault();
                db.Logins.Remove(to_remove);
                db.SaveChangesAsync();
                await JSRuntime.InvokeVoidAsync("deletesuccess");
                await Task.Delay(100);
                clearSearch();
                navigationManager.NavigateTo("/employee-list");

            }
            else
            {
                await JSRuntime.InvokeVoidAsync("delete_error");
            }
        }




        private async void SearchData()
        {
            List_employ = List_employ.Where(f =>
            (selectRole != null ? f.RoleId == selectRole.Id : true) && (selectBranch != null ? f.BranchId == selectBranch.Id : true) &&
              (selectLogin != null ? f.Id == selectLogin.Id : true)).ToList();


        }
        private async Task clearSearch()
        {
            List_employ = db.Logins.Include(s => s.Branch).AsNoTracking().ToList();
            await Task.Delay(100);
        }


    }
}
