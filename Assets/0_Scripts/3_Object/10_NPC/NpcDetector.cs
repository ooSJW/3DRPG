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
            QuestController questController = MainSystem.Instance.QuestManager.QuestController;
            questController.CanTalk = true;
            questController.NpcTransform = transform;

        }
        private void OnTriggerExit(Collider other)
        {
            QuestController questController = MainSystem.Instance.QuestManager.QuestController;
            questController.CanTalk = false;
            questController.NpcTransform = null;
        }

    }
}

