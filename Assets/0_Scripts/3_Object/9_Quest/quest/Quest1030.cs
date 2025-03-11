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

    public partial class Quest1030 : BaseQuest
    {
        public override void AcceptQuest()
        {
            base.AcceptQuest();
            int initialCount = 0;
            BaseQuest.enemyKillCount[(int)PoolObject.Zombie] = initialCount;
            BaseQuest.enemyKillCount[(int)PoolObject.Skeleton] = initialCount;
            questCurrentText.text = $"���� : {initialCount}���� ���\n���̷��� : {initialCount}���� ���";

        }
        private void Update()
        {
            if (isAccept)
            {
                int zombie = BaseQuest.enemyKillCount[(int)PoolObject.Zombie];
                int skeleton = BaseQuest.enemyKillCount[(int)PoolObject.Skeleton];

                if (zombie >= questInfo.conditionValue && skeleton >= questInfo.conditionValue)
                    QuestState = QuestState.Clear;

                questCurrentText.text = $"���� : {Mathf.Clamp(zombie, 0, questInfo.conditionValue)}���� ���\n���̷��� : {Mathf.Clamp(skeleton, 0, questInfo.conditionValue)}���� ���";
            }
        }
    }
    public partial class Quest1030 : BaseQuest
    {
        private void Allocate()
        {
            questIndex = 1030.ToString();
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
