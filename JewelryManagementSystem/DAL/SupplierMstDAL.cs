using JewelryManagementSystem.Interface;
using System.Data;
using System.Data.SqlClient;

namespace JewelryManagementSystem.DAL
{
    public class SupplierMstDAL : ISupplierMst
    {
        public DataTable FillSupplier(Guid p_uId)
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

        public bool AddUpdateDeleteSupplier(Guid p_uId, string p_sName, string p_sEmails, string p_sContact, string p_sAddress, string p_sMode)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@p_uId", p_uId),
                    new SqlParameter("@p_sName", p_sName),
                    new SqlParameter("@p_sEmails", p_sEmails),
                    new SqlParameter("@p_sContact", p_sContact),
                    new SqlParameter("@p_sAddress", p_sAddress),
                    new SqlParameter("@p_sMode", p_sMode)
                };

                int vReturnValue = DALHelper.ExecuteNonQuery("SupplierMst_AddUpdateDelete", parameters);
                return (vReturnValue == -1 ? false : true);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
