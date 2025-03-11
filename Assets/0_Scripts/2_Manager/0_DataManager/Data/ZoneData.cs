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

    public partial class ZoneData
    {
        [System.Serializable]
        public class ZoneInformation : BaseInformation
        {
            public string zone_name;

            public int[] spawnable_enemy_array;
            public int max_spawn_count;

            public float respawn_time;
            public float respawn_probability;
        }
    }

    public partial class ZoneData
    {
        public Dictionary<string, ZoneInformation> zoneDataDict;
    }

    public partial class ZoneData
    {
        private void Allocate()
        {
            zoneDataDict = new Dictionary<string, ZoneInformation>();
        }
        public void Initialize()
        {
            Allocate();
            Setup();
        }
        private void Setup()
        {
            MainSystem.Instance.DataManager.SetUpData<ZoneInformation>(zoneDataDict, "ZoneData");
        }
    }
}
