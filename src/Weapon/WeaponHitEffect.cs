using U3.Core;
using U3.Item;
using U3.ObjectPool;
using UnityEngine;

namespace U3.Weapon
{
    public class WeaponHitEffect : Vassal<WeaponMaster>
    {
        // pool test
        private ObjectPoolsManager effectPool;

        public override void OnMasterEnabled(WeaponMaster master)
        {
            base.OnMasterEnabled(master);

            effectPool = ObjectPoolsManager.Instance;
            Master.EventWeaponFired += TestSpawnObject;
        }

        private void OnDisable()
        {
            Master.EventWeaponFired -= TestSpawnObject;
        }

        private void TestSpawnObject(FireInputOrigin _)
        {
            /*PooledObject obj = effectPool.GetObject("test");
            obj.ObjTransform.position = transform.position;
            obj.Obj.SetActive(true);
            obj.ObjRigidbody.velocity = new Vector3(0f, 0f, 0f);
            obj.ObjRigidbody.AddForce(transform.forward * 20f, ForceMode.Impulse);*/
        }
    }
}
