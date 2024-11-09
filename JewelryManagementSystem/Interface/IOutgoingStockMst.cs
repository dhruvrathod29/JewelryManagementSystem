using System.Data;

namespace JewelryManagementSystem.Interface
{
    public interface IOutgoingStockMst
    {
        DataTable GetAllOutgoingStock(Guid p_uId);
        bool AddUpdateOutgoingStock(Guid p_uId, Guid p_uProductId, Guid p_uCustomerId, int p_iQuantity, DateTime p_dSoldDate, string p_sMode);
    }
}
