using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingSiamOffice.Model
{
    public partial class Login
    {
        [NotMapped]
        public string? oldPassword { get; set; }
        [NotMapped]
        public string? newPassword { get; set; }
        [NotMapped]
        public string? reNewPassword { get; set; }

    }
    

   
}
