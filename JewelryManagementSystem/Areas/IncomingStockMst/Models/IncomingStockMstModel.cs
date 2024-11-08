namespace JewelryManagementSystem.Areas.IncomingStockMst.Models
{
    public class IncomingStockMstModel
    {
        public Guid ID { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public Guid SupplierId { get; set; }
        public string SupplierName { get; set; }
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int Quantity { get; set; }
        public string Description {  get; set; }
        public DateTime ReceivedDate {  get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }

    }
}
