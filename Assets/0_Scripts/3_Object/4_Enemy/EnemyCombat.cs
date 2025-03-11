/*
	* Coder :
	* Last Update :
	* Information
*/
namespace project02
{
    using DG.Tweening;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public partial class EnemyCombat : MonoBehaviour // Data Field
    {
        protected Enemy enemy;

        protected SkillName bossSkill = SkillName.None;
        public virtual SkillName BossSkill { get => bossSkill; set => bossSkill = value; }
        public bool skillEnable = false;
    }
    public partial class EnemyCombat : MonoBehaviour // Initialize
    {
        private void Allocate()
        {

        }
        public virtual void Initialize(Enemy enemyValue)
        {
            enemy = enemyValue;

            Allocate();
            Setup();
        }
        private void Setup()
        {

        }
    }

    public partial class EnemyCombat : MonoBehaviour // Main
    {
        public virtual void Progress() { }
    }
    public partial class EnemyCombat : MonoBehaviour // Property
    {
        public virtual void RandomPattern() { }
        public virtual void WindSkill() { }
        public virtual void BaseAttackFilter() { }
        public virtual void SkillFilter() { }
    }
}
