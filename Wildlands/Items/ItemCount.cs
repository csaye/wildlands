namespace Wildlands.Items
{
    public struct ItemCount
    {
        public Item Item { get; set; }
        public int Count { get; set; }

        public bool IsEmpty => Item == Item.None || Count == 0;

        public ItemCount(Item item, int count)
        {
            Item = item;
            Count = count;
        }
    }
}
