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

    public partial class PlayerGroundChecker : MonoBehaviour
    {
        private Player player;
    }
    public partial class PlayerGroundChecker : MonoBehaviour
    {
        private void Allocate()
        {

        }
        public void Initialize(Player playerValue)
        {
            Allocate();
            Setup();

            player = playerValue;
        }
        private void Setup()
        {

        }
    }
    public partial class PlayerGroundChecker : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            player.IsGround = true;
        }
        private void OnTriggerStay(Collider other)
        {
            player.IsGround = true;
        }
        private void OnTriggerExit(Collider other)
        {
            player.IsGround = false;
        }
    }
}

