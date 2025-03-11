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

    public partial class NpcDetector : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            MainSystem.Instance.QuestManager.QuestController.CanTalk = true;
        }
        private void OnTriggerExit(Collider other)
        {
            MainSystem.Instance.QuestManager.QuestController.CanTalk = false;
        }

    }
}

