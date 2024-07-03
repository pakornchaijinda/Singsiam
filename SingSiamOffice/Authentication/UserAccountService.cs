using SingSiamOffice.Models;
using Microsoft.EntityFrameworkCore;

namespace SingSiamOffice.Authentication
{
    public class UserAccountService
    {
        //private List<UserAccount> _users;

        //public UserAccountService()
        //{
        //    _users = new List<UserAccount>
        //    {
        //        new UserAccount{ UserName = "admin", Password = "admin", Role = "Administrator" },
        //        new UserAccount{ UserName = "user", Password = "user", Role = "User" }
        //    };
        //}

        //public UserAccount? GetByUserName(string userName)
        //{
        //    return _users.FirstOrDefault(x => x.UserName == userName);
        //}

        Manage.Hasher hasher = new Manage.Hasher();

        private List<Login> _users;
        private Login _login;
        private SingsiamdbContext db = new SingsiamdbContext();
        public UserAccountService()
        {

            _users = db.Logins.AsNoTracking().Include(s => s.Role).Include(s => s.Branch).ToList();
        }

        public async Task<Login> GetByUserName(string userName, string passWord)
        {
            if (await checkUserPassword(userName, passWord) == false)
            {
                return null;
            }
            else
            {
                return db.Logins.Where(x => x.Username == userName).AsNoTracking().Include(s => s.Role).Include(s => s.Branch).FirstOrDefault();
            }

        }
        public async Task<bool> checkUserPassword(string username, string password)
        {
        
            _login = db.Logins.Where(u => u.Username == username && u.IsActive == true).AsNoTracking().FirstOrDefault();
            if (_login == null)
            {
                return false;//ไม่มี user นี้
            }
            string hashedPWD = hasher.hashPassword(password, _login.Salt);
            if (hashedPWD == _login.Password)
            {
                return true;
            }
            else
            {
                return false;//password ผิด
            }
        }
    }
}
