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
    public partial class BaseCombatStatInformation : BaseInformation
    {
        public int level;
        public int hp;
        public int maxHp;

        public float moveSpeed;

        public int power;
        public int defense;
        public float criticalPercent;
        public float criticalIncreasePercent;

        public string[] useable_skill;
    }
}
