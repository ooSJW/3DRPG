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
    using UnityEngine;

    public partial class QuestData // Information
    {
        [Serializable]
        public class QuestInformation : BaseInformation
        {
            public string name;
            public string[] talk;
            public string[] completeMessage;
            public string questDetail;
            public string nextQuestIndex;
            public int compensationExp;
            public int priorQuest;
            public int conditionValue;
        }
    }
    public partial class QuestData // Data Field
    {
        public Dictionary<string, QuestInformation> questInformationDict;
    }

    public partial class QuestData // Initialize
    {
        private void Allocate()
        {
            questInformationDict = new Dictionary<string, QuestInformation>();
        }
        public void Initialize()
        {
            Allocate();
            Setup();
        }
        private void Setup()
        {
            MainSystem.Instance.DataManager.SetUpData<QuestInformation>(questInformationDict, "QuestData");
        }
    }

    public partial class QuestData // Property
    {
        public QuestInformation GetData(string index)
        {
            return questInformationDict[index];
        }
    }
}
