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

    public partial class FootStep : MonoBehaviour
    {
        [SerializeField] private SoundClipName soundClipName;
    }

    public partial class FootStep : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            Player player = other.GetComponent<Player>();
            player.PlayerAnimation.currentFootStep = soundClipName;
        }
        private void OnTriggerExit(Collider other)
        {
            Player player = other.GetComponent<Player>();
            player.PlayerAnimation.currentFootStep = player.PlayerAnimation.DefaultFootStep;
        }
    }
}
