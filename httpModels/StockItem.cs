namespace solution_learn.Controllers
{
    public class StockItem
    {
        public StockItem(long itemId, string itemName, int quantity)
        {
            ItemId = itemId;
            ItemName = itemName;
            Quantity = quantity;
        }
        public long ItemId { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }
    }
}
