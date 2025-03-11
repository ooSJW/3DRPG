/*
	* Coder :
	* Last Update :
	* Information
*/
namespace project02
{
    using Castle.Core.Internal;
    using NSubstitute;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.UI;

    public partial class SkillCoolTimeUIController : MonoBehaviour
    {
        private SkillName skillName;
        public SkillName SkillName
        {
            get => skillName;
            set
            {
                skillName = value;
                GameObject currentSkill = coolTimeObjectArray.Where(elemm => elemm.name == skillName.ToString()).SingleOrDefault();
                if (currentSkill != null)
                {
                    SkillCoolTimeUIObject skillCoolTimeUIObject = currentSkill.GetComponent<SkillCoolTimeUIObject>();
                    skillCoolTimeUIObject.Initialize(SkillName);
                    currentSkill.SetActive(true);
                }
            }
        }
        [SerializeField] private GameObject[] coolTimeObjectArray;
    }


    public partial class SkillCoolTimeUIController : MonoBehaviour
    {
        private void Allocate()
        {

        }
        public void Initialize()
        {
            Allocate();
            Setup();

            for (int i = 0; i < coolTimeObjectArray.Length; i++)
            {
                coolTimeObjectArray[i].gameObject.SetActive(false);
            }
        }
        private void Setup()
        {

        }
    }

    public partial class SkillCoolTimeUIController : MonoBehaviour
    {
        public void SignUpCoolTimeSkill(SkillName skillNameValue)
        {
            SkillName = skillNameValue;
        }
    }
}
