using System.Collections;
using TMPro;
using U3.Log;
using U3.ObjectPool;
using UnityEngine;

namespace U3.Damageable
{
    public class DamageText : MonoBehaviour
    {
        [SerializeField] private TMP_Text dmgText;

        public bool IsLocked { get; private set; }

        private Transform m_Transform;
        private PooledObjectReturner objectReturner;

        private IEnumerator DisableEffect()
        {
            yield return DamageTextManager.Instance.WaitLock;
            IsLocked = false;
            objectReturner.ReturnToPool<DamageText>();
            gameObject.SetActive(false);
        }

        private void MoveText(Vector3 posToGo, Transform camTransform)
        {
            m_Transform.LookAt(camTransform);
            m_Transform.SetPositionAndRotation(Vector3.Lerp(m_Transform.position, posToGo, 0.1f),
                Quaternion.LookRotation(camTransform.forward));
        }

        private Vector3 GetGoToPos(float posUp, float posRnd)
        {
            Vector3 posToGo = m_Transform.position;
            posToGo.y += posUp;
            posToGo += Random.insideUnitSphere * posRnd;
            return posToGo;
        }

        private IEnumerator ShowDmgText()
        {
            DamageTextSettings textSettings = DamageTextManager.Instance.DamageTextSettings;
            int animationIterations = textSettings.TextAnimationIterations;
            WaitForSeconds loopDelay = DamageTextManager.Instance.LoopDelay;

            dmgText.outlineWidth = textSettings.OutlineWidth;

            Color32 textColor = textSettings.StartColor;
            Color32 outlineColor = textSettings.OutlineStartColor;
            Vector3 posToGo = GetGoToPos(textSettings.PosToGoUp, textSettings.PosToGoRnd);
            Transform mainCamTransform = DamageTextManager.Instance.MainCameraTransform;

            for (int i = 0; i < animationIterations; i++)
            {
                yield return loopDelay;
                MoveText(posToGo, mainCamTransform);

                if (textColor.a > 8)
                {
                    textColor.a -= 8;
                    outlineColor.a -= 8;
                    dmgText.color = textColor;
                    dmgText.outlineColor = outlineColor;
                }
                else if (textColor.a > 0)
                {
                    textColor.a = 0;
                    outlineColor.a = 0;
                    dmgText.color = textColor;
                    dmgText.outlineColor = outlineColor;
                    StopCoroutine(ShowDmgText());
                }
            }
        }

        public void Play(string dmgVal)
        {
            IsLocked = true;
            dmgText.text = dmgVal;
            StartCoroutine(ShowDmgText());
            StartCoroutine(DisableEffect());
        }

        private void Awake()
        {
            if (m_Transform == null)
                m_Transform = transform;
        }

        private void Start()
        {
            if (TryGetComponent(out PooledObjectReturner por))
                objectReturner = por;
            else
                GameLogger.Log(new GameLog(Log.LogType.Error,$"Missing PooledObjectReturner on {gameObject.name} object"));
        }
    }
}
