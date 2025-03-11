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
    using UnityEngine.EventSystems;
    using UnityEngine.Rendering.LookDev;
    using UnityEngine.UI;

    public partial class UIController : MonoBehaviour // Data Field
    {
        [field: SerializeField] public SkillUIController SkillUIController { get; private set; } = default;
        [field: SerializeField] public SkillCoolTimeUIController SkillCooltimeUI { get; private set; } = default;
        [field: SerializeField] public VideoOption VideoOption { get; private set; } = default;
        [field: SerializeField] public QuestWindow QuestWindow { get; private set; } = default;
        [field: SerializeField] public InstructionUI InstructionUI { get; private set; } = default;

        [SerializeField] private LayerMask uiLayer;
        [SerializeField] private GameObject deadUI;

        private bool isPlayerDead = false;
        public bool IsPlayerDead
        {
            get => isPlayerDead;
            set
            {
                if (isPlayerDead != value)
                {
                    deadUI.SetActive(value);
                }
                isPlayerDead = value;
            }
        }

        public List<GameObject> activeUIList;
        private bool showCursor = false;
        public bool ShowCursor
        {
            get => showCursor;
            set
            {
                if (showCursor != value)
                {
                    showCursor = value;
                    Cursor.visible = showCursor;
                    if (showCursor)
                        Cursor.lockState = CursorLockMode.None;
                    else
                        Cursor.lockState = CursorLockMode.Locked;
                }
            }
        }
    }
    public partial class UIController : MonoBehaviour // Initialize
    {
        private void Allocate()
        {
            activeUIList = new List<GameObject>();
        }
        public void Initialize()
        {
            Allocate();
            Setup();
            SkillUIController.Initialize();
            SkillCooltimeUI.Initialize();
            VideoOption.Initialize();
            QuestWindow.Initialize();
            deadUI.SetActive(false);
            SkillUIController.gameObject.SetActive(false);
            VideoOption.gameObject.SetActive(false);
            Cursor.visible = false;
        }
        private void Setup()
        {

        }
    }
    public partial class UIController : MonoBehaviour // Main
    {
        private void Update()
        {
            SetActiveSkillTree();
            CursorSetting();
            SetActiveGameOption();
        }
    }
    public partial class UIController : MonoBehaviour // Property
    {
        private void SetActiveSkillTree()
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                GameObject skillTree = SkillUIController.gameObject;
                skillTree.SetActive(!skillTree.activeSelf);
                ShowCursor = skillTree.activeSelf;
                if (skillTree.activeSelf)
                    activeUIList.Add(skillTree);
                else
                    activeUIList.Remove(skillTree);
            }
        }
        private void SetActiveGameOption()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (activeUIList.Count > 0)
                {
                    GameObject ui = activeUIList[activeUIList.Count - 1];
                    ui.SetActive(false);
                    activeUIList.Remove(ui);
                }
                else
                {
                    GameObject videoOption = VideoOption.gameObject;
                    videoOption.SetActive(!videoOption.activeSelf);
                    ShowCursor = videoOption.activeSelf;
                }
            }
        }
        private void CursorSetting()
        {
            if (!ShowCursor && Input.GetKeyDown(KeyCode.LeftControl))
                ShowCursor = true;
            else if (ShowCursor)
            {
                if (Input.GetKeyDown(KeyCode.LeftControl))
                    ShowCursor = false;
                if (Input.GetMouseButtonDown(0))
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if (!EventSystem.current.IsPointerOverGameObject() && !Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, uiLayer))
                        ShowCursor = false;
                }
            }
        }
    }
}
