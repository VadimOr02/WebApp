using Repository_DBFirst;
using System;
using System.Linq;

public class AbonamentAccessor
{
    private readonly photoeditEntities _context;

    public AbonamentAccessor(photoeditEntities context)
    {
        _context = context;
    }
    public Abonamente GetAbonamentById(int id)
    {
        return _context.Abonamente.FirstOrDefault(u => u.id == id);
    }
    public void AddAbonament(Abonamente newAbonament)
    {
        _context.Abonamente.Add(newAbonament);
        var existing = _context.Abonamente.FirstOrDefault(u => u.nume == newAbonament.nume);
        if (existing != null)
        {
            throw new Exception("Exista deja abonament cu acest nume.");
        }
        _context.SaveChanges();
    }
    public void UpdateAbonament(Abonamente updAbonament) 
    {
        var existing = _context.Abonamente.FirstOrDefault(u => u.id == updAbonament.id);
        if (existing != null)
        {
            existing.nume = updAbonament.nume;
            existing.pret = updAbonament.pret;
            existing.durata = updAbonament.durata;
            existing.beneficii = updAbonament.beneficii;
        }
    }
    public void DeleteAbonament(int id)
    {
        var AbonamentDelete = _context.Abonamente.FirstOrDefault(u => u.id == id);
        if (AbonamentDelete != null)
        {
            _context.Abonamente.Remove(AbonamentDelete);
            _context.SaveChanges();
        }
    }

}
