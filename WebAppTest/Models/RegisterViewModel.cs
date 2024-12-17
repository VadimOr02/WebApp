using System;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebAppTest.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Numele este obligatoriu")]
        public string Nume { get; set; }

        [Required(ErrorMessage = "Email-ul este obligatoriu")]
        [EmailAddress(ErrorMessage = "Email invalid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Parola este obligatorie")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Parola trebuie să aibă minim 6 caractere")]
        public string Parola { get; set; }

        [Required(ErrorMessage = "Confirmarea parolei este obligatorie")]
        [DataType(DataType.Password)]
        [Compare("Parola", ErrorMessage = "Parolele nu se potrivesc")]
        public string ConfirmParola { get; set; }
    }
}