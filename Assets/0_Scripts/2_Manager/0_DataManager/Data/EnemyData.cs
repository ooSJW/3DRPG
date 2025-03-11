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
    public class EnemyInformation : BaseInformation
    {
        public string name;
        public int drop_exp;
        /*public string[] dropItemArray;*/
    }

    public partial class EnemyData // Data Field
    {
        public Dictionary<string, EnemyInformation> enemyDataDict;
    }
    public partial class EnemyData // Initialize
    {
        private void Allocate()
        {
            enemyDataDict = new Dictionary<string, EnemyInformation>();
        }
        public void Initialize()
        {
            Allocate();
            Setup();
        }
        private void Setup()
        {
            MainSystem.Instance.DataManager.SetUpData<EnemyInformation>(enemyDataDict, "EnemyData");
        }
    }

    public partial class EnemyData // Initialize
    {
        public EnemyInformation GetData(string name)
        {
            return enemyDataDict[name];
        }
    }
}
