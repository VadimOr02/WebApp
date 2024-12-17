using BusinessLayer.Models;
using Repository_DBFirst;
using System.Collections.Generic;
using System.Data.Entity;

namespace BusinessLayer
{
    public interface IFotografiiService
    {
        void AddFotografie(FotografiiViewModel fotografieViewModel);
        List<FotografiiViewModel> Fotografii();
        Fotografii GetFotografieById(int id);
        void EditFotografie(Fotografii updatedFotografie);
        void DeleteFotografie(int id);
        List<FotografiiViewModel> GetFotografiiByUserId(int userId);
        void SoftDeleteFotografie(int id);

    }
}