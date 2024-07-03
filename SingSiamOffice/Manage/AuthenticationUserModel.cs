using System.ComponentModel.DataAnnotations;

namespace SingSiamOffice.Manage
{
    public class AuthenticationUserModel
    {
    
        [Required(ErrorMessage = "Need Username")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Need Password")]
        public string Password { get; set; }
    }
}
