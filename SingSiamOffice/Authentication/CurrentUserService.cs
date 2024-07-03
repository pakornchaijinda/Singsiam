using Microsoft.AspNetCore.Components.Authorization;

namespace SingSiamOffice.Authentication
{
    public class CurrentUserService
    {
        AuthenticationState authenticationState { get; set; }

        public CurrentUserService(AuthenticationState authenticationState)
        {
            this.authenticationState = authenticationState;
        }

        public bool IsLoggedIn()
        {
            return authenticationState.User.Identity.IsAuthenticated;
        }
        public string GetCurrentUser()
        {
            return authenticationState.User.Identity.Name;
        }
    }
}
