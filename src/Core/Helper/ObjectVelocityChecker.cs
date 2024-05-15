using System.Collections;
using UnityEngine;

namespace U3.Core.Helper
{
    public class ObjectVelocityChecker : MonoBehaviour
    {
        [SerializeField] private int checksCount;
        [SerializeField] private float checkDelaySec;

        private Rigidbody rb;
        private WaitForSeconds waitDelay;

        private IEnumerator PrintVelocities()
        {
            Debug.Log($"Rigidbody velocity is: {rb.velocity} with magnitude {rb.velocity.magnitude} sqr with magnitude {rb.velocity.sqrMagnitude}");

            for (int i = 0; i < checksCount; i++)
            {
                yield return waitDelay;
                Debug.Log($"Rigidbody velocity is: {rb.velocity} with magnitude {rb.velocity.magnitude} sqr with magnitude {rb.velocity.sqrMagnitude}");
            }
        }

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            waitDelay = new WaitForSeconds(checkDelaySec);
        }

        private void OnEnable()
        {
            StartCoroutine(PrintVelocities());
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }
    }
}
