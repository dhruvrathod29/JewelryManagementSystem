using JewelryManagementSystem.Areas.CategoryMst.Models;
using JewelryManagementSystem.DAL;
using JewelryManagementSystem.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace JewelryManagementSystem.Areas.CategoryMst.Controllers
{
    [Area("CategoryMst")]
    [Route("CategoryMst/[Controller]/[action]")]
    public class CategoryMstController : Controller
    {
        private readonly ICategoryMst _categoryService;

        public CategoryMstController(ICategoryMst categoryService)
        {
            _categoryService = categoryService;
        }
        public IActionResult Index()
        {
            return View();
        }

        #region FillCategory

        [HttpPost]
        public IActionResult FillCategory()
        {
            string p_sId = string.IsNullOrEmpty(Request.Form["p_sId"]) ? Guid.Empty.ToString() : Request.Form["p_sId"].ToString();

            try
            {
                Guid.TryParse(p_sId, out Guid p_uId);
                DataTable dtblCategory = _categoryService.GetAllCategory(p_uId);
                List<CategoryMstModel> categoryList = new List<CategoryMstModel>();

                if (dtblCategory != null && dtblCategory.Rows.Count > 0)
                {
                    categoryList = dtblCategory
                            .Rows.Cast<DataRow>() // Ensure you're working with DataRow objects
                            .Select(row => new CategoryMstModel
                            {
                                ID = Guid.TryParse(row["ID"].ToString(), out var guid) ? guid : Guid.Empty,
                                Name = CCommon.NullOrEmptyToString(row["NAME"]),
                                CreationDate = CCommon.NullOrDefaultDateTime(row["CREATIONDATE"]),
                                ModificationDate = CCommon.NullOrDefaultDateTime(row["MODIFICATIONDATE"])
                            })
                            .ToList();
                }

                return Json(new
                {
                    CategoryMst = categoryList
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = "An error occurred while retrieving categories.",
                    error = ex.Message // Optionally include the error message for debugging
                });
            }
        }
        #endregion

        #region AddUpdateCategory
        public IActionResult AddUpdateCategory()
        {
            string p_sId = string.IsNullOrEmpty(Request.Form["p_sId"]) ? Guid.Empty.ToString() : Request.Form["p_sId"].ToString();
            string p_sCategoryName = string.IsNullOrEmpty(Request.Form["p_sCategoryName"]) ? string.Empty : Request.Form["p_sCategoryName"].ToString();
            string p_sMode = string.IsNullOrEmpty(Request.Form["p_sMode"]) ? string.Empty : Request.Form["p_sMode"].ToString();

            try
            {
                if (Guid.TryParse(p_sId, out Guid p_uId))
                {
                    bool error = _categoryService.AddUpdateDeleteCategory(p_uId, p_sCategoryName, p_sMode);
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

        #region DeleteCategory
        public IActionResult DeleteCategory()
        {
            string p_sId = string.IsNullOrEmpty(Request.Form["p_sId"]) ? Guid.Empty.ToString() : Request.Form["p_sId"].ToString();
           
            try
            {
                if (Guid.TryParse(p_sId, out Guid p_uId))
                {

                    bool error = _categoryService.AddUpdateDeleteCategory(p_uId, string.Empty, "DELETE");
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
