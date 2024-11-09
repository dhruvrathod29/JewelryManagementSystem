using JewelryManagementSystem.Areas.SupplierMst.Models;
using JewelryManagementSystem.DAL;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using JewelryManagementSystem.Interface;
using JewelryManagementSystem.Areas.CustomerMst.Models;

namespace JewelryManagementSystem.Areas.CustomerMst.Controllers
{
    [Area("CustomerMst")]
    [Route("CustomerMst/[Controller]/[action]")]
    public class CustomerMstController : Controller
	{
        #region Service
        private readonly ICustomerMst _customerService;
        #endregion

        #region Constructor
        public CustomerMstController(ICustomerMst customerService)
        {
            _customerService = customerService;
        }
        #endregion

        public IActionResult Index()
		{
			return View("CustomerMst_Index");
		}

        #region Fill Customer
        [HttpPost]
        public IActionResult FillCustomer()
        {
            try
            {
                Guid.TryParse(Request.Form["p_sId"], out Guid p_uId);
                DataTable dtCustomer = _customerService.GetAllCustomer(p_uId);
                List<CustomerMstModel> customerList = new List<CustomerMstModel>();

                if (dtCustomer != null && dtCustomer.Rows.Count > 0)
                {
                    customerList = dtCustomer
                            .Rows.Cast<DataRow>()
                            .Select(row => new CustomerMstModel
                            {
                                ID = Guid.TryParse(row["ID"].ToString(), out var guid) ? guid : Guid.Empty,
                                Name = CCommon.NullOrEmptyToString(row["NAME"]),
                                Emails = CCommon.NullOrEmptyToString(row["EMAILS"]),
                                Contact = CCommon.NullOrEmptyToString(row["CONTACT"]),
                                Address = CCommon.NullOrEmptyToString(row["ADDRESS"]),
                                CreationDate = CCommon.NullOrDefaultDateTime(row["CREATIONDATE"]),
                                ModificationDate = CCommon.NullOrDefaultDateTime(row["MODIFICATIONDATE"])
                            })
                            .ToList();
                }

                return Json(new
                {
                    CustomerMst = customerList
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = "An error occurred while retrieving supplier.",
                    error = ex.Message // Optionally include the error message for debugging
                });
            }

        }
        #endregion

        #region Add Update Customer
        [HttpPost]
        public IActionResult AddUpdateCustomer()
        {
            Guid.TryParse(Request.Form["p_sId"], out Guid p_uId);
            string p_sName = string.IsNullOrEmpty(Request.Form["p_sName"]) ? string.Empty : Request.Form["p_sName"].ToString();
            string p_sEmails = string.IsNullOrEmpty(Request.Form["p_sEmails"]) ? string.Empty : Request.Form["p_sEmails"].ToString();
            string p_sContact = string.IsNullOrEmpty(Request.Form["p_sContact"]) ? string.Empty : Request.Form["p_sContact"].ToString();
            string p_sAddress = string.IsNullOrEmpty(Request.Form["p_sAddress"]) ? string.Empty : Request.Form["p_sAddress"].ToString();
            string p_sMode = string.IsNullOrEmpty(Request.Form["p_sMode"]) ? string.Empty : Request.Form["p_sMode"].ToString();

            try
            {
                bool error = _customerService.AddUpdateDeleteCustomer(p_uId, p_sName, p_sEmails, p_sContact, p_sAddress, p_sMode);
                if (error)
                {
                    return Json(new
                    {
                        success = true,
                        message = p_sMode.ToString().ToUpper().Equals("INSERT") ? "Record has been insert successfully!" : "Record has been update successfully!",
                    });
                }
                else
                {
                    return Json(new
                    {
                        success = false,
                        message = p_sMode.ToString().ToUpper().Equals("INSERT") ? "Failed to insert record." : "Failed to update record.",
                    });
                }

            }
            catch (SqlException sqlEx) when (sqlEx.Number == 2627)
            {
                return Json(new
                {
                    success = false,
                    message = "Record already exists!",
                });
            }
            catch (Exception ex)
            {

                return Json(new
                {
                    success = false,
                    message = ex.Message,
                    error = ex.Message
                });
            }
        }
        #endregion

        #region Delete Customer
        [HttpPost]
        public IActionResult DeleteCustomer()
        {
            try
            {
                Guid.TryParse(Request.Form["p_sId"], out Guid p_uId);
                bool error = _customerService.AddUpdateDeleteCustomer(p_uId, string.Empty, string.Empty, string.Empty, string.Empty, "DELETE");

                if (error)
                {
                    return Json(new
                    {
                        success = true,
                        message = "Record has been Delete successfully!",
                    });
                }
                else
                {
                    return Json(new
                    {
                        success = false,
                        message = "Failed to Delete record.",
                    });
                }
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = ex.Message,
                });
            }
        }
        #endregion

    }
}
