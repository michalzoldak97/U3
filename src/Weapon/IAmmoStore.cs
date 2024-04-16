namespace U3.Weapon
{
    public interface IAmmoStore
    {
        public int GetAmmo(string ammoCode);

        public int RetrieveAmmo(int amount, string ammoCode);

        public int AddAmmo(int amount, string ammoCode);
    }
}
