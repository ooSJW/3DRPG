/*
	* Coder :
	* Last Update :
	* Information
*/
namespace project02
{
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Unity.VisualScripting;
    using UnityEngine;
    using UnityEngine.UIElements;

    public partial class SkillBase : MonoBehaviour // Data Filed
    {
        protected CombatObjectBase owner;
        protected SkillInformation skillInfo = default;

        public float currentCoollingTime;

        public SkillInformation SkillInfo { get => skillInfo; private set => skillInfo = value; }
        private bool isCoolTIme = false;
        public bool IsCoolTime
        {
            get => isCoolTIme;
            set
            {
                isCoolTIme = value;
                if (isCoolTIme)
                {
                    MainSystem.Instance.UIManager.UIController.SkillCooltimeUI.SignUpCoolTimeSkill(skillName);
                    currentCoollingTime = skillInfo.cool_time;
                }
            }
        }
        public int SkillLevel
        {
            get => skillInfo.level;
            set
            {
                skillInfo.level = value;
                JObject jobject = new JObject();
                jobject.Add(skillName.ToString(), SkillLevel);
                LocalData.Instance.SaveSkill(skillName, jobject);
                CalculateDamage(skillInfo);
            }
        }

        protected SkillName skillName = SkillName.None;

        public float SkillDamage { get; private set; }
    }

    public partial class SkillBase : MonoBehaviour // Initialize
    {
        private void Allocate()
        {

            SkillInformation originInfo = MainSystem.Instance.DataManager.SkillData.GetData(skillName.ToString());
            skillInfo = new SkillInformation()
            {
                index = originInfo.index,
                command = originInfo.command,
                commandInUI = originInfo.commandInUI,
                power = originInfo.power,
                power_growth = originInfo.power_growth,
                level = originInfo.level,
                max_level = originInfo.max_level,
                cool_time = originInfo.cool_time,
                active_time = originInfo.active_time,
                melee_range = originInfo.melee_range,
                range = originInfo.range,
                angle_range = originInfo.angle_range,
                icon_path = originInfo.icon_path,
                effect_path = originInfo.effect_path,
            };
            CalculateDamage(skillInfo);
        }

        public virtual void Initialize(CombatObjectBase combatObjectValue)
        {
            owner = combatObjectValue;
            Allocate();
            Setup();
        }
        private void Setup()
        {

        }
    }

    public partial class SkillBase : MonoBehaviour // Main
    {
        private void Update()
        {
            if (IsCoolTime)
            {
                currentCoollingTime -= Time.deltaTime;
                if (currentCoollingTime <= 0)
                    IsCoolTime = false;
            }
        }
    }
    public partial class SkillBase : MonoBehaviour // Property
    {
        public SkillName GetSkillName()
        {
            return skillName;
        }

        public void CalculateDamage(SkillInformation skillInfo)
        {
            SkillDamage = skillInfo.power_growth * skillInfo.level + skillInfo.power;
        }
    }
}
