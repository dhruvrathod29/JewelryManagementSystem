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

                dt = DALHelper.GetDataTable("SupplierMst_SelectAll", parameters);
                return dt;

            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
