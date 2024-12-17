using System;
using System.Web;

namespace BusinessLayer.Models
{
    [Serializable]
    public class FotografiiViewModel
    {
        public int Id { get; set; }
        public int Utilizator_Id { get; set; }
        public string Nume_Fisier { get; set; }
        public string Cale { get; set; }
        public DateTime? Data_Incarcare { get; set; }
        public int Dimensiune { get; set; }
        public string Format { get; set; }
        public bool IsDeleted { get; set; }
}


}
