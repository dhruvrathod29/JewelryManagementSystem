using System.Data;

namespace JewelryManagementSystem.Interface
{
    public interface ICategoryMst
    {
        DataTable GetAllCategory(Guid p_uId);
        bool AddUpdateDeleteCategory(Guid p_uId, string p_sName, string p_sMode);

    }
}
