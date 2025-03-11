/*
	* Coder :
	* Last Update :
	* Information
*/
namespace project02
{
    using DG.Tweening;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public partial class EnemyAnimation : MonoBehaviour // Data Field
    {

    }
    public partial class EnemyAnimation : MonoBehaviour // Data Field
    {
        private Enemy enemy;
        [SerializeField] Animator animator;
    }

    public partial class EnemyAnimation : MonoBehaviour // Initialize
    {
        private void Allocate()
        {

        }
        public void Initialize(Enemy enemyValue)
        {
            enemy = enemyValue;

            Allocate();
            Setup();
        }
        private void Setup()
        {

        }
    }

    public partial class EnemyAnimation : MonoBehaviour // Main
    {
        public void Progress()
        {
            animator.SetInteger(EnemyAnimationParam.State.ToString(), (int)enemy.State);
        }
    }

    public partial class EnemyAnimation : MonoBehaviour // Property
    {
        public void SetTrigger(EnemyAnimationParam param)
        {
            animator.SetTrigger(param.ToString());
        }

        public void SetTargeting(int isTargetingValue)
        {
            bool isTargeting = Convert.ToBoolean(isTargetingValue);
            enemy.EnemyMovement.SetTargeting(isTargeting);
        }

        public void Attack()
        {
            enemy.EnemyHitDetector.Attack();
        }
        public void EndAttack()
        {
            enemy.EnemyHitDetector.EndAttack();
        }

        public void ReturnState()
        {
            enemy.EnemyCombat.BossSkill = SkillName.None;
            enemy.State = EnemyState.Idle;
            enemy.IsAttack = false;
        }

        public void Knockback()
        {
            if (enemy.Target != null)
            {
                Vector3 knockbackDirection = (transform.position - enemy.Target.position).normalized;
                enemy.transform.DOMove(transform.position + knockbackDirection * 0.5f, 0.3f).SetEase(Ease.OutBack);
            }
        }
 
        public void Death()
        {
            enemy.Death();
        }
    }
    public partial class EnemyAnimation : MonoBehaviour // Boss Property
    {
        public void WindSkill()
        {
            enemy.EnemyCombat.WindSkill();
        }

        public void BaseAttackFilter()
        {
            enemy.EnemyCombat.BaseAttackFilter();
        }

        public void SkillFilter()
        {
            enemy.EnemyCombat.SkillFilter();
        }

        public void UseSkill(SkillName skillName)
        {
            animator.SetTrigger(skillName.ToString());
        }
    }
}
