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

    public partial class Quest1020 : BaseQuest
    {
        public override void AcceptQuest()
        {
            base.AcceptQuest();
            questCurrentText.text = $"현재 : {MainSystem.Instance.PlayerManager.Player.Level}레벨";
        }
        private void Update()
        {
            if (isAccept)
            {
                int level = MainSystem.Instance.PlayerManager.Player.Level;
                if (level >= questInfo.conditionValue)
                    QuestState = QuestState.Clear;

                questCurrentText.text = $"현재 : {Mathf.Clamp(MainSystem.Instance.PlayerManager.Player.Level, 0, questInfo.conditionValue)}레벨";
            }
        }
    }
    public partial class Quest1020 : BaseQuest
    {
        private void Allocate()
        {
            questIndex = 1020.ToString();
        }
        public override void Initialize()
        {
            Allocate();
            Setup();
            base.Initialize();
        }
        private void Setup()
        {

        }
    }
}
