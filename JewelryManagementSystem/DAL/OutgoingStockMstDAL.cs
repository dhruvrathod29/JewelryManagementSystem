using System.Data.SqlClient;
using System.Data;
using JewelryManagementSystem.Interface;

namespace JewelryManagementSystem.DAL
{
    public class OutgoingStockMstDAL : IOutgoingStockMst
    {
        public DataTable GetAllOutgoingStock(Guid p_uId)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@p_uId", p_uId)
                };

                DataTable dt = DALHelper.GetDataTable("OutgoingStockMst_SelectAll", parameters);
                return dt;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool AddUpdateOutgoingStock(Guid p_uId, Guid p_uProductId, Guid p_uCustomerId, int p_iQuantity, DateTime p_dReceivedDate, string p_sMode)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@p_uId", p_uId),
                    new SqlParameter("@p_uProductId", p_uProductId),
                    new SqlParameter("@p_uCustomerId", p_uCustomerId),
                    new SqlParameter("@p_iQuantity", p_iQuantity),
                    new SqlParameter("@p_dReceivedDate", p_dReceivedDate),
                    new SqlParameter("@p_sMode", p_sMode)
                };

                int vReturnValue = DALHelper.ExecuteNonQuery("IncomingStockMst_AddUpdateDelete", parameters);
                return (vReturnValue == -1 ? false : true);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
