/*
	* Coder :
	* Last Update :
	* Information
*/
namespace project02
{
    using System.Collections;
    using System.Collections.Generic;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;

    public partial class SkillCoolTimeUIObject : MonoBehaviour
    {
        private SkillName skillName;
        private SkillBase skill;
        private RectTransform rectTransform;

        [SerializeField] private Image fillImage;
        [SerializeField] private TMP_Text coolTimeText;
    }
    public partial class SkillCoolTimeUIObject : MonoBehaviour
    {
        private void Allocate()
        {
            rectTransform = GetComponent<RectTransform>();
        }
        public void Initialize(SkillName skillNameValue)
        {
            skillName = skillNameValue;
            skill = MainSystem.Instance.PlayerManager.Player.PlayerSkillDict[skillName];
            Allocate();
            Setup();
            rectTransform.SetAsLastSibling();
        }
        private void Setup()
        {

        }
    }
    public partial class SkillCoolTimeUIObject : MonoBehaviour
    {
        private void Update()
        {
            float time = skill.currentCoollingTime / skill.SkillInfo.cool_time;
            fillImage.fillAmount = time;
            coolTimeText.text = skill.currentCoollingTime.ToString("F0");
            if (!skill.IsCoolTime)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
