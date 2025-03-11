/*
	* Coder :
	* Last Update :
	* Information
*/
namespace project02
{
    using System.Collections;
    using System.Collections.Generic;
    using Unity.VisualScripting.FullSerializer;
    using UnityEngine;


    [System.Serializable]
    public class SkillInformation : BaseInformation
    {
        public string[] command;
        public string commandInUI;

        public int level;
        public int max_level;

        public float power;
        public float power_growth;

        public float cool_time;
        public float active_time;
        public float melee_range;
        public float range;
        public float angle_range;

        public string icon_path;
        public string effect_path;
    }

    public partial class SkillData // Data Field
    {
        public Dictionary<string, SkillInformation> skillInformationDict;
    }

    public partial class SkillData // Initialize
    {
        private void Allocate()
        {
            skillInformationDict = new Dictionary<string, SkillInformation>();
        }
        public void Initialize()
        {
            Allocate();
            Setup();
        }
        private void Setup()
        {
            MainSystem.Instance.DataManager.SetUpData<SkillInformation>(skillInformationDict, "SkillData");
        }
    }

    public partial class SkillData // property
    {
        public SkillInformation GetData(string index)
        {
            return skillInformationDict[index];
        }
    }
}
