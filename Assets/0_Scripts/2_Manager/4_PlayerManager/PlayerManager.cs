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

    public partial class PlayerManager : MonoBehaviour // Data Field
    {
        public Player Player { get; private set; } = default;
    }
    public partial class PlayerManager : MonoBehaviour // Initialize
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
    public partial class PlayerManager : MonoBehaviour // Sign
    {
        public void SignUpPlayer(Player playerValue)
        {
            Player = playerValue;
            Player.Initialize();
        }
        public void SignDownPlayer()
        {
            Player = null;
        }
    }
}
