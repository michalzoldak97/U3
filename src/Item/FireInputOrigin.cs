namespace U3.Item
{
    public readonly struct FireInputOrigin
    {
        int ID { get; }
        public string Name { get; }
        // TODO: public TeamID { get; }

        public FireInputOrigin(int id, string name)
        {
            ID = id;
            Name = name;
        }
    }
}
