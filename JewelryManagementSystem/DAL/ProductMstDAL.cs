using JewelryManagementSystem.Interface;
using System;
using System.Data;
using System.Data.SqlClient;

namespace JewelryManagementSystem.DAL
{
	public class ProductMstDAL : IProductMst
    {
        public DataTable GetAllProduct(Guid p_uId)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@p_uId", p_uId)
                };

                DataTable dt = DALHelper.GetDataTable("ProductMst_SelectAll", parameters);
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

				if (ds != null && ds.Tables.Count > 0)
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
						if (ds.Tables[3].Rows.Count > 0)
						{
							ds.Tables[3].TableName = "dtCustomer";
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

		public DataTable GetAllAvailableStock()
		{
			try
			{
				DataTable dt = DALHelper.GetDataTable("AvailableStockMst_SelectAll");
				return dt;

			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}
