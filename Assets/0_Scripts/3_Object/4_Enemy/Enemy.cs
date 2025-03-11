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
    using System.Linq;
    using Unity.VisualScripting;
    using UnityEngine;
    using static project02.EnemyData;

    public partial class Enemy : CombatObjectBase // Data Property
    {
        public override int Hp
        {
            get => hp;
            set
            {
                hp = value;
                if (hp > 0)
                    State = EnemyState.GetDamage;
                else
                    State = EnemyState.Death;

                MainSystem.Instance.SoundManager.SoundController.SpecialEffects.PlaySfx(SoundClipName.EnemyHit);
                infoUI.SetHpUI(hp, EnemyStatInformation.maxHp);
            }
        }
        private Transform target = null;
        public Transform Target
        {
            get => target;
            set
            {
                if (target != value)
                {
                    if (value == null)
                        State = EnemyState.Return;
                    else
                        State = EnemyState.Follow;
                    target = value;
                }
            }
        }
    }
    public partial class Enemy : CombatObjectBase // Data Field
    {
        private EnemyState state = EnemyState.Idle;
        public EnemyState State
        {
            get => state;
            set
            {
                if (state != value)
                {
                    if (state == EnemyState.Death)
                        return;

                    state = value;
                    switch (state)
                    {
                        case EnemyState.Attack:
                            IsAttack = true;
                            EnemyMovement.StopNavSetting();
                            EnemyMovement.SetTargeting(false);
                            EnemyAnimation.SetTrigger(EnemyAnimationParam.Attack);
                            break;

                        case EnemyState.GetDamage:
                            if (SuperArmor)
                                return;
                            EnemyMovement.StopNavSetting();
                            EnemyMovement.SetTargeting(false);
                            EnemyAnimation.SetTrigger(EnemyAnimationParam.GetDamage);
                            break;

                        case EnemyState.Death:
                            EnemyMovement.StopNavSetting();
                            EnemyMovement.SetTargeting(false);
                            EnemyAnimation.SetTrigger(EnemyAnimationParam.Death);
                            if (LastDamageSender != null)
                                LastDamageSender.Exp += EnemyInformation.drop_exp;
                            break;

                        default:
                            if (state != EnemyState.Death)
                                EnemyMovement.MoveNavSetting();
                            break;
                    }
                }
                state = value;
            }
        }

        public Vector3 hitPoint = default;
        public Player LastDamageSender = default;
        public EnemyInformation EnemyInformation { get; private set; } = default;
        public EnemyStatInformation EnemyStatInformation { get; private set; } = default;
        public Vector3 OriginPosition { get; set; } = Vector3.zero;
        public bool IsAttack { get; set; } = false;
        public bool SuperArmor { get; set; }

        [SerializeField] LayerMask deadLayer;

        private ZoneObject zone = default;
        private Transform playerTransform = default;
        private BossRoomDoor bossRoomDoor = default;

        [field: SerializeField] public EnemyMovement EnemyMovement { get; private set; } = default;
        [field: SerializeField] public EnemyHitDetector EnemyHitDetector { get; private set; } = default;
        [field: SerializeField] public EnemyCombat EnemyCombat { get; private set; } = default;
        [field: SerializeField] public EnemyAnimation EnemyAnimation { get; private set; } = default;
    }
    public partial class Enemy : CombatObjectBase // Initialize
    {
        private void Allocate()
        {
            EnemyInformation info = MainSystem.Instance.DataManager.EnemyData.enemyDataDict.Values.Where(elem => name == elem.name).SingleOrDefault();
            EnemyInformation = new EnemyInformation
            {
                index = info.index,
                name = info.name,
                drop_exp = info.drop_exp,
            };

            EnemyStatInformation stat = MainSystem.Instance.DataManager.EnemyStatData.GetData(gameObject.name);
            EnemyStatInformation = new EnemyStatInformation
            {
                level = stat.level,
                hp = stat.hp,
                maxHp = stat.maxHp,
                power = stat.power,
                criticalPercent = stat.criticalPercent,
                criticalIncreasePercent = stat.criticalIncreasePercent,
                defense = stat.defense,
                moveSpeed = stat.moveSpeed,
                targetingRange = stat.targetingRange,
                attackRange = stat.attackRange,
                skillUseDistance = stat.skillUseDistance,
                useable_skill = stat.useable_skill,
            };
            hp = EnemyStatInformation.hp;
            CalculateDamage(EnemyStatInformation);

            playerTransform = MainSystem.Instance.PlayerManager.Player.transform;
        }
        public override void Initialize()
        {
            base.Initialize();

            Allocate();
            Setup();

            if (gameObject.name == PoolObject.BossOrc.ToString())
                bossRoomDoor = GameObject.Find("BossRoomDoor").GetComponent<BossRoomDoor>();

            EnemyMovement.Initialize(this);
            if (EnemyHitDetector != null)
                EnemyHitDetector.Initialize(this);
            EnemyCombat.Initialize(this);
            EnemyAnimation.Initialize(this);
            infoUI.Initialize();
            InitializeUI();
        }
        private void Setup()
        {
            state = EnemyState.Idle;
        }
    }

    public partial class Enemy : CombatObjectBase // Main
    {
        private void Update()
        {
            if (state != EnemyState.Death)
            {
                DistanceCalculator();
                EnemyMovement.Progress();
                EnemyCombat.Progress();
                EnemyAnimation.Progress();
            }
        }
    }
    public partial class Enemy : CombatObjectBase // Property
    {
        public void SetZone(ZoneObject zoneValue)
        {
            zone = zoneValue;
        }

        public void Death()
        {
            Target = null;

            PoolObject name = Enum.Parse<PoolObject>(gameObject.name);
            BaseQuest.enemyKillCount[(int)name]++;
            if (bossRoomDoor != null)
                bossRoomDoor.IsBossDead = true;

            MainSystem.Instance.EnemyManager.SignDownEnemy(this);
            zone.Despawn(this);
        }
    }
    public partial class Enemy : CombatObjectBase // Private Property
    {
        private void DistanceCalculator()
        {
            if (Vector3.Distance(playerTransform.position, transform.position) <= EnemyStatInformation.targetingRange)
                Target = playerTransform;
            else
                Target = null;

            if (MainSystem.Instance.PlayerManager.Player.IsDead)
                Target = null;
        }
        private void InitializeUI()
        {
            infoUI.SetHpUI(Hp, EnemyStatInformation.maxHp);
        }
    }
}
