
using Microsoft.EntityFrameworkCore;
using System.Net.Sockets;
using System.Numerics;
using System.Xml.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using static MudBlazor.Colors;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SingSiamOffice.Models;
using SingSiamOffice.Manage;
using Microsoft.AspNetCore.Mvc;


namespace SingSiamOffice.Manage
{
    public class UserManagement
    {
        public SingsiamdbContext db  = new SingsiamdbContext();
        private List<Role> list_role =  new List<Role>();
        private List<Login> list_all_userlogin = new List<Login>();
        private Login get_userlogin = new Login();
      
        public string generateSalt(int maxSize = 10)
        {
            char[] chars = new char[62];
            chars =
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            byte[] data = new byte[1];
            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetNonZeroBytes(data);
                data = new byte[maxSize];
                crypto.GetNonZeroBytes(data);
            }
            StringBuilder result = new StringBuilder(maxSize);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }
            return result.ToString();
        }
        public string hashPassword(string inputPassword, string salt)
        {
            byte[] buffer = Encoding.UTF8.GetBytes($"{inputPassword}{salt}");
            byte[] key = Encoding.UTF8.GetBytes(salt);
            HMACSHA256 hmac = new HMACSHA256(key);
            string hash = Convert.ToBase64String(hmac.ComputeHash(buffer)).Replace("-", "");
            return hash;
        }
        public async Task<List<Role>> List_all_role() 
        {
            list_role = db.Roles.AsNoTracking().Where(s=>s.IsActive == true).ToList();
            return list_role;
        }
        public async Task<List<Login>> List_all_user()
        {
            List<Login> list_user = new List<Login>();
            list_user = db.Logins.Include(s=>s.Branch).Include(s => s.Role).AsNoTracking().ToList();
            return list_user;
        }
      
        public async Task<Login> GetUserLogin(Login input) 
        {
            get_userlogin = db.Logins.Include(s=>s.Role).Where(s=>s.Username == input.Username && s.Dob == input.Dob).FirstOrDefault(); // 
            return get_userlogin;
        }
        public async Task<int> GetUserId(string usernaem)
        {
           var data = db.Logins.Include(s => s.Role).Where(s => s.Username == usernaem).FirstOrDefault().Id; // 
            return data;
        }
        public async Task<bool> CheckUserLoginActive(UserLogin input)
        {
            var data = db.Logins.Where(s => s.Username == input.UserName).Select(s=>s.IsActive).FirstOrDefault();
            return data;
        }
        public async Task<bool> CheckUserLoginPass(UserLogin input)
        {
            var data = db.Logins.Where(s => s.Username == input.UserName).FirstOrDefault();
            if (data == null)
            {
                return false;//ไม่มี user นี้
            }
            string hashedPWD = hashPassword(input.Password, data.Salt);
            if (hashedPWD == data.Password)
            {
                return true;
            }
            else
            {
                return false;//password ผิด
            }
        }
        public async Task<bool> Add_UserLogin(Login input)
        {
            var Salt = generateSalt();
            try 
            {
                Login login_ = new Login
                {
                    Username = input.Username,
                    Salt = Salt,
                    Password = hashPassword(input.Password, Salt),
                    Fullname = input.Fullname,
                    Email = input.Email,
                    RoleId = input.RoleId,
                    IsActive = true,
                    CreatedAt = DateTime.Now,
                    Phone = input.Phone,
                    Dob = input.Dob,
                    BranchId = input.BranchId,
                    EmNickname = input.EmNickname,
                    Code = input.Code,
                    Img = input.Img,
                    Address = input.Address,

                   

                };
                db.Logins.Add(login_);
                await db.SaveChangesAsync ();

                return true;
            }catch(Exception ex) { return false; }
       

        }
        public async Task<bool> Edit_UserLogin(Login input)
        {
            var to_Edit = db.Logins.Where(s=>s.Id == input.Id).FirstOrDefault();
            if (to_Edit != null)
            {
                try
                {
                    to_Edit.Username = input.Username;
                    to_Edit.Password = hashPassword(input.Password, to_Edit.Salt);
                    to_Edit.Fullname= input.Fullname;
                    to_Edit.Email = input.Email;
                    to_Edit.RoleId = input.RoleId;
                    to_Edit.IsActive = input.IsActive;
                    to_Edit.CreatedAt = DateTime.Now;
                    to_Edit.Phone = input.Phone;
                    to_Edit.Dob = input.Dob;
                    to_Edit.BranchId = input.BranchId;
                    to_Edit.EmNickname = input.EmNickname;
                    to_Edit.Img = input.Img; 
                    to_Edit.Address = input.Address;
                   

                    db.Entry(to_Edit).State = EntityState.Modified;
                    await db.SaveChangesAsync();

                    return true;
                }
                catch (Exception ex) { return false; }
            }
            else { return false; }
        }
        public async Task<bool> Del_UserLogin(Login input)
        {
            var to_Edit = db.Logins.Where(s => s.Id == input.Id).FirstOrDefault();
            if (to_Edit != null)
            {
                try
                {
                   
                    to_Edit.IsActive = false;
                   
                    db.Entry(to_Edit).State = EntityState.Modified;
                    await db.SaveChangesAsync();

                    return true;
                }
                catch (Exception ex) { return false; }
            }
            else { return false; }
        }
        public async Task<bool> Changepassword_UserLogin(Login input)
        {
            return true;
        }
        public async Task<bool> AddEventLog(SingSiamOffice.Models.EventLog toAdd)
        {
            try
            {
                db.EventLogs.Add(toAdd);
                await db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }

        }

    }
}
