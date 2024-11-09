using System.Data;

namespace JewelryManagementSystem.Interface
{
    public interface IIncomingStockMst
    {
        DataTable GetAllIncomingStock(Guid p_uId);
        bool AddUpdateIncomingStock(Guid p_uId, Guid p_uProductId, Guid p_uSupplierId, int p_iQuantity, DateTime p_dReceivedDate, string p_sMode);
    }
}
