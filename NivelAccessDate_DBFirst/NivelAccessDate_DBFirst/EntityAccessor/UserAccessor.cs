using Repository_DBFirst;
using System;
using System.Linq;

public class UserAccessor
{
    private readonly photoeditEntities _context;

    public UserAccessor(photoeditEntities context)
    {
        _context = context;
    }
    public Utilizatori GetUserById(int id)
    {
        return _context.Utilizatori.FirstOrDefault(u => u.id == id);
    }
    public void AddUser(Utilizatori newUser)
    {
        _context.Utilizatori.Add(newUser);
        var existingUser = _context.Utilizatori.FirstOrDefault(u => u.email == newUser.email);
        if (existingUser != null)
        {
            throw new Exception("Un utilizator cu acest email există deja.");
        }
            _context.SaveChanges();
    }
    public void UpdateUser(Utilizatori updUser)
    {
        var existingUser = _context.Utilizatori.FirstOrDefault(u => u.id == updUser.id);
        if(existingUser !=null)
        {
            existingUser.nume = updUser.nume;
            existingUser.email = updUser.email;
            existingUser.abonament_id = updUser.abonament_id;
        }
    }
    public void DeleteUser(int id)
    {
        var UserDelete= _context.Utilizatori.FirstOrDefault(u=>u.id== id);
        if(UserDelete != null)
        {
            _context.Utilizatori.Remove(UserDelete);
            _context.SaveChanges();
        }
    }

}
