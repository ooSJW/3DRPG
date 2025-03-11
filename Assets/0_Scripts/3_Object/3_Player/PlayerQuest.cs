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

    public partial class PlayerQuest : MonoBehaviour // Data Field
    {
        private Player player;
    }
    public partial class PlayerQuest : MonoBehaviour // Initialize
    {
        private void Allocate()
        {

        }
        public void Initialize(Player playerValue)
        {
            player = playerValue;

            Allocate();
            Setup();
        }
        private void Setup()
        {
            
        }
    }
}
