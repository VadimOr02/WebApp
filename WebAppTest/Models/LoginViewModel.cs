using System;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebAppTest.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email-ul este obligatoriu")]
        [EmailAddress(ErrorMessage = "Email invalid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Parola este obligatorie")]
        [DataType(DataType.Password)]
        public string Parola { get; set; }
    }
}