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


    [System.Serializable]
    public class EnemyStatInformation : BaseCombatStatInformation
    {
        public float attackRange;
        public float skillUseDistance;
        public float targetingRange;
    }

    public class EnemyStatData
    {
        public Dictionary<string, EnemyStatInformation> enemyStatInformationDict = default;

        private void Allocate()
        {
            enemyStatInformationDict = new Dictionary<string, EnemyStatInformation>();
        }
        public void Initialize()
        {
            Allocate();
            Setup();
        }
        private void Setup()
        {
            MainSystem.Instance.DataManager.SetUpData<EnemyStatInformation>(enemyStatInformationDict, "EnemyStatData");
        }

        public EnemyStatInformation GetData(string index)
        {
            // index == name
            return enemyStatInformationDict[index];
        }
    }
}
