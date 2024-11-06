using System.Data;

namespace JewelryManagementSystem.Interface
{
    public interface IProductMst
    {
        DataTable FillProduct(Guid p_uId);

        bool AddUpdateDeleteProduct(Guid p_uId, string p_sName, Guid p_uCategoryId, int p_iPrice, string p_sDescription, string p_sMode);

    }
}
