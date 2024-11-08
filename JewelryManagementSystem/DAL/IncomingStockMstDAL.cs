using JewelryManagementSystem.Interface;
using System;
using System.Data;
using System.Data.SqlClient;

namespace JewelryManagementSystem.DAL
{
    public class IncomingStockMstDAL : IIncomingStockMst
    {
        public DataTable GetAllIncomingStock(Guid p_uId)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@p_uId", p_uId)
                };

                DataTable dt = DALHelper.GetDataTable("IncomingStockMst_SelectAll", parameters);
                return dt;

            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet ddlFillProduct(Guid p_uId)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@p_uId", p_uId)
                };

                DataSet ds = DALHelper.GetDataSet("ProductMst_DropDown_SelectAll", parameters);

                if (ds != null && ds.Tables.Count > 0 )
                {
                    if (p_uId == Guid.Empty)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            ds.Tables[0].TableName = "dtCategory";
                        }
                        if (ds.Tables[1].Rows.Count > 0)
                        {
                            ds.Tables[1].TableName = "dtSupplier";
                        }
                        if (ds.Tables[2].Rows.Count > 0)
                        {
                            ds.Tables[2].TableName = "dtProduct";
                        }
                    }
                    else
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            ds.Tables[0].TableName = "dtProduct";
                        }
                    }
                }
                
                return ds;

            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool AddUpdateIncomingStock(Guid p_uId, Guid p_uProductId, Guid p_uSupplierId, int p_iQuantity, DateTime p_dReceivedDate, string p_sMode)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@p_uId", p_uId),
                    new SqlParameter("@p_uProductId", p_uProductId),
                    new SqlParameter("@p_uSupplierId", p_uSupplierId),
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
