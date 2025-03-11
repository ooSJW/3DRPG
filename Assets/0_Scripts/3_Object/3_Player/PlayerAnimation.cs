/*
	* Coder :
	* Last Update :
	* Information
*/
namespace project02
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public partial class PlayerAnimation : MonoBehaviour // Data Field
    {
        private Player player;

        [SerializeField] private Animator animator;

        [field: SerializeField] public SoundClipName DefaultFootStep { get; set; }
        public SoundClipName currentFootStep;

    }
    public partial class PlayerAnimation : MonoBehaviour // Initialize
    {
        private void Allocate()
        {

        }
        public void Initialize(Player playerValue)
        {
            Allocate();
            Setup();
            player = playerValue;
        }
        private void Setup()
        {

        }
    }
    public partial class PlayerAnimation : MonoBehaviour // Progress
    {
        public void Progress()
        {
            SetAnimationState();
        }
    }
    public partial class PlayerAnimation : MonoBehaviour // Property
    {
        public void SetCanMove(int canMoveValue)
        {
            player.PlayerInput.SetCanMove(Convert.ToBoolean(canMoveValue));
        }

        public void SetPlayerWeaponStateAnimation()
        {
            animator.SetTrigger(player.WeaponState.ToString());
        }

        public void SetAnimationStateTrigger(PlayerState playerStateValue)
        {
            PlayerInput playerInput = player.PlayerInput;
            Vector3 direction = new Vector3(playerInput.GetAxisRawX(), 0, playerInput.GetAxisRawZ()).normalized;

            animator.SetFloat(PlayerAnimationParam.VelocityX.ToString(), direction.x);
            animator.SetFloat(PlayerAnimationParam.VelocityZ.ToString(), direction.z);

            animator.SetTrigger(playerStateValue.ToString());
        }

        public void ChangeWeaponState()
        {
            player.ChangeWeaponState();
        }
        public void SetAttackAnimation(SkillName playerSkillValue)
        {
            if (playerSkillValue != SkillName.None)
                animator.SetTrigger(playerSkillValue.ToString());
        }

        public void SendDamage()
        {
            player.PlayerCombat.HitEnemyFilter();
        }
        public void Melee()
        {
            player.PlayerCombat.MeleeFilter();
        }
        public void EndAttack()
        {
            player.PlayerCombat.EndAttack();
        }
        public void ReturnState()
        {
            player.PlayerCombat.ReturnState();
        }
        public void PlayFootStep()
        {
            MainSystem.Instance.SoundManager.SoundController.SpecialEffects.PlaySfx(currentFootStep);
        }
    }
    public partial class PlayerAnimation : MonoBehaviour // Private Property
    {
        private void SetAnimationState()
        {
            if (player.State != PlayerState.Death)
            {
                animator.SetInteger(PlayerAnimationParam.State.ToString(), (int)player.State);

                PlayerInput playerInput = player.PlayerInput;
                float vertical = playerInput.GetAxisRawZ();
                float horizontal = playerInput.GetAxisRawX();

                if (player.State != PlayerState.Evade)
                {
                    if (playerInput.RunKeyPress())
                    {
                        vertical *= 2;
                        horizontal *= 2;
                    }
                    animator.SetFloat(PlayerAnimationParam.VelocityZ.ToString(), vertical);
                    animator.SetFloat(PlayerAnimationParam.VelocityX.ToString(), horizontal);
                }
            }
        }
    }
}