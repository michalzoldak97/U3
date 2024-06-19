using U3.Core;
using U3.Item;
using U3.Log;
using UnityEngine;

namespace U3.Weapon
{
    public class WeaponAnimation : Vassal<WeaponMaster>
    {
        private Transform animatedMesh;
        private Animator animator;

        private void FetchAnimator()
        {
            animator = GetComponentInChildren<Animator>();
            if (animator == null)
                GameLogger.Log(new GameLog(Log.LogType.Error,$"Missing animator on {gameObject.name} object"));

            animatedMesh = animator.transform;
        }

        public override void OnMasterEnabled(WeaponMaster master)
        {
            base.OnMasterEnabled(master);

            if (animator == null)
                FetchAnimator();

            Master.EventWeaponFired += PlayShootAnimation;
            Master.EventReloadStarted += PlayReloadAnimation;
        }

        private void OnDisable()
        {
            Master.EventWeaponFired -= PlayShootAnimation;
            Master.EventReloadStarted -= PlayReloadAnimation;

            ResetAnimatedMesh();
        }

        private void PlayShootAnimation(FireInputOrigin _)
        {
            animator.SetTrigger("shoot");
        }

        private void PlayReloadAnimation()
        {
            animator.SetTrigger("reload");
        }

        private void ResetAnimatedMesh()
        {
            animatedMesh.localPosition = Vector3.zero;
            animatedMesh.localRotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }
}
