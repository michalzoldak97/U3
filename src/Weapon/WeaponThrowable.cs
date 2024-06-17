using System.Collections;
using U3.Core;
using U3.Front.Components;
using U3.Item;
using U3.Log;
using UnityEngine;

namespace U3.Weapon
{
    public class WeaponThrowable : Vassal<WeaponMaster>
    {
        private float throwForce, forceStep, stepDelay;
        private IProgressBar forceProgressBar;
        private WaitForSeconds waitStepDelay;

        public override void OnMasterEnabled(WeaponMaster master)
        {
            base.OnMasterEnabled(master);

            Master.EventAimDownCalled += OnForceIncrease;
            Master.EventAimUpCalled += ResetForce;

            Master.EventInputInterrupted += ResetForce;
            Master.ItemMaster.EventDeselected += ResetForce;
        }

        private void OnDisable()
        {
            Master.EventAimDownCalled -= OnForceIncrease;
            Master.EventAimUpCalled -= ResetForce;

            Master.EventInputInterrupted -= ResetForce;
            Master.ItemMaster.EventDeselected -= ResetForce;
        }

        private IEnumerator IncreaseForce()
        {
            for (int i = 0; i < Master.WeaponSettings.ThrowableSetting.NumberOfIncreaseSteps; i++)
            {
                throwForce += forceStep;
                forceProgressBar.AddProgress((int)((throwForce / Master.WeaponSettings.ThrowableSetting.ThrowForce) * 100));
                yield return waitStepDelay;
            }

            forceProgressBar.SetFull();
        }

        private void OnForceIncrease()
        {
            StartCoroutine(IncreaseForce());
        }

        private void ResetForce()
        {
            StopAllCoroutines();
            throwForce = 0f;
            forceProgressBar.ResetProgeress();
        }

        private void Throw(FireInputOrigin origin)
        {

        }

        private void Start()
        {
            forceStep = Master.WeaponSettings.ThrowableSetting.ThrowForce / Master.WeaponSettings.ThrowableSetting.NumberOfIncreaseSteps;
            stepDelay = Master.WeaponSettings.ThrowableSetting.IncreaseMaxDuration / Master.WeaponSettings.ThrowableSetting.NumberOfIncreaseSteps;
            waitStepDelay = new WaitForSeconds(stepDelay);

            if (TryGetComponent(out IProgressBar progressBar))
            {
                forceProgressBar = progressBar;
            }
            else
            {
                GameLogger.Log(new GameLog(Log.LogType.Error, $"Required progress bar not found on object {gameObject.name}"));
            }
        }
    }
}
