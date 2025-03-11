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
    using static UnityEngine.GraphicsBuffer;

    public partial class CombatObjectBase : MonoBehaviour // Data Field
    {
        [SerializeField] protected GameObject weaponObject;
        protected Weapon weapon;

        [SerializeField] protected InfoUI infoUI;

        [SerializeField] protected Transform damageTextTransform;

        protected int level;
        public virtual int Level { get => level; set => level = value; }

        protected int hp;
        public virtual int Hp { get => hp; set => hp = value; }

        protected int defense;
        public int Defense { get => defense; set => defense = value; }

        protected float totalDamage;
    }


    public partial class CombatObjectBase : MonoBehaviour // Initialize
    {
        private void Allocate()
        {
            if (weaponObject != null)
                weapon = weaponObject.GetComponent<Weapon>();
        }
        public virtual void Initialize()
        {
            Allocate();
            Setup();
            if (weapon != null)
                weapon.Initialize(this);
        }
        private void Setup()
        {

        }
    }


    public partial class CombatObjectBase : MonoBehaviour
    {
        public virtual void CalculateDamage(BaseCombatStatInformation statInfo)
        {
            totalDamage = statInfo.power;
        }

        public virtual void SendDamage(CombatObjectBase target, BaseCombatStatInformation sender, float skillDamage = 0)
        {
            float resultDamage = totalDamage;
            bool isCritical = false;

            if (UnityEngine.Random.Range(0f, 1f) <= sender.criticalPercent)
            {
                resultDamage += resultDamage * sender.criticalIncreasePercent;
                isCritical = true;
            }

            resultDamage += resultDamage * skillDamage;

            target.ReceiveDamage((int)resultDamage, isCritical);
        }

        public virtual void ReceiveDamage(int damage, bool isCritical = false)
        {
            if (Hp <= 0)
                return;

            if (damage <= Defense)
                damage = 1;
            else
                damage -= Defense;

            Hp -= damage;
            GameObject damageObj = MainSystem.Instance.PoolManager.Spawn(PoolObject.HpParticle.ToString(), damageTextTransform, damageTextTransform.position);
            DamageUI dmgUI = damageObj.GetComponent<DamageUI>();
            dmgUI.Initialize();
            dmgUI.SetDamageText(damage, isCritical);
        }
    }
}
