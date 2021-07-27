namespace Assets.ObjectTypes.ItemData
{
    public class ExclusiveItem : ItemData
    {
        public ExclusiveItem()
        {
            itemType = ItemType.Exclusive;
        }

        public ExclusiveItem(int id)
        {
            this.id = id;
            itemType = ItemType.Exclusive;
        }

        public override bool isConsumedOnUse()
        {
            return false;
        }

        public override bool isUsable()
        {
            return false;
        }

        public override bool isStackable()
        {
            return false;
        }

    }
}