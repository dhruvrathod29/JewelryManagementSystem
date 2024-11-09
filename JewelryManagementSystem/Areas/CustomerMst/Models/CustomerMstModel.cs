namespace JewelryManagementSystem.Areas.CustomerMst.Models
{
	public class CustomerMstModel
	{
        public Guid ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Emails { get; set; } = string.Empty;
        public string Contact { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
    }
}
