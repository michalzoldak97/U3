namespace U3.Weapon
{
    public interface IAmmoStore
    {
        public int GetAmmo(int requestedAmount, string ammoCode);
        public int AddAmmo(int amount, string ammoCode);
    }
}
