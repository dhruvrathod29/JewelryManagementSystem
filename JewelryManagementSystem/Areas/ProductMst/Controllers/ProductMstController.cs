using JewelryManagementSystem.Interface;
using Microsoft.AspNetCore.Mvc;

namespace JewelryManagementSystem.Areas.ProductMst.Controllers
{
    public class ProductMstController : Controller
    {
        private readonly IProductMst _productService;

        public ProductMstController(IProductMst productService)
        {
            _productService = productService;   
        }

        public IActionResult Index()
        {
            return View("ProductMst_Index");
        }

        public IActionResult FillProduct()
        {
            try
            {

            }
            catch (Exception)
            {

                throw;
            }

            return View();
        }
    }
}
