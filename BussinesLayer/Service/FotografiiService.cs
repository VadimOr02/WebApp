using Repository_DBFirst;
using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLayer.Models;
using System.Web.Caching;
using NLog;

namespace BusinessLayer
{
    public class FotografiiService : IFotografiiService
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        private readonly IPhotoeditEntities _context;
        private readonly ICache _cacheManager;

        public FotografiiService(IPhotoeditEntities context, ICache cacheManager)
        {
            _context = context;
            _cacheManager = cacheManager;
        }

        public List<FotografiiViewModel> Fotografii()
        {
            return _context.Fotografii
         .Where(f => !f.IsDeleted)  // Filtream doar fotografiile care nu sunt șterse
         .Select(f => new FotografiiViewModel
         {
             Id = f.id,
             Utilizator_Id = f.utilizator_id,
             Nume_Fisier = f.nume_fisier,
             Cale = f.cale,
             Data_Incarcare = f.data_incarcare,
             Dimensiune = f.dimensiune,
             Format = f.format,
             IsDeleted = f.IsDeleted
         }).ToList();
        }
        // Obține toate fotografiile unui utilizator
        public List<FotografiiViewModel> GetFotografiiByUserId(int userId)
        {
            string cacheKey = "fotografii_user_" + userId;
            if (_cacheManager.IsSet(cacheKey))
            {
                return _cacheManager.Get<List<FotografiiViewModel>>(cacheKey);
            }

            var fotografii = _context.Fotografii
             .Where(f => f.utilizator_id == userId && !f.IsDeleted)
             .Select(f => new FotografiiViewModel
             {
                 Id = f.id,
                 Utilizator_Id = f.utilizator_id,
                 Nume_Fisier = f.nume_fisier,
                 Cale = f.cale,
                 Data_Incarcare = f.data_incarcare,
                 Dimensiune = f.dimensiune,
                 Format = f.format,
                 IsDeleted = f.IsDeleted
             })
             .ToList();
            _cacheManager.Set(cacheKey, fotografii);
            return fotografii;
        }

        // Adaugă o fotografie
        public void AddFotografie(FotografiiViewModel fotografieViewModel)
        {
            try { 
            logger.Info("Adding a new fotografie: {Name}", fotografieViewModel.Nume_Fisier);
            var fotografie = new Fotografii
            {
                utilizator_id = fotografieViewModel.Utilizator_Id,
                nume_fisier = fotografieViewModel.Nume_Fisier,
                cale = fotografieViewModel.Cale,
                data_incarcare = DateTime.Now,
                dimensiune = fotografieViewModel.Dimensiune,
                format = fotografieViewModel.Format
            };

            _context.Fotografii.Add(fotografie);
            _context.SaveChanges();
            logger.Info("Fotografie {Name} added successfully.", fotografieViewModel.Nume_Fisier);

            _cacheManager.Remove("fotografii_user_" + fotografieViewModel.Utilizator_Id);
        }
            catch (Exception ex)
            {
                logger.Error(ex, "Error occurred while adding fotografie.");
            }
        }

        // Găsește o fotografie după ID
        public Fotografii GetFotografieById(int id)
        {
            return _context.Fotografii
                .FirstOrDefault(f => f.id == id && !f.IsDeleted);  // Filtrare pentru a exclude fotografiile șterse logic
        }

        // Editează o fotografie
        public void EditFotografie(Fotografii updatedFotografie)
        {
            var existingFotografie = _context.Fotografii.Find(updatedFotografie.id);
            if (existingFotografie == null) return;

            existingFotografie.nume_fisier = updatedFotografie.nume_fisier;
            existingFotografie.cale = updatedFotografie.cale;
            existingFotografie.format = updatedFotografie.format;
            existingFotografie.dimensiune = updatedFotografie.dimensiune;

            _context.SaveChanges();

            _cacheManager.Remove("fotografii_user_" + existingFotografie.utilizator_id);
        }
        public void SoftDeleteFotografie(int id)
        {
            try { 
            logger.Info("Attempting to delete fotografie with ID {Id}.", id);
            var fotografie = _context.Fotografii.Find(id);
            if (fotografie != null)
            {
                fotografie.IsDeleted = true;
                _context.SaveChanges();

                _cacheManager.Remove("fotografii_user_" + fotografie.utilizator_id);
            }
            else
            {
                logger.Warn("Fotografie with ID {Id} not found.", id);
                return;
            }
            logger.Info("Fotografie with ID {Id} deleted successfully.", id);
        }catch (Exception ex)
    {
        logger.Error(ex, "Error occurred while deleting fotografie with ID {Id}.", id);
    }
}
        public void DeleteFotografie(int id)
        {
            var fotografie = _context.Fotografii.Find(id);
            if (fotografie != null)
            {
                _context.Fotografii.Remove(fotografie);
                _context.SaveChanges();

                _cacheManager.Remove("fotografii_user_" + fotografie.utilizator_id);
            }
        }
    }
}