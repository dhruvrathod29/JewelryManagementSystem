using JewelryManagementSystem.Areas.CategoryMst.Models;
using JewelryManagementSystem.DAL;
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

        #region FillData

        [HttpPost]
        public IActionResult FillData()
        {
            DataTable dtblCategory = _categoryService.GetAllCategory();
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
                        })
                        .ToList();
            }

            return Json(new
            {
                CategoryMst = categoryList
            });
		}
		#endregion

	}
}
