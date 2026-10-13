namespace Web_API_Order.Models
{
    public class Orders
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public float TotalAmount { get; set; }
        public string Status { get; set; }
        public IEnumerable<Items> Items { get; set; }
    }
}
