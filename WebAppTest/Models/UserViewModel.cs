using Microsoft.SqlServer.Server;
using System;

namespace WebAppTest.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string Nume { get; set; }
        public string Email { get; set; }
        public int? Abonament_id { get; set; }
        public int Rol { get; set; }
        public DateTime? Data_inregistrare { get; set; }
        public string Parola {get;set;}
    }
}
