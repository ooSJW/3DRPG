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

    public partial class BossShootEnergy : SkillBase
    {

    }
    public partial class BossShootEnergy : SkillBase
    {
        private void Allocate()
        {

        }
        public override void Initialize(CombatObjectBase combatObjectBase)
        {
            Allocate();
            Setup();
            base.Initialize(combatObjectBase);
        }
        private void Setup()
        {
            skillName = SkillName.BossShootEnergy;
        }
    }
}
