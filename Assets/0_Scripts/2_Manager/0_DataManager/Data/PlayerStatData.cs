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

    public partial class PlayerStatData  // Information
    {
        [System.Serializable]
        public class PlayerStatInformation : BaseCombatStatInformation
        {
            public int exp;
            public int maxExp;

            public float maxSpeed;
            public float evadeSpeed;
            public float acceleration;
            public int skillPoint;
        }
    }


    public partial class PlayerStatData
    {
        public Dictionary<string, PlayerStatInformation> playerStatInformationDict = default;
    }

    public partial class PlayerStatData
    {
        private void Allocate()
        {
            playerStatInformationDict = new Dictionary<string, PlayerStatInformation>();
        }
        public void Initialize()
        {
            Allocate();
            Setup();
        }
        private void Setup()
        {
            MainSystem.Instance.DataManager.SetUpData<PlayerStatInformation>(playerStatInformationDict, "PlayerStatData");
        }

        public PlayerStatInformation GetData(string index)
        {
            // index == level
            return playerStatInformationDict[index];
        }
    }
}

