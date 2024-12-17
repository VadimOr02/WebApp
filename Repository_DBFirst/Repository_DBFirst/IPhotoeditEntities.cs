using Repository_DBFirst;
using System.Linq;
using System.Data.Entity;
using System;

namespace Repository_DBFirst
{
    public interface IPhotoeditEntities : IDisposable
    {
        DbSet<Fotografii> Fotografii { get; set; }
        DbSet<Abonamente> Abonamente { get; set; }
        DbSet<Edits> Edits { get; set; }
        DbSet<Utilizatori> Utilizatori { get; set; }
        int SaveChanges();  // Trebuie să existe o metodă SaveChanges pentru a salva modificările în baza de date
    }
}