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

    [System.Serializable]
    public class ItemInformation : BaseInformation
    {
        public string item_type;
        public int item_value;
        public string icon_path;
    }

    public partial class ItemData
    {
        public Dictionary<string, ItemInformation> itemDataDict;
    }
    public partial class ItemData
    {
        private void Allocate()
        {
            itemDataDict=new Dictionary<string, ItemInformation>(); 
        }
        public void Initialize()
        {
            Allocate();
            Setup();
        }
        private void Setup()
        {
            MainSystem.Instance.DataManager.SetUpData<ItemInformation>(itemDataDict, "ItemData");
        }
    }
}
