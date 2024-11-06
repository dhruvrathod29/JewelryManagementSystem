using System.Data;

namespace JewelryManagementSystem.Interface
{
    public interface IProductMst
    {
        DataTable FillProduct(Guid p_uId);
    }
}
