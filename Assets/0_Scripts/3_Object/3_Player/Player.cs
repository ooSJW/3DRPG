/*
	* Coder :
	* Last Update :
	* Information
*/
namespace project02
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using static project02.PlayerStatData;


    public partial class Player : CombatObjectBase // Data Property
    {
        private PlayerStatInformation playerStatInformation;
        public PlayerStatInformation PlayerStatInformation
        {
            get => playerStatInformation;
            private set
            {
                playerStatInformation = new PlayerStatInformation()
                {
                    index = value.index,
                    level = int.Parse(value.index),
                    exp = value.exp,
                    maxExp = value.maxExp,
                    hp = value.hp,
                    maxHp = value.maxHp,
                    moveSpeed = value.moveSpeed,
                    maxSpeed = value.maxSpeed,
                    evadeSpeed = value.evadeSpeed,
                    acceleration = value.acceleration,
                    skillPoint = value.skillPoint,
                    power = value.power,
                    defense = value.defense,
                    criticalPercent = value.criticalPercent,
                    criticalIncreasePercent = value.criticalIncreasePercent,
                    useable_skill = value.useable_skill,
                };
                Hp = value.hp;
                Defense = value.defense;
                CalculateDamage(PlayerStatInformation);
            }
        }

        private int exp;
        public int Exp
        {
            get => exp;
            set
            {
                exp = value;
                while (exp >= PlayerStatInformation.maxExp)
                {
                    exp -= PlayerStatInformation.maxExp;
                    Level++;
                    SkillPoint += PlayerStatInformation.skillPoint;
                    MainSystem.Instance.SoundManager.SoundController.SpecialEffects.PlaySfx(SoundClipName.LevelUp);
                }
                infoUI.SetExpText(exp, PlayerStatInformation.maxExp);
            }
        }
        public override int Level
        {
            get => level;
            set
            {
                level = value;
                PlayerStatInformation = MainSystem.Instance.DataManager.PlayerStatData.GetData(level.ToString());
                MainSystem.Instance.PlayerManager.Save();
                infoUI.SetLevelText(level);
            }
        }

        public override int Hp
        {
            get => hp;
            set
            {
                hp = value;
                if (hp <= 0)
                {
                    State = PlayerState.Death;
                    MainSystem.Instance.PlayerManager.Save();
                }
                int uiHp = Mathf.Clamp(hp, 0, PlayerStatInformation.maxHp);
                infoUI.SetHpUI(uiHp, PlayerStatInformation.maxHp);
                infoUI.SetHPText(uiHp, PlayerStatInformation.maxHp);
            }
        }

        private PlayerState state = PlayerState.Idle;
        public PlayerState State
        {
            get => state;
            set
            {
                if (state != value)
                {
                    if (state == PlayerState.Death)
                        return;

                    state = value;

                    switch (state)
                    {
                        case PlayerState.Evade:
                            PlayerAnimation.SetAnimationStateTrigger(PlayerState.Evade);
                            PlayerMovement.Evade();
                            break;

                        case PlayerState.Stun:
                            PlayerInput.Moveable = false;
                            PlayerAnimation.SetAnimationStateTrigger(PlayerState.Stun);
                            break;

                        case PlayerState.Death:
                            IsDead = true;
                            PlayerAnimation.SetAnimationStateTrigger(PlayerState.Death);
                            break;
                    }
                }
                state = value;
            }
        }

        private bool isDead = false;
        public bool IsDead
        {
            get => isDead;
            set
            {
                if (isDead != value)
                {
                    MainSystem.Instance.UIManager.UIController.IsPlayerDead = value;
                }
                isDead = value;
            }
        }

        private SkillName playerSkill = SkillName.None;
        public SkillName PlayerSkill
        {
            get => playerSkill;
            set
            {
                if (playerSkill != value)
                {
                    playerSkill = value;
                    if (playerSkill != SkillName.None)
                    {
                        State = PlayerState.Attack;
                        PlayerCombat.IsAttacking = true;
                        PlayerAnimation.SetAttackAnimation(playerSkill);
                        PlayerSkillDict[playerSkill].IsCoolTime = true;
                    }
                }
                playerSkill = value;
            }
        }

        private PlayerWeaponState weaponState = PlayerWeaponState.Unequip;
        public PlayerWeaponState WeaponState
        {
            get => weaponState;
            set
            {
                if (weaponState != value)
                {
                    weaponState = value;
                    PlayerAnimation.SetPlayerWeaponStateAnimation();
                }
            }
        }
        private int skillPoint;
        public int SkillPoint
        {
            get => skillPoint; set { skillPoint = value; LocalData.Instance.Save(); }
        }

        private bool closeToHealer = false;
        public bool CloseToHealer { get => closeToHealer; set => closeToHealer = value; }
    }
    public partial class Player : CombatObjectBase
    {
        public Dictionary<SkillName, SkillBase> PlayerSkillDict { get; private set; }
        public bool IsGround { get; set; } = false;
        [field: SerializeField] public List<SkillBase> PlayerSkillList { get; private set; }

        [field: SerializeField] public PlayerInput PlayerInput { get; private set; } = default;
        [field: SerializeField] public PlayerQuest PlayerQuest { get; private set; } = default;
        [field: SerializeField] public PlayerGroundChecker PlayerGroundChecker { get; private set; } = default;
        [field: SerializeField] public PlayerMovement PlayerMovement { get; private set; } = default;
        [field: SerializeField] public PlayerCombat PlayerCombat { get; private set; } = default;
        [field: SerializeField] public PlayerRotation PlayerRotation { get; private set; } = default;
        [field: SerializeField] public PlayerAnimation PlayerAnimation { get; private set; } = default;

    }
    public partial class Player : CombatObjectBase
    {
        private void Allocate()
        {
            if (!PlayerPrefs.HasKey("level"))
            {
                level = 1;
                skillPoint = 1;
            }
            else
                level = PlayerPrefs.GetInt("level");

            PlayerStatInformation = MainSystem.Instance.DataManager.PlayerStatData.GetData(level.ToString());
            PlayerSkillDict = new Dictionary<SkillName, SkillBase>();
        }
        public override void Initialize()
        {
            base.Initialize();
            Allocate();
            Setup();
            PlayerQuest.Initialize(this);
            PlayerInput.Initialize(this);
            PlayerGroundChecker.Initialize(this);
            PlayerMovement.Initialize(this);
            PlayerCombat.Initialize(this);

            SkillInitialize();
            PlayerRotation.Initialize(this);
            PlayerAnimation.Initialize(this);
            infoUI.Initialize();
            InitializeUI();
        }

        private void Setup()
        {
            weaponObject.SetActive(false);
        }
    }
    public partial class Player : CombatObjectBase
    {
        private void Update()
        {
            if (state != PlayerState.Death)
            {
                PlayerInput.Progress();
                PlayerMovement.Progress();
                PlayerRotation.Progress();
                PlayerCombat.Progress();
                PlayerAnimation.Progress();
            }
        }
        private void FixedUpdate()
        {
            if (state != PlayerState.Death)
            {
                PlayerInput.FixedProgress();
                PlayerMovement.FixedProgress();
            }
        }
    }


    public partial class Player : CombatObjectBase // Private Property
    {
        private void InitializeUI()
        {
            infoUI.SetLevelText(Level);
            infoUI.SetExpText(Exp, PlayerStatInformation.maxExp);
            infoUI.SetHPText(Hp, PlayerStatInformation.maxHp);
            infoUI.SetHpUI(Hp, PlayerStatInformation.maxHp);
        }
        private void SkillInitialize()
        {   // 초기화 시 Json에 선언된 내용을 스킬에 적용 및 참조
            string nameSpace = typeof(Player).Namespace;
            for (int i = 0; i < PlayerStatInformation.useable_skill.Length; i++)
            {
                string skillName = PlayerStatInformation.useable_skill[i];
                Type skill = Type.GetType(nameSpace + "." + skillName);

                if (skill is not null)
                {
                    SkillBase skillBase = gameObject.AddComponent(skill) as SkillBase;

                    if (skillBase is not null)
                    {
                        skillBase.Initialize(this);
                        PlayerSkillDict.Add(skillBase.GetSkillName(), skillBase);
                        InputKey resultKey = InputKey.none;
                        for (int j = 0; j < skillBase.SkillInfo.command.Length; j++)
                        {
                            if (Enum.TryParse<InputKey>(skillBase.SkillInfo.command[j], true, out InputKey parseKey))
                                resultKey |= parseKey;
                            else
                                Debug.Log("Parse Error");
                        }
                        if (!PlayerInput.commandDict.ContainsKey((int)resultKey))
                            PlayerInput.commandDict.Add((int)resultKey, skillBase);
                        else
                            Debug.Log("중복된 키 조합 등록 시도 :" + resultKey);
                    }
                }
            }
        }

    }


    public partial class Player : CombatObjectBase // Property
    {
        public void ChangeWeaponState()
        {
            if (weaponObject != null)
            {
                bool isActive = weaponObject.activeSelf ? true : false;
                weaponObject.SetActive(!isActive);
            }
        }
    }
}
