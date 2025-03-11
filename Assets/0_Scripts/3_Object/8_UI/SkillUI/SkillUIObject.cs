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
    using UnityEngine.EventSystems;
    using UnityEngine.UI;
    using static project02.PlayerStatData;

    public partial class SkillUIObject : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler // Data Field
    {
        [SerializeField] private Button levelUpButton;
        [SerializeField] private Button levelDownButton;
        [SerializeField] private SkillName thisSkillName;
        [SerializeField] private GameObject skillInfoObject;

        private TMP_Text skillInfoText;
        private string command;
        private float powerGrowth;
        private int minSkillLevel;
        private int maxSkillLevel;
        private int currentSkillLevel;

    }

    public partial class SkillUIObject : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler // Initialize
    {
        private void Allocate()
        {
            SkillInformation skillInfo = MainSystem.Instance.PlayerManager.Player.PlayerSkillDict[thisSkillName].SkillInfo;
            command = skillInfo.commandInUI;
            powerGrowth = skillInfo.power_growth;
            minSkillLevel = MainSystem.Instance.DataManager.SkillData.GetData(thisSkillName.ToString()).level;
            maxSkillLevel = skillInfo.max_level;
            currentSkillLevel = skillInfo.level;
        }
        public void Initialize()
        {
            Allocate();
            Setup();
            skillInfoText = skillInfoObject.transform.Find("SkillInfoText").GetComponent<TMP_Text>();
        }
        private void Setup()
        {

        }
    }
    public partial class SkillUIObject : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler // Main
    {
        private void Update()
        {
            SetActiveButton();
        }
    }
    public partial class SkillUIObject : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler // Private Property
    {
        private void SetActiveButton()
        {
            if (currentSkillLevel == maxSkillLevel || MainSystem.Instance.UIManager.UIController.SkillUIController.SKillPoint == 0)
                levelUpButton.interactable = false;
            else
                levelUpButton.interactable = true;

            if (currentSkillLevel == minSkillLevel)
                levelDownButton.interactable = false;
            else
                levelDownButton.interactable = true;
        }
    }

    public partial class SkillUIObject : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler // Property
    {
        public void OnPointerEnter(PointerEventData eventData)
        {
            skillInfoObject.SetActive(true);
            skillInfoText.text = $"커맨드 : {command}\n레벨당 데미지 증가율 : {powerGrowth * 100}%\n현재 레벨 : {currentSkillLevel}\n최대 레벨 : {maxSkillLevel}";
        }
        public void OnPointerExit(PointerEventData eventData)
        {
            skillInfoObject.SetActive(false);
        }

        public void SkillLevelUp()
        {
            SkillBase skill = MainSystem.Instance.PlayerManager.Player.PlayerSkillDict[thisSkillName];
            skill.SkillLevel++;
            currentSkillLevel = skill.SkillLevel;
            MainSystem.Instance.PlayerManager.Player.SkillPoint--;
        }

        public void SkillLevelDown()
        {
            SkillBase skill = MainSystem.Instance.PlayerManager.Player.PlayerSkillDict[thisSkillName];
            skill.SkillLevel--;
            currentSkillLevel = skill.SkillLevel;
            MainSystem.Instance.PlayerManager.Player.SkillPoint++;
        }
    }
}
