using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCLogin.ViewModels
{
    public class EditUserViewModel
    {
            public System.Guid UserID { get; set; }
            [Required]
            [DisplayName("User Name")]
            public string UserName { get; set; }

            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Compare("Password", ErrorMessage = "Passwords do not match!")]
            [DisplayName("Confirm Password")]
            [DataType(DataType.Password)]
            public string confirmPassword { get; set; }

            public Nullable<int> Age { get; set; }
            public string Country { get; set; }
            public string Hobby { get; set; }

            [DisplayName("Date of Birth")]
            public Nullable<System.DateTime> Date { get; set; }
        }
    
}