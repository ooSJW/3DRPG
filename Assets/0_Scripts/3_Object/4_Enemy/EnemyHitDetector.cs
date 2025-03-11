/*
	* Coder :
	* Last Update :
	* Information
*/
namespace project02
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public partial class EnemyHitDetector : MonoBehaviour // Data Field
    {
        protected Enemy enemy;
        [SerializeField] protected Collider hitDetector;
    }


    public partial class EnemyHitDetector : MonoBehaviour // Initialize
    {
        private void Allocate()
        {

        }
        public virtual void Initialize(Enemy enemyValue)
        {
            enemy = enemyValue;

            Allocate();
            Setup();
        }
        private void Setup()
        {
            if (hitDetector != null)
            {
                hitDetector.isTrigger = true;
                hitDetector.enabled = false;
            }
        }
    }

    public partial class EnemyHitDetector : MonoBehaviour // Property
    {
        public void Attack()
        {
            hitDetector.enabled = true;
            MainSystem.Instance.SoundManager.SoundController.SpecialEffects.PlaySfx(SoundClipName.EnemyAttack);
        }

        public void EndAttack()
        {
            hitDetector.enabled = false;
        }
    }

    public partial class EnemyHitDetector : MonoBehaviour // TriggerEvent
    {
        protected virtual void OnTriggerEnter(Collider other)
        {
            Player player = other.GetComponent<Player>();
            Vector3 hitPoint = other.ClosestPoint(transform.position);
            hitPoint.y += 0.8f;
            MainSystem.Instance.PoolManager.Spawn(PoolObject.PlayerHitEffect.ToString(), null, hitPoint);
            enemy.SendDamage(player, enemy.EnemyStatInformation);
        }
    }
}
