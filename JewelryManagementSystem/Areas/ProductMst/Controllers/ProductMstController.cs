using JewelryManagementSystem.Areas.CategoryMst.Models;
using JewelryManagementSystem.Areas.ProductMst.Models;
using JewelryManagementSystem.DAL;
using JewelryManagementSystem.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using System.Data.SqlClient;

namespace JewelryManagementSystem.Areas.ProductMst.Controllers
{
    [Area("ProductMst")]
    [Route("ProductMst/[Controller]/[action]")]
    public class ProductMstController : Controller
    {
        #region Service
        private readonly IProductMst _productService;
        private readonly ICategoryMst _categoryService;
        #endregion

        #region Constructor
        public ProductMstController(IProductMst productService, ICategoryMst categoryService)
        {
            _productService = productService;   
            _categoryService = categoryService;
        }
        #endregion

        #region Index
        public IActionResult Index()
        {
            FillddlCategory();
            return View("ProductMst_Index");
        }
        #endregion

        #region Fill Product
        public IActionResult FillProduct()
        {
            string p_sId = string.IsNullOrEmpty(Request.Form["p_sId"]) ? Guid.Empty.ToString() : Request.Form["p_sId"].ToString();
            try
            {
                Guid.TryParse(p_sId, out Guid p_uId);
                DataTable dtProduct = _productService.GetAllProduct(p_uId);
                List<ProductMstModel> productList = new List<ProductMstModel>();

                if (dtProduct != null && dtProduct.Rows.Count > 0)
                {
                    productList = dtProduct
                            .Rows.Cast<DataRow>() // Ensure you're working with DataRow objects
                            .Select(row => new ProductMstModel
                            {
                                ID = Guid.TryParse(row["ID"].ToString(), out var guid) ? guid : Guid.Empty,
                                Name = CCommon.NullOrEmptyToString(row["NAME"]),
                                CategoryID = Guid.TryParse(row["CATEGORYID"].ToString(), out var CategoryId) ? CategoryId : Guid.Empty,
                                CategoryName = CCommon.NullOrEmptyToString(row["CATEGORYNAME"]),
                                Description = CCommon.NullOrEmptyToString(row["Description"]),
                                Price = CCommon.GetInt(row["PRICE"]),
                                CreationDate = CCommon.NullOrDefaultDateTime(row["CREATIONDATE"]),
                                ModificationDate = CCommon.NullOrDefaultDateTime(row["MODIFICATIONDATE"])
                            })
                            .ToList();
                }

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

        #region Add Update Product
        [HttpPost]
        public IActionResult AddUpdateProduct()
        {
            string p_sId = string.IsNullOrEmpty(Request.Form["p_sId"]) ? Guid.Empty.ToString() : Request.Form["p_sId"].ToString();
            string p_sName = string.IsNullOrEmpty(Request.Form["p_sName"]) ? string.Empty : Request.Form["p_sName"].ToString();
            string p_sCategoryId = string.IsNullOrEmpty(Request.Form["p_sCategoryId"]) ? string.Empty : Request.Form["p_sCategoryId"].ToString();
            int.TryParse(Request.Form["p_sPrice"], out int p_iPrice);
            string p_sDescription = string.IsNullOrEmpty(Request.Form["p_sDescription"]) ? string.Empty : Request.Form["p_sDescription"].ToString();
            string p_sMode = string.IsNullOrEmpty(Request.Form["p_sMode"]) ? string.Empty : Request.Form["p_sMode"].ToString();

            try
            {
                if (Guid.TryParse(p_sId, out Guid p_uId) && Guid.TryParse(p_sCategoryId, out Guid p_uCategoryId))
                {
                    bool error = _productService.AddUpdateDeleteProduct(p_uId, p_sName, p_uCategoryId, p_iPrice, p_sDescription, p_sMode);

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

        #region Delete Product
        [HttpPost]
        public IActionResult DeleteProduct()
        {
            string p_sId = string.IsNullOrEmpty(Request.Form["p_sId"]) ? Guid.Empty.ToString() : Request.Form["p_sId"].ToString();
            try
            {
                if (Guid.TryParse(p_sId, out Guid p_uId))
                {
                    bool error = _productService.AddUpdateDeleteProduct(p_uId, string.Empty, Guid.Empty, 0, string.Empty, "DELETE");
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

        #region Fill Category DropDown
        public void FillddlCategory()
        {
            DataTable dtCategory = _categoryService.GetAllCategory(Guid.Empty);

            ViewBag.ddlCategory = dtCategory.AsEnumerable()
                                    .Select(row => new SelectListItem
                                    {
                                        Value = row["ID"].ToString(),
                                        Text = row["NAME"].ToString()
                                    })
                                    .ToList();
        }
        #endregion
    }
}
