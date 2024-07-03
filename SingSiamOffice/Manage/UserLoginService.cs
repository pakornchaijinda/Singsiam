using SingSiamOffice.Models;

namespace SingSiamOffice.Manage
{
    public class UserLoginService
    {
        private SingSiamOffice.Models.Login _userLogin;

        public SingSiamOffice.Models.Login UserLogin
        {
            get => _userLogin;
            set => _userLogin = value;
        }
    }

   
}
