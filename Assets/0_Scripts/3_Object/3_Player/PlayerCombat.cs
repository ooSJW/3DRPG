/*
	* Coder :
	* Last Update :
	* Information
*/
namespace project02
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Unity.VisualScripting;
    using UnityEditor;
    using UnityEngine;
    using UnityEngine.UIElements;
    using static project02.PlayerStatData;

    public partial class PlayerCombat : MonoBehaviour // Data Field
    {
        private Player player;
        private List<Enemy> hitEnemyList;

        public float r;
        public float g;

        public bool IsAttack { get; set; } = false;

        [SerializeField] private LayerMask layer;
    }
    public partial class PlayerCombat : MonoBehaviour // Initialize
    {
        private void Allocate()
        {
            hitEnemyList = new List<Enemy>();
        }
        public void Initialize(Player playerValue)
        {
            player = playerValue;

            Allocate();
            Setup();
        }
        private void Setup()
        {

        }
    }

    public partial class PlayerCombat : MonoBehaviour // Progress
    {
        public void Progress()
        {
            if (player.State == PlayerState.Attack && player.PlayerSkill == SkillName.None)
            {
                player.State = PlayerState.Idle;
                IsAttack = false;
            }
        }
    }


    public partial class PlayerCombat : MonoBehaviour // Property
    {
        public void HitEnemyFilter()
        {
            MainSystem.Instance.SoundManager.SoundController.SpecialEffects.PlaySfx(SoundClipName.PlayerShoot);
            if (player.PlayerSkillDict.ContainsKey(player.PlayerSkill))
            {
                SkillInformation skillInfo = player.PlayerSkillDict[player.PlayerSkill].SkillInfo;
                Vector3 center = transform.position - (transform.forward * 0.5f);
                Collider[] targetCollider = Physics.OverlapSphere(center, skillInfo.range, layer);


                for (int i = 0; i < targetCollider.Length; ++i)
                {
                    Vector3 direction = targetCollider[i].transform.position - center;

                    float angle = Vector3.Angle(transform.forward, direction);

                    if (angle <= skillInfo.angle_range * 0.5f)
                    {
                        hitEnemyList.Add(targetCollider[i].GetComponent<Enemy>());

                        Vector3 hitPoint = targetCollider[i].ClosestPoint(transform.position);
                        hitPoint.y += 1;

                        MainSystem.Instance.PoolManager.Spawn(PoolObject.EnemyHitEffect.ToString(), null, hitPoint);
                    }
                }
                SendDamage();
            }
        }

        public void MeleeFilter()
        {
            MainSystem.Instance.SoundManager.SoundController.SpecialEffects.PlaySfx(SoundClipName.PlayerMelee);
            SkillInformation skillInfo = player.PlayerSkillDict[player.PlayerSkill].SkillInfo;
            Vector3 center = transform.position;
            Collider[] targetCollider = Physics.OverlapSphere(center, skillInfo.melee_range, layer);

            for (int i = 0; i < targetCollider.Length; ++i)
            {
                Vector3 direction = targetCollider[i].transform.position - center;

                float angle = Vector3.Angle(transform.forward, direction);

                if (angle <= 180 * 0.5f)
                {
                    hitEnemyList.Add(targetCollider[i].GetComponent<Enemy>());

                    Vector3 hitPoint = targetCollider[i].ClosestPoint(transform.position);
                    hitPoint.y += 1;

                    MainSystem.Instance.PoolManager.Spawn(PoolObject.EnemyHitEffect.ToString(), null, hitPoint);
                }
            }
            SendDamage();
        }

        public void SendDamage()
        {
            PlayerStatInformation statInfo = player.PlayerStatInformation;
            float skillDamage = player.PlayerSkillDict[player.PlayerSkill].SkillDamage;

            for (int i = 0; i < hitEnemyList.Count; ++i)
            {
                hitEnemyList[i].LastDamageSender = player;
                player.SendDamage(hitEnemyList[i], statInfo, skillDamage);
            }
            hitEnemyList.Clear();
        }

        public void EndAttack()
        {
            IsAttack = false;
        }

        public void ReturnState()
        {
            player.PlayerSkill = SkillName.None;
            player.State = PlayerState.Idle;
            IsAttack = false;
            player.PlayerMovement.isEvade = false;
            player.PlayerInput.CanMove = true;
        }
    }
}
