namespace U3.Item
{
    public readonly struct ItemInputOrigin
    {
        int ID { get; }
        public string Name { get; }
        // TODO: public TeamID { get; }

        public ItemInputOrigin(int id, string name)
        {
            ID = id;
            Name = name;
        }
    }
}
