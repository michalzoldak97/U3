namespace U3.Item
{
    public readonly struct FireInputOrigin
    {
        public int ID { get; }
        public int TeamID { get; }
        public string Name { get; }

        public FireInputOrigin(int id, int teamID, string name)
        {
            ID = id;
            TeamID = teamID;
            Name = name;
        }
    }
}
