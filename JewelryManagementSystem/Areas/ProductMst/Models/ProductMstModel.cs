namespace JewelryManagementSystem.Areas.ProductMst.Models
{
    public class ProductMstModel
    {
        public Guid ID{ get; set; }
        public string Name { get; set; }
        public Guid CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set;}
    }
}
