using JewelryManagementSystem.Areas.CategoryMst.Models;
using System.Data;

namespace JewelryManagementSystem.DAL
{
    public interface ICategoryMst
    {
        DataTable GetAllCategory(Guid p_uId);
        bool AddUpdateCategory(Guid p_uId, string p_sName ,string p_sMode);

    }
}
