namespace JewelryManagementSystem.Areas.OutgoingStockMst.Models
{
    public class OutgoingStockMstModel
    {
        public Guid ID { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; }
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int Quantity { get; set; }
        public string Description {  get; set; }
        public int Price { get; set; }
        public int TotalPrice { get; set; }
        public DateTime SoldDate {  get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
    }
}
