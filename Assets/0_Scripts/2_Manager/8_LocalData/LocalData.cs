/*
	* Coder :
	* Last Update :
	* Information
*/
namespace project02
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using UnityEngine;
    using static project02.PlayerStatData;

    public class UserInformation
    {
        public string nickName;
    }
    public class PlayerInformation
    {
        public int level;
        public int hp;
        public int exp;
        public string quest_index;
    }

    public struct SkillKeyValue
    {
        public string name;
        public int level;
    }

    public class PlayerSkillInformation
    {
        public string skill_json;
        public int skill_point;
    }

    public partial class LocalData : GenericSingleton<LocalData>
    {
        private UserInformation userInformation = new UserInformation();
        private PlayerInformation playerInformation = new PlayerInformation();
        private PlayerSkillInformation playerSkillInformation = new PlayerSkillInformation();
    }

    public partial class LocalData : GenericSingleton<LocalData>
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

    public partial class LocalData : GenericSingleton<LocalData>
    {

        public void Save(JObject jobject = null, IDataKey data = null)
        {
            if (data is PlayerStatInformation)
            {
                PlayerStatInformation playerData = (PlayerStatInformation)data;

                string questIndex = jobject.TryGetValue("questIndex", out JToken value) ?
                    value.ToObject<string>() : string.Empty;

                playerInformation = new PlayerInformation()
                {
                    level = playerData.level,
                    hp = playerData.hp <= 0 ? 1 : playerData.hp,
                    exp = playerData.exp,
                    quest_index = questIndex,
                };

                QuestState questState = MainSystem.Instance.QuestManager.QuestController.GetCurrentQuest().QuestState;

                PlayerPrefs.SetInt("level", playerInformation.level);
                PlayerPrefs.SetInt("hp", playerInformation.hp);
                PlayerPrefs.SetInt("exp", playerInformation.exp);
                PlayerPrefs.SetString("questIndex", playerInformation.quest_index);
                PlayerPrefs.SetInt("questState", (int)questState);
            }

            /*List<SkillKeyValue> skillKeyList = new List<SkillKeyValue>();

            playerSkillInformation = new PlayerSkillInformation()
            {
                skill_json = JsonConvert.SerializeObject(skillKeyList.ToArray()),
                skill_point = MainSystem.Instance.PlayerManager.Player.SkillPoint,
            }
                PlayerPrefs.SetInt("skillPoint", playerSkillInformation.skill_point);
            ;*/

            int skill_point = MainSystem.Instance.PlayerManager.Player.SkillPoint;

            PlayerPrefs.SetInt("skillPoint", skill_point);

            PlayerPrefs.Save();
        }

        public void SaveSkill(SkillName name, JObject jobject)
        {

            int skillLevel = jobject.TryGetValue(name.ToString(), out JToken value) ?
                    value.ToObject<int>() : 1;

            /*List<SkillKeyValue> skillKeyList = new List<SkillKeyValue>();

            skillKeyList.Add(new SkillKeyValue()
            {
                name = name.ToString(),
                level = skillLevel,
            });*/

            PlayerPrefs.SetInt(name.ToString(), skillLevel);

            PlayerPrefs.Save();
        }

        public void SaveQuest()
        {
            int[] questArr = BaseQuest.enemyKillCount;
            for (int i = 0; i < questArr.Length; i++)
            {
                string enemyIndex = "enemy" + i.ToString();
                PlayerPrefs.SetInt(enemyIndex, questArr[i]);
            }
        }

        public void Load()
        {
            if (PlayerPrefs.HasKey("hp"))
            {
                MainSystem.Instance.PlayerManager.Player.Hp = PlayerPrefs.GetInt("hp");
                MainSystem.Instance.PlayerManager.Player.Exp = PlayerPrefs.GetInt("exp");
                MainSystem.Instance.PlayerManager.Player.SkillPoint = PlayerPrefs.GetInt("skillPoint");

                foreach (SkillName skill in MainSystem.Instance.PlayerManager.Player.PlayerSkillDict.Keys)
                {
                    if (PlayerPrefs.GetInt(skill.ToString()) < MainSystem.Instance.DataManager.SkillData.GetData(skill.ToString()).level)
                        continue;

                    MainSystem.Instance.PlayerManager.Player.PlayerSkillDict[skill].SkillLevel = PlayerPrefs.GetInt(skill.ToString());
                }
                string questIndex = PlayerPrefs.GetString("questIndex");
                MainSystem.Instance.QuestManager.QuestController.CurrentQuestIndex = questIndex;
                MainSystem.Instance.QuestManager.QuestController.questDict[questIndex].QuestState = (QuestState)PlayerPrefs.GetInt("questState");

                for (int i = 0; i < BaseQuest.enemyKillCount.Length; i++)
                {
                    string enemyIndex = "enemy" + i.ToString();
                    BaseQuest.enemyKillCount[i] = PlayerPrefs.GetInt(enemyIndex);
                }
            }
        }
    }
}
