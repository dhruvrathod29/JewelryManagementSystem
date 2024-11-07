using JewelryManagementSystem.Areas.CategoryMst.Models;
using JewelryManagementSystem.Areas.SupplierMst.Models;
using JewelryManagementSystem.DAL;
using JewelryManagementSystem.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace JewelryManagementSystem.Areas.SupplierMst.Controllers
{
    [Area("SupplierMst")]
    [Route("SupplierMst/[Controller]/[action]")]
    public class SupplierMstController : Controller
    {
        #region Service
        private readonly ISupplierMst _supplierService;
        #endregion

        #region Constructor
        public SupplierMstController(ISupplierMst supplierService)
        {
            _supplierService = supplierService;
        }
        #endregion

        #region Index
        public IActionResult Index()
        {
            return View("SupplierMst_Index");
        }
        #endregion

        #region Fill Supplier
        [HttpPost]
        public IActionResult FillSupplier()
        {
            string p_sId = string.IsNullOrEmpty(Request.Form["p_sId"]) ? Guid.Empty.ToString() : Request.Form["p_sId"].ToString();

            try
            {
                Guid.TryParse(p_sId, out Guid p_uId);
                DataTable dtSupplier = _supplierService.GetAllSupplier(p_uId);
                List<SupplierMstModel> supplierList = new List<SupplierMstModel>();

                if (dtSupplier != null && dtSupplier.Rows.Count > 0)
                {
                    supplierList = dtSupplier
                            .Rows.Cast<DataRow>()
                            .Select(row => new SupplierMstModel
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
                    SupplierMst = supplierList
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

        #region Add Update Supplier
        [HttpPost]
        public IActionResult AddUpdateSupplier()
        {
            string p_sId = string.IsNullOrEmpty(Request.Form["p_sId"]) ? Guid.Empty.ToString() : Request.Form["p_sId"].ToString();
            string p_sName = string.IsNullOrEmpty(Request.Form["p_sName"]) ? string.Empty : Request.Form["p_sName"].ToString();
            string p_sEmails = string.IsNullOrEmpty(Request.Form["p_sEmails"]) ? string.Empty : Request.Form["p_sEmails"].ToString();
            string p_sContact = string.IsNullOrEmpty(Request.Form["p_sContact"]) ? string.Empty : Request.Form["p_sContact"].ToString();
            string p_sAddress = string.IsNullOrEmpty(Request.Form["p_sAddress"]) ? string.Empty : Request.Form["p_sAddress"].ToString();
            string p_sMode = string.IsNullOrEmpty(Request.Form["p_sMode"]) ? string.Empty : Request.Form["p_sMode"].ToString();

            try
            {
                if (Guid.TryParse(p_sId, out Guid p_uId))
                {
                    bool error = _supplierService.AddUpdateDeleteSupplier(p_uId, p_sName, p_sEmails, p_sContact, p_sAddress, p_sMode);
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

        #region Delete Supplier
        [HttpPost]
        public IActionResult DeleteSupplier()
        {
            string p_sId = string.IsNullOrEmpty(Request.Form["p_sId"]) ? Guid.Empty.ToString() : Request.Form["p_sId"].ToString();

            try
            {
                if (Guid.TryParse(p_sId, out Guid p_uId))
                {

                    bool error = _supplierService.AddUpdateDeleteSupplier(p_uId, string.Empty, string.Empty, string.Empty, string.Empty, "DELETE");
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
