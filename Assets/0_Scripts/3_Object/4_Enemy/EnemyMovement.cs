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
    using UnityEngine.AI;

    public partial class EnemyMovement : MonoBehaviour // Data Field
    {

        [SerializeField] protected NavMeshAgent agent;
        protected NavMeshPath navMeshPath;
        protected int pathIndex;

        protected bool isTargeting = false;
        protected Enemy enemy;
        protected Vector3 destPosition;
        protected Vector3[] destpos;
    }
    public partial class EnemyMovement : MonoBehaviour // Initialzie
    {
        private void Allocate()
        {
            pathIndex = 0;
            navMeshPath = new NavMeshPath();
        }
        public virtual void Initialize(Enemy enemyValue)
        {
            enemy = enemyValue;

            Allocate();
            Setup();
        }
        private void Setup()
        {
            agent.isStopped = false;
            agent.stoppingDistance = 0;
            agent.angularSpeed = 0;
            agent.speed = enemy.EnemyStatInformation.moveSpeed;
        }
    }


    public partial class EnemyMovement : MonoBehaviour // Main
    {
        public virtual void Progress()
        {
            if (!enemy.IsAttack)
            {
                switch (enemy.State)
                {
                    case EnemyState.Idle:
                        StopNavSetting();
                        isTargeting = false;
                        if (enemy.Target != null)
                            enemy.State = EnemyState.Follow;
                        else
                        {
                            if (Vector3.Distance(transform.position, enemy.OriginPosition) > 2)
                                enemy.State = EnemyState.Return;
                        }

                        if (GetAttackableInRange())
                            enemy.State = EnemyState.Attack;
                        break;

                    case EnemyState.Follow:
                        MoveNavSetting();
                        SetDestPosition(enemy.Target.position);
                        Movement(EnemyState.Follow);
                        break;

                    case EnemyState.Return:
                        MoveNavSetting();
                        Movement(EnemyState.Return);
                        break;

                    case EnemyState.Attack:
                        if (enemy.Target != null)
                            StopNavSetting();
                        else
                            enemy.State = EnemyState.Idle;
                        break;

                    default:
                        agent.isStopped = true;
                        break;

                }
            }
            Targeting();
        }
    }

    public partial class EnemyMovement : MonoBehaviour // Property
    {
        public void SetTargeting(bool isTargetingValue)
        {
            isTargeting = isTargetingValue;
        }
        public bool GetAttackableInRange()
        {
            if (enemy.Target == null)
                return false;
            else
                return (enemy.Target.position - transform.position).magnitude <= enemy.EnemyStatInformation.attackRange;
        }
        public virtual bool GetUseableSkillRange()
        {
            return false;
        }
        public Vector3 GetRandomPositionNearPoint(Vector3 targetPosition)
        {
            Vector3 direction = (transform.position - targetPosition).normalized;
            direction *= enemy.EnemyStatInformation.attackRange * 0.5f;

            return UnityEngine.Random.onUnitSphere * enemy.EnemyStatInformation.attackRange * 0.5f + direction + targetPosition;
        }
        public void MoveNavSetting()
        {
            agent.isStopped = false;
            agent.avoidancePriority = 51;
        }
        public void StopNavSetting()
        {
            agent.isStopped = true;
            agent.velocity = Vector3.zero;
            agent.avoidancePriority = 50;
        }
    }

    public partial class EnemyMovement : MonoBehaviour // protected Property
    {
        protected void Targeting()
        {
            if (isTargeting)
            {
                transform.DOLookAt(GetLookTarget(), 0.5f).SetEase(Ease.Linear);
            }
        }

        protected void SetDestPosition(Vector3 targetPosition)
        {
            destPosition = GetRandomPositionNearPoint(targetPosition);
            destPosition.y = transform.position.y;
        }

        protected Vector3 GetLookTarget()
        {
            if (enemy.Target != null)
            {
                Vector3 lookTarget = enemy.Target.position;
                lookTarget.y = transform.position.y;
                return lookTarget;
            }
            else
            {
                Vector3 lookTarget = enemy.OriginPosition;
                lookTarget.y = transform.position.y;
                return lookTarget;
            }
        }

        protected void OnMovement(Vector3 destPosition)
        {
            if (NavMesh.CalculatePath(transform.position, destPosition, NavMesh.AllAreas, navMeshPath))
            {
                if (pathIndex < navMeshPath.corners.Length)
                {
                    Vector3 nextWayPoint = navMeshPath.corners[pathIndex];
                    agent.avoidancePriority = 51;
                    transform.position = Vector3.MoveTowards(transform.position, nextWayPoint, enemy.EnemyStatInformation.moveSpeed * Time.deltaTime);

                    float distanceToWayPoint = Vector3.Distance(transform.position, nextWayPoint);
                    if (distanceToWayPoint < 0.1f)
                        pathIndex++;
                }
            }
            if (Mathf.Approximately((transform.position - destPosition).magnitude, 0))
                pathIndex = 0;
        }

        protected virtual void Movement(EnemyState state)
        {
            if (GetAttackableInRange())
                enemy.State = EnemyState.Attack;

            else
            {
                switch (state)
                {
                    case EnemyState.Follow:
                        if ((enemy.Target.position - destPosition).magnitude > enemy.EnemyStatInformation.attackRange)
                            SetDestPosition(enemy.Target.position);

                        isTargeting = true;
                        break;

                    case EnemyState.Return:
                        destPosition = enemy.OriginPosition;
                        isTargeting = true;

                        if (Vector3.Distance(transform.position, destPosition) <= 1)
                        {
                            isTargeting = false;
                            enemy.State = EnemyState.Idle;
                        }
                        break;
                }
                agent.SetDestination(destPosition);
            }
        }
    }
}
