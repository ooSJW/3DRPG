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

    public partial class BaseScene : MonoBehaviour // Data Field
    {
        public List<GameObject> poolableObject = new List<GameObject>();

        [field: SerializeField] public QuestController QuestController { get; private set; }
        [field: SerializeField] public Player Player { get; private set; }
        [field: SerializeField] public ZoneController ZoneController { get; private set; }
        [field: SerializeField] public SoundController SoundController { get; private set; }
        [field: SerializeField] public UIController UIController { get; private set; }

        // InGameScene Member
        [field: SerializeField] public FollowCamera FollowCamera { get; private set; }
    }
    public partial class BaseScene : MonoBehaviour // Initialize
    {
        private void Allocate()
        {

        }
        public virtual void Initialize()
        {
            Allocate();
            Setup();
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        private void Setup()
        {
            MainSystem.Instance.QuestManager.SignUpQuestController(QuestController);
            MainSystem.Instance.PlayerManager.SignUpPlayer(Player);
            MainSystem.Instance.ZoneManager.SignUpZoneController(ZoneController);
            MainSystem.Instance.PoolManager.Register();
            LocalData.Instance.Load();
            MainSystem.Instance.SoundManager.SignUpSoundController(SoundController);
            MainSystem.Instance.UIManager.SignUpUIController(UIController);
        }
    }
    public partial class BaseScene : MonoBehaviour // Main
    {
        private void Awake()
        {
            MainSystem.Instance.SceneManager.SignUpActiveScene(this);
        }
    }
}
