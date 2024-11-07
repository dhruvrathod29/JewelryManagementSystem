using JewelryManagementSystem.Areas.ProductMst.Models;
using JewelryManagementSystem.DAL;
using JewelryManagementSystem.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

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

        #region Fill Incoming Stock
        public IActionResult FillIncomingStock()
        {
            string p_sId = string.IsNullOrEmpty(Request.Form["p_sId"]) ? Guid.Empty.ToString() : Request.Form["p_sId"].ToString();
            try
            {
                Guid.TryParse(p_sId, out Guid p_uId);
                DataTable dtProduct = _incomingStockService.GetAllIncomingStock(p_uId);


                List<ProductMstModel> productList = new List<ProductMstModel>();

                //if (dtProduct != null && dtProduct.Rows.Count > 0)
                //{
                //    productList = dtProduct
                //            .Rows.Cast<DataRow>() // Ensure you're working with DataRow objects
                //            .Select(row => new ProductMstModel
                //            {
                //                ID = Guid.TryParse(row["ID"].ToString(), out var guid) ? guid : Guid.Empty,
                //                Name = CCommon.NullOrEmptyToString(row["NAME"]),
                //                CategoryID = Guid.TryParse(row["CATEGORYID"].ToString(), out var CategoryId) ? CategoryId : Guid.Empty,
                //                CategoryName = CCommon.NullOrEmptyToString(row["CATEGORYNAME"]),
                //                Description = CCommon.NullOrEmptyToString(row["Description"]),
                //                Price = CCommon.GetInt(row["PRICE"]),
                //                CreationDate = CCommon.NullOrDefaultDateTime(row["CREATIONDATE"]),
                //                ModificationDate = CCommon.NullOrDefaultDateTime(row["MODIFICATIONDATE"])
                //            })
                //            .ToList();
                //}

                return Json(new
                {
                    ProductMst = productList
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
                DataTable dtCategory = _categoryService.GetAllCategory(Guid.Empty);
                DataTable dtSupplier = _supplierService.GetAllSupplier(Guid.Empty);

                if (dtCategory != null && dtCategory.Rows.Count > 0)
                {
                    ViewBag.ddlCategory = dtCategory.AsEnumerable()
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

                if (dtSupplier != null && dtSupplier.Rows.Count > 0)
                {
                    ViewBag.ddlSupplier = dtSupplier.AsEnumerable()
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
            string p_sId = string.IsNullOrEmpty(Request.Form["p_sId"]) ? Guid.Empty.ToString() : Request.Form["p_sId"].ToString();
            Guid.TryParse(p_sId, out Guid p_uId);

            try
            {
                DataTable dtProduct = _productService.ddlFillProduct(p_uId);

                if (dtProduct == null || dtProduct.Rows.Count == 0)
                {
                    return Json(new List<SelectListItem>());
                }

                var result = dtProduct.AsEnumerable()
                        .Select(row => new SelectListItem
                        {
                            Value = row["ID"].ToString(),
                            Text = row["NAME"].ToString(),
                        }).ToList();

                return Json(result);
            }
            catch (Exception)
            {
                return Json(new { success = false, message = "An error occurred while fetching products." });
            }
        }


        #endregion
    }
}
