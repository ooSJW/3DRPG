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

    public partial class VideoOption : MonoBehaviour // Data Field
    {
        [SerializeField] private TMP_Dropdown resolutionDropdown;
        [SerializeField] private Toggle fullScreenBtn;

        private Camera mainCamera;
        private List<Resolution> resolutionList;
        private FullScreenMode screenMode;
        private int resolutionIndex = 0;
    }
    public partial class VideoOption : MonoBehaviour // Initialize
    {
        private void Allocate()
        {
            resolutionList = new List<Resolution>();
        }
        public void Initialize()
        {
            Allocate();
            Setup();
            InitUI();
        }
        private void Setup()
        {

        }
    }
    public partial class VideoOption : MonoBehaviour // Private Property
    {
        private void InitUI()
        {
            mainCamera = Camera.main;
            for (int i = 0; i < Screen.resolutions.Length; i++)
            {
                if (Screen.resolutions[i].width >= 1024)
                {
                    int refreshRate = Screen.resolutions[i].refreshRate;
                    if (refreshRate == 60 || refreshRate == 144)
                        resolutionList.Add(Screen.resolutions[i]);
                }
            }

            resolutionDropdown.options.Clear();
            foreach (Resolution item in resolutionList)
            {
                TMP_Dropdown.OptionData option = new TMP_Dropdown.OptionData();
                option.text = $"{item.width} X {item.height}  {item.refreshRate}hz";
                resolutionDropdown.options.Add(option);


                if (item.width == Screen.width && item.height == Screen.height)
                    resolutionDropdown.value = resolutionIndex;
                resolutionIndex++;
            }
            resolutionDropdown.RefreshShownValue();
            fullScreenBtn.isOn = Screen.fullScreenMode.Equals(FullScreenMode.FullScreenWindow) ? true : false;
        }
    }
    public partial class VideoOption : MonoBehaviour // Property
    {
        public void DropdownOptionChange(int index)
        {
            resolutionIndex = index;
        }

        public void FullScreenBtn(bool isFull)
        {
            screenMode = isFull ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
        }
        public void Instruction()
        {
            GameObject instructionUI = MainSystem.Instance.UIManager.UIController.InstructionUI.gameObject;
            instructionUI.SetActive(true);
            MainSystem.Instance.UIManager.UIController.activeUIList.Add(instructionUI);
        }
        public void CloseBtnClick()
        {
            gameObject.SetActive(false);
        }
        public void OkBtnClick()
        {
            if (resolutionList.Count > resolutionIndex)
                Screen.SetResolution(resolutionList[resolutionIndex].width, resolutionList[resolutionIndex].height, screenMode, resolutionList[resolutionIndex].refreshRate);
            gameObject.SetActive(false);
        }
        public void GoToMenu()
        {
            MainSystem.Instance.PlayerManager.Player.Save();
            MainSystem.Instance.QuestManager.QuestController.SaveQuest();
            MainSystem.Instance.SceneManager.LoadScene(SceneName.Menu.ToString());
        }
    }
}
