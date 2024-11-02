using JewelryManagementSystem.Areas.CategoryMst.Models;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;

namespace JewelryManagementSystem.DAL
{
    public class CategoryMstDAL : ICategoryMst
    {
        public DataTable GetAllCategory()
        {
            DataTable dt = DALHelper.ExecuteStoredProcedure("CategoryMst_SelectAll");
            return dt;
        }

        //public CategoryMstModel GetCategoryById(int id)
        //{

        //}
    }
}
