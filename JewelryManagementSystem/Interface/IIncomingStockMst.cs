using System.Data;

namespace JewelryManagementSystem.Interface
{
    public interface IIncomingStockMst
    {
        DataTable GetAllIncomingStock(Guid p_uId);
    }
}
