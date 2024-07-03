using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using SingSiamOffice.Manage;
using Microsoft.AspNetCore.Components.Authorization;
using SingSiamOffice.Models.Authentication;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace SingSiamOffice.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient client;
        private readonly AuthenticationStateProvider authStateProvider;
        private readonly ILocalStorageService localStorage;
        private Models.SingsiamdbContext db = new Models.SingsiamdbContext();

        UserAccountService UserAccountService = new UserAccountService();
        public AuthenticationService(HttpClient client, AuthenticationStateProvider authStateProvider, ILocalStorageService localStorage)
        {
            this.client = client;
            this.authStateProvider = authStateProvider;
            this.localStorage = localStorage;
        }

        public async Task<AuthenticatedUserModel> Login(AuthenticationUserModel userForAuthentication)
        {
            //var data = new FormUrlEncodedContent(new[]
            //{
            //    new KeyValuePair<string, string>("grant_Type", "password"),
            //    new KeyValuePair<string, string>("username", userForAuthentication.UserName),
            //    new KeyValuePair<string, string>("password", userForAuthentication.Password)
            //});
            var userinfo = await UserAccountService.GetByUserName(userForAuthentication.UserName,userForAuthentication.Password);
          
            if (userinfo != null)
            {
                await localStorage.SetItemAsync("authToken", userinfo.Username);
                var customAuthStateProvider = (CustomAuthenticationStateProvider)authStateProvider;
                await customAuthStateProvider.UpdateAuthenticationState(new UserSession
                {
                    UserName = userinfo.Username,
                    Role = userinfo.Role.RoleName,
                    BranchId = (int)userinfo.BranchId
                });
                AuthenticatedUserModel userModel = new AuthenticatedUserModel
                {
                    Username = userinfo.Username,
                    RoleName = userinfo.Role.RoleName,
                    BranchId= (int)userinfo.BranchId    
                };

                return userModel;
            }
            else 
            {
                return null;
            }
          

           
          
        }

        public async Task Logout()
        {
            await localStorage.RemoveItemAsync("authToken");
            var customAuthStateProvider = (CustomAuthenticationStateProvider)authStateProvider;
            await customAuthStateProvider.UpdateAuthenticationState(null);
            client.DefaultRequestHeaders.Authorization = null;
        
        }
    }
}
