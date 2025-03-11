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

    public partial class Quest1040 : BaseQuest
    {
        public override void AcceptQuest()
        {
            base.AcceptQuest();
            int initialCount = 0;
            BaseQuest.enemyKillCount[(int)PoolObject.BossOrc] = initialCount;
        }
        private void Update()
        {
            if (isAccept)
            {
                int boss = BaseQuest.enemyKillCount[(int)PoolObject.BossOrc];

                if (boss >= questInfo.conditionValue)
                {
                    QuestState = QuestState.Clear;
                    questCurrentText.text = "보스 처치 완료!";
                }

            }
        }
    }
    public partial class Quest1040 : BaseQuest
    {
        private void Allocate()
        {
            questIndex = 1040.ToString();
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
