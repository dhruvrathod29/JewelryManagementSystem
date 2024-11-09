namespace JewelryManagementSystem.Areas.AvailableStockMst.Models
{
	public class AvailableStockMstModel
	{
		public string ProductName { get; set; }
		public string CategoryName { get; set; }
		public string Description { get; set; }
		public int Price { get; set; }
		public int IncomingStockQuantity { get; set; }
		public int OutgoingStockQuantity { get; set; }
		public int TotalQuantity {  get; set; }
	}
}
