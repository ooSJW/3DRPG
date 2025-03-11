/*
	* Coder :
	* Last Update :
	* Information
*/
namespace project02
{
    using DG.Tweening;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public partial class BossMovement : EnemyMovement // Data Field
    {

    }
    public partial class BossMovement : EnemyMovement // Initialize
    {
        private void Allocate()
        {

        }
        public override void Initialize(Enemy enemyValue)
        {
            base.Initialize(enemyValue);
            Allocate();
            Setup();
            enemyValue.SuperArmor = true;
        }
        private void Setup()
        {

        }
    }
    public partial class BossMovement : EnemyMovement // main
    {
        public override void Progress()
        {
            if (!enemy.IsAttack)
            {
                switch (enemy.State)
                {
                    case EnemyState.Idle:
                        StopNavSetting();
                        if (enemy.Target != null)
                            enemy.State = EnemyState.Follow;
                        else
                        {
                            if (Vector3.Distance(transform.position, enemy.OriginPosition) > 2)
                                enemy.State = EnemyState.Return;
                        }
                        break;

                    case EnemyState.Follow:
                        MoveNavSetting();
                        Movement(EnemyState.Follow);
                        break;

                    case EnemyState.Return:
                        MoveNavSetting();
                        Movement(EnemyState.Return);
                        break;

                    case EnemyState.Attack:
                        if (enemy.Target != null)
                        {
                            StopNavSetting();
                            enemy.EnemyCombat.skillEnable = true;
                        }
                        else
                            enemy.State = EnemyState.Idle;
                        break;

                    default:
                        StopNavSetting();
                        break;

                }
            }
            Targeting();
        }
    }


    public partial class BossMovement : EnemyMovement // Property
    {
        public override bool GetUseableSkillRange()
        {
            if (enemy.Target == null)
                return false;
            else
                return (enemy.Target.position - transform.position).magnitude <= enemy.EnemyStatInformation.skillUseDistance;
        }
        protected override void Movement(EnemyState state)
        {
            if (GetUseableSkillRange())
            {
                enemy.EnemyCombat.skillEnable = true;
                if (GetAttackableInRange())
                {
                    enemy.State = EnemyState.Attack;
                    return;
                }
            }

            switch (state)
            {
                case EnemyState.Follow:
                    if ((enemy.Target.position - destPosition).magnitude > enemy.EnemyStatInformation.attackRange)
                        SetDestPosition(enemy.Target.position);
                    isTargeting = true;
                    break;

                case EnemyState.Return:
                    destPosition = enemy.OriginPosition;
                    destPosition.y = transform.position.y;
                    isTargeting = true;

                    if (Mathf.Approximately((transform.position - destPosition).magnitude, 0))
                        enemy.State = EnemyState.Idle;
                    break;
            }

            agent.SetDestination(destPosition);
        }
    }
}
