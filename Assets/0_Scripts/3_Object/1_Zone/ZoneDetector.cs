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
    using UnityEditor;
    using UnityEngine;

    public partial class ZoneDetector : MonoBehaviour
    {
        private ZoneObject zoneObject = default;
    }
    public partial class ZoneDetector : MonoBehaviour
    {
        private void Allocate()
        {

        }
        public void Initialize(ZoneObject zoneObjectValue)
        {
            zoneObject = zoneObjectValue;
            Allocate();
            Setup();
        }
        private void Setup()
        {

        }
    }

    public partial class ZoneDetector : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            zoneObject.CloseToPlayer = true;
        }
        private void OnTriggerExit(Collider other)
        {
            zoneObject.CloseToPlayer = false;
        }
    }
}
