using BanditDynamite;
using RoR2;
using RoR2.Projectile;
using UnityEngine;

namespace EntityStates.Moffein.BanditDynamite
{
    public class ClusterBomb : BaseState
    {
        public override void OnEnter()
        {
            base.OnEnter();
            duration = baseDuration / attackSpeedStat;
            Ray aimRay = GetAimRay();
            StartAimMode(aimRay, 2f, false);
            PlayAnimation("Gesture, Additive", "SlashBlade", "SlashBlade.playbackRate", duration);
            Util.PlaySound("Play_MoffeinBanditDynamite_toss", gameObject);
            if (isAuthority)
            {
                if (characterMotor && !characterMotor.isGrounded)
                {
                    characterMotor.velocity = new Vector3(characterMotor.velocity.x, Mathf.Max(characterMotor.velocity.y, 6), characterMotor.velocity.z);   //Bandit2 FireShiv Shorthop Velocity = 6
                }
                // big jank way of fixing this until persuadopulse give me an actual fix
                ProjectileManager.instance.FireProjectile(projectilePrefab, aimRay.origin + (aimRay.direction * 1.5f), Util.QuaternionSafeLookRotation(aimRay.direction), gameObject, damageStat * damageCoefficient, 0f, Util.CheckRoll(critStat, characterBody.master), DamageColorIndex.Default, null, -1f);
            }
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if (fixedAge >= duration && isAuthority)
            {
                outer.SetNextStateToMain();
                return;
            }
        }

        public override InterruptPriority GetMinimumInterruptPriority()
        {
            if (inputBank && inputBank.skill2.down)
            {
                return InterruptPriority.PrioritySkill;
            }
            return InterruptPriority.Skill;
        }

        public static GameObject projectilePrefab;
        public static float damageCoefficient;
        public static float force = 2500f;
        public static float baseDuration;
        public static float bombletDamageCoefficient;
        private float duration;
        public static bool quickdrawEnabled = false;
    }
}