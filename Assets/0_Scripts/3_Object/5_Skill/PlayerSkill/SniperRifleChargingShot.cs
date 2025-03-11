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

    public partial class SniperRifleChargingShot : SkillBase
    {

    }
    public partial class SniperRifleChargingShot : SkillBase
    {
        private void Allocate()
        {

        }
        public override void Initialize(CombatObjectBase combatObjectValue)
        {
            Allocate();
            Setup();

            base.Initialize(combatObjectValue);
        }
        private void Setup()
        {
            skillName = SkillName.SniperRifleChargingShot;
        }
    }
}
