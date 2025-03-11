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
    public partial class Quest1010 : BaseQuest
    {
        public override void AcceptQuest()
        {
            base.AcceptQuest();
            int initialCount = 0;
            BaseQuest.enemyKillCount[(int)PoolObject.Spider] = initialCount;
            questCurrentText.text = $"���� : {initialCount}���� ���";
        }
    }
    public partial class Quest1010 : BaseQuest // Main
    {
        private void Update()
        {
            if (isAccept)
            {
                int spiderCount = BaseQuest.enemyKillCount[(int)PoolObject.Spider];

                if (spiderCount >= questInfo.conditionValue)
                    QuestState = QuestState.Clear;

                questCurrentText.text = $"���� : {Mathf.Clamp(spiderCount, 0, questInfo.conditionValue)}���� ���";
            }
        }
    }
    public partial class Quest1010 : BaseQuest
    {
        private void Allocate()
        {
            questIndex = 1010.ToString();
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
