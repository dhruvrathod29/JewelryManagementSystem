using JewelryManagementSystem.Areas.IncomingStockMst.Models;
using JewelryManagementSystem.DAL;
using JewelryManagementSystem.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data.SqlClient;
using System.Data;
using JewelryManagementSystem.Areas.OutgoingStockMst.Models;

namespace JewelryManagementSystem.Areas.OutgoingStockMst.Controllers
{
    [Area("OutgoingStockMst")]
    [Route("OutgoingStockMst/[Controller]/[action]")]
    public class OutgoingStockMstController : Controller
    {

        #region Service
        private readonly IOutgoingStockMst _outgoingStockService;
        private readonly IProductMst _productService;
        private readonly ICategoryMst _categoryService;
        private readonly ICustomerMst _customerService;
        #endregion

        #region Constructor
        public OutgoingStockMstController(IProductMst productService, ICategoryMst categoryService, ICustomerMst customerService, IOutgoingStockMst outgoingStockService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _customerService = customerService;
            _outgoingStockService = outgoingStockService;
        }
        #endregion

        #region Index
        public IActionResult Index()
        {
            FillDropDown();
            return View("OutgoingStockMst_Index");
        }
        #endregion

        #region Fill Outgoing Stock
        public IActionResult FillOutgoingStock()
        {
            try
            {
                Guid.TryParse(Request.Form["p_sId"], out Guid p_uId);
                DataTable dtOutgoingStock = _outgoingStockService.GetAllOutgoingStock(p_uId);

                List<OutgoingStockMstModel> outgoingStockList = new List<OutgoingStockMstModel>();

                if (dtOutgoingStock != null && dtOutgoingStock.Rows.Count > 0)
                {
                    outgoingStockList = dtOutgoingStock
                            .Rows.Cast<DataRow>() // Ensure you're working with DataRow objects
                            .Select(row => new OutgoingStockMstModel
                            {
                                ID = Guid.TryParse(row["ID"].ToString(), out Guid guid) ? guid : Guid.Empty,
                                ProductId = Guid.TryParse(row["PRODUCTID"].ToString(), out Guid productid) ? productid : Guid.Empty,
                                ProductName = CCommon.NullOrEmptyToString(row["PRODUCTNAME"]),
                                CustomerId = Guid.TryParse(row["CUSTOMERID"].ToString(), out Guid supplierid) ? supplierid : Guid.Empty,
                                CustomerName= CCommon.NullOrEmptyToString(row["CUSTOMERNAME"]),
                                CategoryId = Guid.TryParse(row["CATEGORYID"].ToString(), out Guid categoryid) ? categoryid : Guid.Empty,
                                CategoryName = CCommon.NullOrEmptyToString(row["CATEGORYNAME"]),
                                Quantity = CCommon.GetInt(row["QUANTITY"]),
                                Description = CCommon.NullOrEmptyToString(row["DESCRIPTION"]),
                                Price = CCommon.GetInt(row["PRICE"]),
                                TotalPrice = CCommon.GetInt(row["TOTALPRICE"]),
                                SoldDate = CCommon.NullOrDefaultDateTime(row["SOLDDATE"]),
                                CreationDate = CCommon.NullOrDefaultDateTime(row["CREATIONDATE"]),
                                ModificationDate = CCommon.NullOrDefaultDateTime(row["MODIFICATIONDATE"])

                            })
                            .ToList();
                }

                return Json(new
                {
                    OutgoingStockMst = outgoingStockList
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

        #region Add Update Outgoing Stock
        [HttpPost]
        public IActionResult AddUpdateOutgoingStock()
        {
            Guid.TryParse(Request.Form["p_sId"], out Guid p_uId);
            Guid.TryParse(Request.Form["p_sProductId"], out Guid p_uProductId);
            Guid.TryParse(Request.Form["p_sCustomerId"], out Guid p_uCustomerId);
            int.TryParse(Request.Form["p_iQuantity"], out int p_iQuantity);
            DateTime.TryParse(Request.Form["p_sReceivedDate"], out DateTime p_dReceivedDate);
            string p_sMode = string.IsNullOrEmpty(Request.Form["p_sMode"]) ? string.Empty : Request.Form["p_sMode"].ToString();
            try
            {
                bool error = _outgoingStockService.AddUpdateOutgoingStock(p_uId, p_uProductId, p_uCustomerId, p_iQuantity, p_dReceivedDate, p_sMode);

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

        #region Delete Incoming Stock
        [HttpPost]
        public IActionResult DeleteIncomingStock()
        {
            try
            {
                Guid.TryParse(Request.Form["p_sId"], out Guid p_uId);
                bool error = _outgoingStockService.AddUpdateOutgoingStock(p_uId, Guid.Empty, Guid.Empty, 0, DateTime.Now, "DELETE");

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

        #region Fill Category and Supplier DropDown
        public void FillDropDown()
        {
            try
            {
                DataSet ds = _productService.ddlFillProduct(Guid.Empty);

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

                    if (ds.Tables.Contains("dtCustomer"))
                    {
                        ViewBag.ddlCustomer = ds.Tables["dtCustomer"].AsEnumerable()
                                            .Select(row => new SelectListItem
                                            {
                                                Value = row["ID"].ToString(),
                                                Text = row["NAME"].ToString()
                                            })
                                            .ToList();
                    }
                    else
                    {
                        ViewBag.ddlCustomer = new List<SelectListItem>();
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
                DataSet ds = _productService.ddlFillProduct(p_uId);

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
