/*
	* Coder :
	* Last Update :
	* Information
*/
namespace project02
{
    using Newtonsoft.Json.Linq;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using static project02.PlayerStatData;

    public partial class PlayerManager : MonoBehaviour // Data Field
    {
        public Player Player { get; private set; } = default;
    }
    public partial class PlayerManager : MonoBehaviour // Initialize
    {
        private void Allocate()
        {

        }
        public void Initialize()
        {
            Allocate();
            Setup();
        }
        private void Setup()
        {

        }
    }
    public partial class PlayerManager : MonoBehaviour // Sign
    {
        public void SignUpPlayer(Player playerValue)
        {
            Player = playerValue;
            Player.Initialize();
        }
        public void SignDownPlayer()
        {
            Player = null;
        }
    }
    public partial class PlayerManager : MonoBehaviour // Property
    {
        public void Save()
        {
            JObject jobject = new JObject();
            jobject.Add("questIndex", MainSystem.Instance.QuestManager.QuestController.CurrentQuestIndex);
            LocalData.Instance.Save(jobject, SaveData());
        }
        public PlayerStatInformation SaveData()
        {
            PlayerStatInformation info = Player.PlayerStatInformation;
            PlayerStatInformation saveData = new PlayerStatInformation()
            {
                index = info.index,
                level = int.Parse(info.index),
                maxExp = info.maxExp,
                maxHp = info.maxHp,
                moveSpeed = info.moveSpeed,
                maxSpeed = info.maxSpeed,
                evadeSpeed = info.evadeSpeed,
                acceleration = info.acceleration,
                power = info.power,
                criticalPercent = info.criticalPercent,
                criticalIncreasePercent = info.criticalIncreasePercent,
                useable_skill = info.useable_skill,
                defense = Player.Defense,
                skillPoint = Player.SkillPoint,
                hp = Player.Hp,
                exp = Player.Exp,
            };
            return saveData;
        }
    }
}
