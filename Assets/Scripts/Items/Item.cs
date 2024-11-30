namespace Items
{
    public class Item
    {
        private int _count;
        private string _id;
        private string _name;
        private string _description;

        public int Count
        {
            get
            {
                return _count;
            }
            private set
            {
                _count = value;
            }
        }

        public string Id
        {
            get
            {
                return _id;
            }
            private set
            {
                _id = value;
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
            private set
            {
                _name = value;
            }
        }

        public string Description
        {
            get
            {
                return _description;
            }
            private set
            {
                _description = value;
            }
        }

        public Item AddItem(Item other)
        {
            if (IsStackable(other))
            {
                Count += other.Count;
                return null;
            }
            else
            {
                return other;
            }
        }

        public Item AddCount(int count)
        {
            Count += count;
            return null;
        }

        public Item GetItem(Item other)
        {
            if (!IsStackable(other))
            {
                return null;
            }

            if (other.Count >= Count)
            {
                Count = 0;
                return this;
            }
            else
            {
                Count -= other.Count;
                return other;
            }
        }

        public Item GetCount(int count)
        {
            if (count >= Count)
            {
                Count = 0;
                return this;
            }

            Item item = ItemManager.GetById(Id, count);
            Count -= count;
            return item;
        }

        public virtual bool IsStackable(Item other)
        {
            return other.Id == Id;
        }

        public Item(int count, string id, string name, string description)
        {
            _count = count;
            _id = id;
            _name = name;
            _description = description;
        }

        public Item(Item reference) : this(reference.Count, reference.Id, reference.Name, reference.Description) { }

        public virtual Item Clone()
        {
            return new Item(this);
        }
    }
}
