using UnityEngine.VFX;
using UnityEngine;
using System.Collections;

namespace U3.Destructible
{
    public class EffectVFX : Effect
    {
        [SerializeField] private float lockTime;
        [SerializeField] private GameObject effectObj;

        private Transform m_Transform;
        private VisualEffect effect;
        private WaitForSeconds waitLock;

        private IEnumerator DisableEffect()
        {
            yield return waitLock;
            IsLocked = false;
            gameObject.SetActive(false);
        }

        public override void Play()
        {
            effect.Play();
            IsLocked = true;
            StartCoroutine(DisableEffect());
        }

        private void Awake()
        {
            if (m_Transform == null)
                m_Transform = transform;

            effect = effectObj.GetComponent<VisualEffect>();
            waitLock = new WaitForSeconds(lockTime);
        }
    }
}
