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
    using static project02.ZoneData;

    public partial class DungeonZoneObject : ZoneObject // Main
    {
        public override bool CloseToPlayer { get => closeToPlayer; set => closeToPlayer = value; }
    }
    public partial class DungeonZoneObject : ZoneObject // Main
    {
        protected override void Update()
        {

        }
    }
    public partial class DungeonZoneObject : ZoneObject
    {
        private void Allocate()
        {

        }
        public override void Initialize(ZoneInformation zoneInformationValue)
        {
            base.Initialize(zoneInformationValue);
            Allocate();
            Setup();
            Spawn();
        }
        private void Setup()
        {

        }
    }
}
