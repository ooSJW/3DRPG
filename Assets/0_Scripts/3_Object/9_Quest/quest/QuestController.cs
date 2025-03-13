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
    using TMPro;
    using UnityEngine;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;

    public partial class QuestController : MonoBehaviour // Data Property
    {
        private string currentQuestIndex;
        public string CurrentQuestIndex
        {
            get => currentQuestIndex;
            set
            {
                currentQuestIndex = value;
                if (questDict.ContainsKey(currentQuestIndex.ToString()))
                    if (currentQuestIndex.ToString() != 1000.ToString())
                        questDict[currentQuestIndex.ToString()].QuestState = QuestState.Acceptable;
                    else
                        giveUpBtn.SetActive(false);
            }
        }
        private int talkIndex = -1;
        public int TalkIndex
        {
            get => talkIndex;
            set
            {
                if (talkIndex != value)
                {
                    if (CurrentQuestIndex == 1040.ToString() && questDict[CurrentQuestIndex].QuestState == QuestState.During)
                    {
                        // EndTalk();
                        return;
                    }

                    talkIndex = value;
                    if (questDict.ContainsKey(CurrentQuestIndex))
                    {
                        BaseQuest quest = questDict[CurrentQuestIndex];

                        switch (quest.QuestState)
                        {
                            case QuestState.Acceptable:
                                if (talkIndex < quest.QuestInfo.talk.Length)
                                    talkText.text = quest.QuestInfo.talk[talkIndex];
                                else
                                {
                                    questWindow.SetActive(true);
                                    AcceptQuest();
                                    EndTalk();
                                }

                                if (CurrentQuestIndex == 1000.ToString())
                                    questWindow.SetActive(false);

                                break;

                            case QuestState.Clear:
                                if (talkIndex < quest.QuestInfo.completeMessage.Length)
                                    talkText.text = quest.QuestInfo.completeMessage[talkIndex];
                                else
                                {
                                    ClearQuest();
                                    EndTalk();
                                }
                                break;

                            default:
                                if (talkIndex < questDict[1000.ToString()].QuestInfo.talk.Length)
                                    talkText.text = questDict[1000.ToString()].QuestInfo.talk[talkIndex];
                                else
                                    EndTalk();
                                break;
                        }
                    }
                    else
                    {
                        if (talkIndex < questDict[1000.ToString()].QuestInfo.talk.Length)
                            talkText.text = questDict[1000.ToString()].QuestInfo.talk[talkIndex];
                        else
                            EndTalk();
                    }
                }
                MainSystem.Instance.PlayerManager.Player.Save();
            }
        }

        private bool isTalking = false;
        public bool IsTalking
        {
            get => isTalking;
            set
            {
                isTalking = value;
                if (isTalking)
                {
                    try
                    {
                        MainSystem.Instance.PlayerManager.Player.PlayerInput.CanMove = false;
                        MainSystem.Instance.SceneManager.ActiveScene.FollowCamera.StartDialouge(NpcTransform);
                    }
                    catch (Exception ex)
                    {
                        Debug.LogException(ex);
                    }
                }
                else
                {
                    try
                    {
                        MainSystem.Instance.PlayerManager.Player.PlayerInput.CanMove = true;
                        MainSystem.Instance.SceneManager.ActiveScene.FollowCamera.EndDialouge();
                    }
                    catch (Exception ex)
                    {
                        Debug.LogException(ex);
                    }
                }
                if (CurrentQuestIndex == 1040.ToString() && questDict[CurrentQuestIndex].QuestState == QuestState.During)
                {
                    portalUI.SetActive(value);
                    MainSystem.Instance.UIManager.UIController.ShowCursor = value;
                }
                if (questDict.ContainsKey(CurrentQuestIndex))
                    questTalkUI.SetActive(value);
            }
        }
    }
    public partial class QuestController : MonoBehaviour // Data Field
    {
        [SerializeField] private List<BaseQuest> questList;
        [SerializeField] private QuestMark questMark;
        [SerializeField] private GameObject questTalkUI;
        [SerializeField] private TMP_Text talkText;
        [SerializeField] private GameObject questWindow;
        [SerializeField] private GameObject portalUI;

        [SerializeField] private GameObject giveUpBtn;
        [SerializeField] private GameObject giveupMsg;

        public Dictionary<string, BaseQuest> questDict;

        private bool canTalk = false;
        public bool CanTalk { get => canTalk; set => canTalk = value; }

        public Transform NpcTransform { get; set; }
    }
    public partial class QuestController : MonoBehaviour // Initialize
    {
        private void Allocate()
        {
            questDict = new Dictionary<string, BaseQuest>();
            questTalkUI.SetActive(false);
        }
        public void Initialize()
        {
            Allocate();
            Setup();
            ChangeQuestMark();
        }
        private void Setup()
        {
            for (int i = 0; i < questList.Count; ++i)
            {
                SignUpQuest(questList[i]);
            }
            questWindow.SetActive(false);
            portalUI.SetActive(false);
            CurrentQuestIndex = 1010.ToString();
        }
    }
    public partial class QuestController : MonoBehaviour // Main
    {
        private void Update()
        {
            if (canTalk)
            {
                if (Input.GetKeyDown(KeyCode.R))
                    IsTalking = true;
            }

            if (IsTalking && Input.GetKeyDown(KeyCode.R))
                TalkIndex++;
        }
    }
    public partial class QuestController : MonoBehaviour // Private Property
    {
        private void EndTalk()
        {
            talkIndex = -1;
            IsTalking = false;
            ChangeQuestMark();
            MainSystem.Instance.UIManager.UIController.ShowCursor = false;
        }
        private void AcceptQuest()
        {
            questDict[CurrentQuestIndex].AcceptQuest();
            questWindow.SetActive(true);
            MainSystem.Instance.UIManager.UIController.QuestWindow.RotateImage();
        }
        private void ClearQuest()
        {
            questDict[CurrentQuestIndex].ClearQuest();
            CurrentQuestIndex = questDict[CurrentQuestIndex].QuestInfo.nextQuestIndex;
            MainSystem.Instance.SoundManager.SoundController.SpecialEffects.PlaySfx(SoundClipName.ClearQuest);
            MainSystem.Instance.PlayerManager.Player.Save();
            questWindow.SetActive(false);
            MainSystem.Instance.UIManager.UIController.QuestWindow.RotateImage();
        }
    }
    public partial class QuestController : MonoBehaviour //  Property
    {
        public void SetActiveGiveUpBtn(bool value)
        {
            giveUpBtn.SetActive(value);
        }
        public void ShowQuestGiveupMsg()
        {
            giveupMsg.SetActive(true);
        }
        public void GiveupQuest()
        {
            questDict[CurrentQuestIndex].GiveupQuest();
            giveupMsg.SetActive(false);
        }
        public void NotGiveupQuest()
        {
            giveupMsg.SetActive(false);
        }
        public void SaveQuest()
        {
            LocalData.Instance.SaveQuest();
        }

        public void ShowQuestWindow()
        {
            questDict[CurrentQuestIndex].SetQuestText();
            questWindow.SetActive(!questWindow.activeSelf);
        }
        public void ClosePortalUI()
        {
            EndTalk();
            portalUI.SetActive(false);
        }
        public void UsePortal(string sceneName)
        {
            EndTalk();
            portalUI.SetActive(false);
            MainSystem.Instance.PlayerManager.Player.Save();
            SaveQuest();
            MainSystem.Instance.SceneManager.LoadScene(sceneName);
        }
        public void ChangeQuestMark()
        {
            if (questMark != null)
            {
                switch (questDict[CurrentQuestIndex].QuestState)
                {
                    case QuestState.Acceptable:
                        questMark.ChangeImage(QuestImage.Acceptable);
                        break;
                    case QuestState.Clear:
                        questMark.ChangeImage(QuestImage.Clear);
                        break;
                    default:
                        questMark.ChangeImage(QuestImage.None);
                        break;
                }
            }
        }
        public BaseQuest GetCurrentQuest()
        {
            return questDict[CurrentQuestIndex];
        }
    }

    public partial class QuestController : MonoBehaviour// Sign
    {
        public void SignUpQuest(BaseQuest questValue)
        {
            questValue.Initialize();
            questDict.Add(questValue.QuestIndex, questValue);
        }
        public void SignDownQuest(BaseQuest questValue)
        {
            questDict.Remove(questValue.QuestIndex);
        }
    }

}
