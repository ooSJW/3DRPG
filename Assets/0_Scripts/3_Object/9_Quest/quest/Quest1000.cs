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

    public partial class Quest1000 : BaseQuest
    {

    }
    public partial class Quest1000 : BaseQuest
    {
        private void Allocate()
        {
            questIndex = 1000.ToString();
        }
        public override void Initialize()
        {
            Allocate();
            Setup();
            base.Initialize();
        }
        private void Setup()
        {

        }
    }
}
