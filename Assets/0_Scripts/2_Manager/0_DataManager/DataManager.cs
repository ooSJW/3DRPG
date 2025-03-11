/*
	* Coder :
	* Last Update :
	* Information
*/
namespace project02
{
    using Newtonsoft.Json;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using System.IO;
    using DG.Tweening.Plugins.Core.PathCore;

    public partial class DataManager : MonoBehaviour // Data
    {
        public ZoneData ZoneData { get; private set; } = default;
        public EnemyData EnemyData { get; private set; } = default;
        public ItemData ItemData { get; private set; } = default;
        public QuestData QuestData { get; private set; } = default;
        public EnemyStatData EnemyStatData { get; private set; } = default;
        public PlayerStatData PlayerStatData { get; private set; } = default;
        public SkillData SkillData { get; private set; } = default;

        private string path = null;
        private string fileName = "save";
        public int CurrentSlot { get; set; } = 0;
    }
    public partial class DataManager : MonoBehaviour // Initialize
    {
        private void Allocate()
        {
            path = Application.persistentDataPath + "/";
            ZoneData = new ZoneData();
            EnemyData = new EnemyData();
            ItemData = new ItemData();
            QuestData = new QuestData();
            EnemyStatData = new EnemyStatData();
            PlayerStatData = new PlayerStatData();
            SkillData = new SkillData();
        }
        public void Initialize()
        {
            Allocate();
            Setup();

            ZoneData.Initialize();
            EnemyData.Initialize();
            ItemData.Initialize();
            QuestData.Initialize();
            EnemyStatData.Initialize();
            PlayerStatData.Initialize();
            SkillData.Initialize();
        }
        private void Setup()
        {

        }
    }

    public partial class DataManager : MonoBehaviour // Property
    {
        private Wrapper<T> LoadJson<T>(string path) where T : BaseInformation
        {
            string jsonStringData = Resources.Load<TextAsset>(path).ToString();
            return JsonConvert.DeserializeObject<Wrapper<T>>(jsonStringData);
        }


        public void SetUpData<T>(Dictionary<string, T> dataDict, string path) where T : BaseInformation
        {
            // Json파일을 string으로 변환, 변환한 데이터를 매개변수의 Dictionary에 추가할 수 있도록 만듦.
            dataDict.Clear();

            Wrapper<T> jsonData = LoadJson<T>(path);

            foreach (T data in jsonData.array)
            {
                dataDict.Add(data.index, data);
            }
        }
    }
}
