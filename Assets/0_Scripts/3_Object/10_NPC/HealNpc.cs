/*
	* Coder :
	* Last Update :
	* Information
*/
namespace project02
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Security;
    using UnityEngine;

    public partial class HealNpc : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
                player.CloseToHealer = true;
        }

        private void OnTriggerExit(Collider other)
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
                player.CloseToHealer = false;
        }
    }
}