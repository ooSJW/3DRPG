/*
	* Coder :
	* Last Update :
	* Information
*/
namespace project02
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using static project02.ZoneData;

    public partial class ZoneController : MonoBehaviour
    {
        [SerializeField] private List<ZoneObject> zoneObjectList;
    }


    public partial class ZoneController : MonoBehaviour
    {
        private void Allocate()
        {
            zoneObjectList = new List<ZoneObject>();
        }
        public void Initialize()
        {
            Allocate();
            Setup();
        }
        private void Setup()
        {
            //for (int i = 0; i < zoneObjectList.Count; i++)
            //{
            //    SignUpZoneObject(zoneObjectList[i]);
            //}
            //print(zoneObjectList.Count);
            for (int i = 0; i < transform.childCount; ++i)
            {
                SignUpZoneObject(transform.GetChild(i).GetComponent<ZoneObject>());
            }
        }
    }


    public partial class ZoneController : MonoBehaviour
    {
        public void SignUpZoneObject(ZoneObject zoneObjectValue)
        {
            ZoneInformation zoneInformation =
                MainSystem.Instance.DataManager.ZoneData.zoneDataDict.Values.
                Where(elem => zoneObjectValue.name == elem.zone_name).SingleOrDefault();
            if (zoneInformation != null)
            {
                zoneObjectValue.Initialize(zoneInformation);
                zoneObjectList.Add(zoneObjectValue);
            }
        }
        public void SugnDownZoneObject(ZoneObject zoneObjectValue)
        {
            zoneObjectList.Remove(zoneObjectValue);
        }
    }
}
