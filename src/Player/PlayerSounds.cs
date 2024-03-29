using U3.Input;
using U3.Player.Controller;
using UnityEngine;

namespace U3.Player
{
    public class PlayerSounds : MonoBehaviour
    {
        private int previousStep;
        private float stepTimer;
        private Vector2 stepSpeed;
        private AudioClip[] footsteps; // cashing because these're used frequently
        private AudioSource m_audioSource;
        private PlayerMaster playerMaster;
        private PlayerMoveManager moveManager;

        private void SetInit()
        {
            m_audioSource = GetComponent<AudioSource>();
            playerMaster = GetComponent<PlayerMaster>();
            moveManager = GetComponent<PlayerMoveManager>();

            footsteps = playerMaster.PlayerSettings.Sound.StepSounds;
            stepSpeed = playerMaster.PlayerSettings.Controller.StepSpeed;
        }

        private void OnEnable()
        {
            SetInit();

            PlayerInputManager.HumanoidInputActions.EventJump += PlayJumpSound;
            moveManager.EventLand += PlayLandSound;
            moveManager.EventStep += PlayFootstepSound;
        }
        private void OnDisable()
        {
            PlayerInputManager.HumanoidInputActions.EventJump -= PlayJumpSound;
            moveManager.EventLand -= PlayLandSound;
            moveManager.EventStep -= PlayFootstepSound;
        }

        private void PlayJumpSound()
        {
            m_audioSource.PlayOneShot(playerMaster.PlayerSettings.Sound.JumpSound);
        }

        private void PlayLandSound(int dummy)
        {
            m_audioSource.PlayOneShot(playerMaster.PlayerSettings.Sound.LandSound);
        }

        /// <summary>
        /// Returns audio clip index
        /// Different than previously choosen one
        /// </summary>
        /// <returns></returns>
        private int GetAnotherStep()
        {
            int fLen = footsteps.Length;

            if (previousStep == 0)
                return Random.Range(1, fLen);

            if (previousStep == fLen - 1)
                return Random.Range(0, fLen - 1);

            int[] aSteps = new int[fLen]; // sequence of indexes

            for (int i = 0; i < fLen; i++)
            {
                aSteps[i] = i;
            }

            aSteps[previousStep]++; // replace previous with upper

            int randIdx = Random.Range(0, aSteps.Length);
            return aSteps[randIdx];
        }
        private void PlayFootstepSound(int stateIdx)
        {
            stepTimer -= Time.deltaTime;

            if (stepTimer > 0)
                return;

            int stepIdx = GetAnotherStep();
            m_audioSource.PlayOneShot(footsteps[stepIdx]);
            previousStep = stepIdx;

            stepTimer = stepSpeed[stateIdx];
        }
    }
}
