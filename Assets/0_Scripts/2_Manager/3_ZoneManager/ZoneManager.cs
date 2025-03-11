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

    public partial class ZoneManager : MonoBehaviour
    {
        public ZoneController ZoneController { get; private set; } = default;
    }
    public partial class ZoneManager : MonoBehaviour
    {
        private void Allocate()
        {

        }
        public void Initialize()
        {
            Allocate();
            Setup();
        }
        private void Setup()
        {

        }
    }
    public partial class ZoneManager : MonoBehaviour
    {
        public void SignUpZoneController(ZoneController zoneControllerValue)
        {
            ZoneController = zoneControllerValue;
            ZoneController.Initialize();
        }
        public void SignDownZoneController()
        {
            ZoneController = null;
        }
    }
}
