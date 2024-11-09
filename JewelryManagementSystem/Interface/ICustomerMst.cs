using System.Data;

namespace JewelryManagementSystem.Interface
{
    public interface ICustomerMst
    {
        DataTable GetAllCustomer(Guid p_uId);

        bool AddUpdateDeleteCustomer(Guid p_uId, string p_sName, string p_sEmails, string p_sContact, string p_sAddress, string p_sMode);
    }
}
