/*
	* Coder :
	* Last Update :
	* Information
*/
namespace project02
{
    using System.Collections;
    using System.Collections.Generic;
    using TMPro;
    using UnityEngine;
    using static project02.QuestData;

    public partial class BaseQuest : MonoBehaviour // Data Field
    {
        static public int[] enemyKillCount = new int[4] { 0, 0, 0, 0 };

        protected QuestInformation questInfo;
        public QuestInformation QuestInfo { get => questInfo; set => questInfo = value; }

        [SerializeField] protected GameObject questUI;
        [SerializeField] protected TMP_Text questTitleText;
        [SerializeField] protected TMP_Text questDetailsText;
        [SerializeField] protected TMP_Text questCurrentText;

        protected string questIndex;
        public string QuestIndex { get => questIndex; set => questIndex = value; }

        protected QuestState questState = QuestState.NotAcceptable;
        public QuestState QuestState
        {
            get => questState;
            set
            {
                if (questState != value)
                {
                    questState = value;

                    switch (questState)
                    {
                        case QuestState.During:
                            isAccept = true;
                            MainSystem.Instance.QuestManager.QuestController.SetActiveGiveUpBtn(true);
                            break;

                        case QuestState.Clear:
                            isAccept = true;
                            MainSystem.Instance.QuestManager.QuestController.SetActiveGiveUpBtn(true);
                            if (MainSystem.Instance.SoundManager.SoundController != null)
                                MainSystem.Instance.SoundManager.SoundController.SpecialEffects.PlaySfx(SoundClipName.ClearQuest);
                            break;

                        default:
                            isAccept = false;
                            MainSystem.Instance.QuestManager.QuestController.SetActiveGiveUpBtn(false);
                            break;
                    }
                    MainSystem.Instance.QuestManager.QuestController.ChangeQuestMark();
                }
                questState = value;
            }
        }

        protected bool isAccept = false;
    }
    public partial class BaseQuest : MonoBehaviour // Initialize
    {
        private void Allocate()
        {
            QuestInformation quest = MainSystem.Instance.DataManager.QuestData.GetData(questIndex);
            QuestInfo = new QuestInformation()
            {
                index = questIndex,
                name = quest.name,
                talk = quest.talk,
                completeMessage = quest.completeMessage,
                questDetail = quest.questDetail,
                compensationExp = quest.compensationExp,
                priorQuest = quest.priorQuest,
                conditionValue = quest.conditionValue,
                nextQuestIndex = quest.nextQuestIndex,
            };
        }
        public virtual void Initialize()
        {
            Allocate();
            Setup();
        }
        private void Setup()
        {

        }
    }
    public partial class BaseQuest : MonoBehaviour // virtual property
    {
        public void SetQuestText()
        {
            if (questState == QuestState.During || questState == QuestState.Clear)
            {
                questTitleText.text = questInfo.name;
                questDetailsText.text = questInfo.questDetail;
            }
        }

        public virtual void AcceptQuest()
        {
            isAccept = true;
            QuestState = QuestState.During;
            questTitleText.text = questInfo.name;
            questDetailsText.text = questInfo.questDetail;
        }
        public virtual void ClearQuest()
        {
            isAccept = false;
            questTitleText.text = null;
            questDetailsText.text = null;
            questCurrentText.text = null;
            LocalData.Instance.Save();
            MainSystem.Instance.PlayerManager.Player.Exp += questInfo.compensationExp;
        }
        public virtual void GiveupQuest()
        {
            isAccept = false;
            QuestState = QuestState.Acceptable;
            questTitleText.text = null;
            questDetailsText.text = null;
            questCurrentText.text = null;
        }
    }
}
