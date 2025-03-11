/*
	* Coder :
	* Last Update :
	* Information
*/
namespace project02
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Text;
    using UnityEngine;

    public partial class MainSystem : GenericSingleton<MainSystem> // Data Field
    {
        public DataManager DataManager { get; private set; } = default;
        public QuestManager QuestManager { get; private set; } = default;
        public SceneManager SceneManager { get; private set; } = default;
        public PoolManager PoolManager { get; private set; } = default;
        public ZoneManager ZoneManager { get; private set; } = default;
        public EnemyManager EnemyManager { get; private set; } = default;
        public PlayerManager PlayerManager { get; private set; } = default;
        public SoundManager SoundManager { get; private set; } = default;
        public UIManager UIManager { get; private set; } = default;
    }
    public partial class MainSystem : GenericSingleton<MainSystem> // Initialize
    {
        private void Allocate()
        {
            DataManager = gameObject.AddComponent<DataManager>();
            QuestManager = gameObject.AddComponent<QuestManager>();
            SceneManager = gameObject.AddComponent<SceneManager>();
            PoolManager = gameObject.AddComponent<PoolManager>();
            ZoneManager = gameObject.AddComponent<ZoneManager>();
            EnemyManager = gameObject.AddComponent<EnemyManager>();
            SoundManager = gameObject.AddComponent<SoundManager>();
            PlayerManager = gameObject.AddComponent<PlayerManager>();
            UIManager = gameObject.AddComponent<UIManager>();
        }
        public void Initialize()
        {
            Allocate();
            Setup();

            DataManager.Initialize();
            QuestManager.Initialize();
            SceneManager.Initialize();
            PoolManager.Initialize();
            ZoneManager.Initialize();
            EnemyManager.Initialize();
            SoundManager.Initialize();
            PlayerManager.Initialize();
            UIManager.Initialize();
        }
        private void Setup()
        {

        }
    }
    public partial class MainSystem : GenericSingleton<MainSystem> // Property
    {
        public void MainSystemStart()
        {
            Initialize();
            SceneManager.LoadScene(SceneName.Menu.ToString());
        }
    }
}
