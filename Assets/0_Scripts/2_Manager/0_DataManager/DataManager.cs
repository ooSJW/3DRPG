/*
	* Coder :
	* Last Update :
	* Information
*/
namespace project02
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using UnityEngine;
    using System.Linq;
    using System.Reflection;
    using System;

    public partial class DataManager : MonoBehaviour  // Data
    {
        public ZoneData ZoneData { get; private set; } = default;
        public EnemyData EnemyData { get; private set; } = default;
        public ItemData ItemData { get; private set; } = default;
        public QuestData QuestData { get; private set; } = default;
        public EnemyStatData EnemyStatData { get; private set; } = default;
        public PlayerStatData PlayerStatData { get; private set; } = default;
        public SkillData SkillData { get; private set; } = default;

        private string path = null;
        // private string fileName = "save";
        public int CurrentSlot { get; set; } = 0;
    }
    public partial class DataManager  // Initialize
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

    public partial class DataManager  // Property
    {
        #region Json
        private Wrapper<T> LoadJson<T>(string path) where T : BaseInformation
        {
            string jsonStringData = Resources.Load<TextAsset>(path).ToString();
            return JsonConvert.DeserializeObject<Wrapper<T>>(jsonStringData);
        }


        public void SetUpData<T>(Dictionary<string, T> dataDict, string path) where T : BaseInformation
        {
            dataDict.Clear();

            Wrapper<T> jsonData = LoadJson<T>(path);

            foreach (T data in jsonData.array)
            {
                dataDict.Add(data.index, data);
            }
        }
        #endregion

        #region Csv
        public void LoadCsv<T>(string path, Dictionary<string, T> dataDict) where T : BaseInformation, new()
        {
            dataDict.Clear();
            TextAsset csvFile = Resources.Load<TextAsset>($"Csv/{path}");
            if (csvFile != null)
            {
                string[] csvValueArray = csvFile.text.Split('\n');
                string[] csvFieldName = csvValueArray[0].Split(',');

                for (int i = 1; i < csvValueArray.Length; i++)
                {
                    string[] csvValue = csvValueArray[i].Split(',');
                    dataDict.Add(csvValue[0], ParseCsv<T>(csvFieldName, csvValue));
                }
            }
        }

        private T ParseCsv<T>(string[] csvFieldName, string[] csvValue) where T : BaseInformation, new()
        {
            // Reflectio과 박싱, 언박싱은 성능에 불리할 수 있지만, 게임 시작 후 데이터 로드 시 한 번씩만 호출되는 기능이기 때문에
            // 성능보다는 코드의 재사용성과 유연성 증가에 중점을 두고 사용.
            T data = new T();
            FieldInfo[] fieldInfoArray = typeof(T).GetFields();
            string[] arrayData;

            for (int i = 0; i < csvFieldName.Length; i++)
            {
                string currentCsvKey = csvFieldName[i].Trim();
                string currentCsvValue = csvValue[i].Trim();
                FieldInfo currentField = null;
                try
                {
                    currentField = fieldInfoArray.SingleOrDefault(info => info.Name == currentCsvKey);

                    if (currentField is not null)
                    {
                        if (!currentField.FieldType.IsArray)
                            currentField.SetValue(data, Convert.ChangeType(currentCsvValue, currentField.FieldType));

                        else
                        {
                            Type arrayType = currentField.FieldType.GetElementType();
                            arrayData = currentCsvValue.Split(' ');
                            Array array = Array.CreateInstance(arrayType, arrayData.Length);
                            for (int j = 0; j < array.Length; j++)
                            {
                                try
                                {
                                    var element = Convert.ChangeType(arrayData[j], arrayType);
                                    array.SetValue(element, j);
                                }
                                catch
                                {
                                    Debug.LogWarning($"Converting Error / Cant Convert [{arrayData[j]}] to [{arrayType.Name}]");
                                }
                            }
                            currentField.SetValue(data, array);
                        }
                    }
                    else
                        Debug.LogWarning($"No Matching Filed Found For Key : {currentCsvKey}");
                }
                catch
                {
                    Debug.LogWarning($"Field Name Is Duplicate Or Has Error, Check Data File [name :{currentField.Name}]");
                }
            }
            return data;
        }


        #endregion
    }
}
