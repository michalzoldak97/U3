using UnityEngine;
using U3.Item;
using System.Collections;

namespace U3.Weapon.Explosive
{
    public class ExplosiveFuse : MonoBehaviour
    {
        [SerializeField] private LayerMask layersToHit;
        private ExplosiveMaster m_Explosive;

        private IEnumerator Ignite()
        {
            yield return new WaitForSeconds(2f);
            m_Explosive.Explode(new FireInputOrigin(gameObject.GetInstanceID(), layersToHit, layersToHit));
        }

        private void Start ()
        {
            m_Explosive = GetComponent<ExplosiveMaster>();
            StartCoroutine(Ignite());
        }
    }
}