using JewelryManagementSystem.Areas.IncomingStockMst.Models;
using JewelryManagementSystem.Areas.ProductMst.Models;
using JewelryManagementSystem.DAL;
using JewelryManagementSystem.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Data;
using System.Data.SqlClient;

namespace JewelryManagementSystem.Areas.IncomingStockMst.Controllers
{
    [Area("IncomingStockMst")]
    [Route("IncomingStockMst/[Controller]/[action]")]
    public class IncomingStockMstController : Controller
    {
        #region Service
        private readonly IIncomingStockMst _incomingStockService;
        private readonly IProductMst _productService;
        private readonly ICategoryMst _categoryService;
        private readonly ISupplierMst _supplierService;
        #endregion

        #region Constructor
        public IncomingStockMstController(IProductMst productService, ICategoryMst categoryService, ISupplierMst supplierService, IIncomingStockMst incomingStockService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _supplierService = supplierService;
            _incomingStockService = incomingStockService;
        }
        #endregion

        #region Index
        public IActionResult Index()
        {
            FillDropDown();
            return View("IncomingStockMst_Index");
        }
        #endregion

        #region Add Update Product
        [HttpPost]
        public IActionResult AddUpdateIncomingStock()
        {
            Guid.TryParse(Request.Form["p_sId"], out Guid p_uId);
            Guid.TryParse(Request.Form["p_sProductId"], out Guid p_uProductId);
            Guid.TryParse(Request.Form["p_sSupplierId"], out Guid p_sSupplierId);
            int.TryParse(Request.Form["p_iQuantity"], out int p_iQuantity);
            DateTime.TryParse(Request.Form["p_sReceivedDate"], out DateTime p_dReceivedDate);
            string p_sMode = string.IsNullOrEmpty(Request.Form["p_sMode"]) ? string.Empty : Request.Form["p_sMode"].ToString();
            try
            {
                bool error = _incomingStockService.AddUpdateIncomingStock();

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

        #region Fill Incoming Stock
        public IActionResult FillIncomingStock()
        {
            try
            {
                Guid.TryParse(Request.Form["p_sId"], out Guid p_uId);
                DataTable dtIncomingStock = _incomingStockService.GetAllIncomingStock(p_uId);

                List<IncomingStockMstModel> incomingStockList = new List<IncomingStockMstModel>();

                if (dtIncomingStock != null && dtIncomingStock.Rows.Count > 0)
                {
                    incomingStockList = dtIncomingStock
                            .Rows.Cast<DataRow>() // Ensure you're working with DataRow objects
                            .Select(row => new IncomingStockMstModel
                            {
                                ID = Guid.TryParse(row["ID"].ToString(), out Guid guid) ? guid : Guid.Empty,
                                ProductId = Guid.TryParse(row["PRODUCTID"].ToString(), out Guid productid) ? productid : Guid.Empty,
                                ProductName = CCommon.NullOrEmptyToString(row["PRODUCTNAME"]),
                                SupplierId = Guid.TryParse(row["SUPPLIERID"].ToString(), out Guid supplierid) ? supplierid : Guid.Empty,
                                SupplierName = CCommon.NullOrEmptyToString(row["SUPPLIERNAME"]),
                                CategoryId = Guid.TryParse(row["CATEGORYID"].ToString(), out Guid categoryid) ? categoryid : Guid.Empty,
                                CategoryName = CCommon.NullOrEmptyToString(row["CATEGORYNAME"]),
                                Quantity = CCommon.GetInt(row["QUANTITY"]),
                                Description = CCommon.NullOrEmptyToString(row["DESCRIPTION"]),
                                Price = CCommon.GetInt(row["PRICE"]),
                                TotalPrice = CCommon.GetInt(row["TOTALPRICE"]),
                                ReceivedDate = CCommon.NullOrDefaultDateTime(row["RECEIVEDDATE"]),
                                CreationDate = CCommon.NullOrDefaultDateTime(row["CREATIONDATE"]),
                                ModificationDate = CCommon.NullOrDefaultDateTime(row["MODIFICATIONDATE"])
                                
                            })
                            .ToList();
                }

                return Json(new
                {
                    IncomingStockMst = incomingStockList
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

        #region Fill Category and Supplier DropDown
        public void FillDropDown()
        {
            try
            {
                DataSet ds = _incomingStockService.ddlFillProduct(Guid.Empty);

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables.Contains("dtCategory"))
                    {
                        ViewBag.ddlCategory = ds.Tables["dtCategory"].AsEnumerable()
                                           .Select(row => new SelectListItem
                                           {
                                               Value = row["ID"].ToString(),
                                               Text = row["NAME"].ToString()
                                           })
                                           .ToList();
                    }
                    else
                    {
                        ViewBag.ddlCategory = new List<SelectListItem>();
                    }

                    if (ds.Tables.Contains("dtSupplier"))
                    {
                        ViewBag.ddlSupplier = ds.Tables["dtSupplier"].AsEnumerable()
                                            .Select(row => new SelectListItem
                                            {
                                                Value = row["ID"].ToString(),
                                                Text = row["NAME"].ToString()
                                            })
                                            .ToList();
                    }
                    else
                    {
                        ViewBag.ddlSupplier = new List<SelectListItem>();
                    }

                    if (ds.Tables.Contains("dtProduct"))
                    {
                        ViewBag.dtProduct = ds.Tables["dtProduct"];
                    }
                }
            }
            catch (Exception)
            {
                ViewBag.ddlCategory = new List<SelectListItem>();
                ViewBag.ddlSupplier = new List<SelectListItem>();
            }
        }
        #endregion

        #region Fill Product DropDown
        [HttpPost]
        public IActionResult ddlFillProduct()
        {
            try
            {
                Guid.TryParse(Request.Form["p_sId"], out Guid p_uId);
                DataSet ds = _incomingStockService.ddlFillProduct(p_uId);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables.Contains("dtProduct"))
                {
                    var result = ds.Tables["dtProduct"].AsEnumerable()
                                .Select(row => new SelectListItem
                                {
                                    Value = row["ID"].ToString(),
                                    Text = row["NAME"].ToString(),
                                }).ToList();

                    return Json(result);
                }
                return Json(new List<SelectListItem>());

            }
            catch (Exception)
            {
                return Json(new { success = false, message = "An error occurred while fetching products." });
            }
        }
        #endregion
    }
}
