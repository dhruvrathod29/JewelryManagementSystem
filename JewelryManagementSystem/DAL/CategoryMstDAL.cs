using JewelryManagementSystem.Areas.CategoryMst.Models;
using JewelryManagementSystem.Interface;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace JewelryManagementSystem.DAL
{
    public class CategoryMstDAL : ICategoryMst
    {
        public DataTable GetAllCategory(Guid p_uId)
        {
            DataTable dt = new DataTable();
            try
            {
                // Create parameters for the stored procedure
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@p_uId", p_uId)
                };

                dt = DALHelper.GetDataTable("CategoryMst_SelectAll", parameters);
                return dt;
            }
            catch (Exception)
            {
                return dt;
                
            }
        }
        public bool AddUpdateDeleteCategory(Guid p_uId, string p_sName ,string p_sMode)
        {
            try
            {
                // Create parameters for the stored procedure
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@p_uId", p_uId),
                    new SqlParameter("@p_sName", p_sName),
                    new SqlParameter("@p_sMode", p_sMode)
                };

                int vReturnValue = DALHelper.ExecuteNonQuery("CategoryMst_AddUpdateDelete", parameters);
                return (vReturnValue == -1 ? false : true);
            }
            catch (Exception)
            {
                return false;
            }
            
        }
    }
}
