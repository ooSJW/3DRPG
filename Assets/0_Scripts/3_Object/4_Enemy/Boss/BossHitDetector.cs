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

    public partial class BossHitDetector : EnemyHitDetector // DataField
    {

    }

    public partial class BossHitDetector : EnemyHitDetector // Initialize
    {
        private void Allocate()
        {

        }
        public override void Initialize(Enemy enemyValue)
        {
            base.Initialize(enemyValue);
            Allocate();
            Setup();
        }
        private void Setup()
        {

        }
    }

    public partial class BossHitDetector : EnemyHitDetector // Trigger Evenet
    {
        protected override void OnTriggerEnter(Collider other)
        {
            Player player = other.GetComponent<Player>();

            player.State = PlayerState.Stun;
            Vector3 hitPoint = other.ClosestPoint(transform.position);
            hitPoint.y += 0.8f;
            MainSystem.Instance.PoolManager.Spawn(PoolObject.PlayerHitEffect.ToString(), null, hitPoint);
            enemy.SendDamage(player, enemy.EnemyStatInformation);
        }
    }
}
