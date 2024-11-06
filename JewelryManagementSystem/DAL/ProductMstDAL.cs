using JewelryManagementSystem.Interface;
using System.Data;
using System.Data.SqlClient;

namespace JewelryManagementSystem.DAL
{
    public class ProductMstDAL : IProductMst
    {
        public DataTable FillProduct(Guid p_uId)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@p_uId", p_uId)
                };

                dt = DALHelper.GetDataTable("ProductMst_SelectAll", parameters);
                return dt;

            }
            catch (Exception)
            {
                throw;
            }

        }

        public bool AddUpdateDeleteProduct(Guid p_uId, string p_sName, Guid p_uCategoryId, int p_iPrice, string p_sDescription, string p_sMode)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@p_uId", p_uId),
                    new SqlParameter("@p_sName", p_sName),
                    new SqlParameter("@p_uCategoryId", p_uCategoryId),
                    new SqlParameter("@p_iPrice", p_iPrice),
                    new SqlParameter("@p_sDescription", p_sDescription),
                    new SqlParameter("@p_sMode", p_sMode)
                };

                int vReturnValue = DALHelper.ExecuteNonQuery("ProductMst_AddUpdateDelete", parameters);
                return (vReturnValue == -1 ? false : true);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
