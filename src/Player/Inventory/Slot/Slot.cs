
namespace U3.Player.Inventory
{
    public class Slot
    {
        public string Name { get; private set; }
        public Container[] Containers { get; private set; }

        public Slot(string name, int cNum)
        {
            Name = name;
            Containers = new Container[cNum];

            for (int i = 0; i < cNum; i++)
            {
                Containers[i] = new Container(i);
            }
        }
    }
}
