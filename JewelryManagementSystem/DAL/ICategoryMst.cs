using JewelryManagementSystem.Areas.CategoryMst.Models;
using System.Data;

namespace JewelryManagementSystem.DAL
{
    public interface ICategoryMst
    {
        DataTable GetAllCategory();

       //CategoryMstModel GetCategoryById(int id);
    }
}
