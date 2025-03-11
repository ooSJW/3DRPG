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

    public partial class QuestManager : MonoBehaviour // Initialize
    {
        public QuestController QuestController { get; private set; } = default;
    }
    public partial class QuestManager : MonoBehaviour // Data Field
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
    public partial class QuestManager : MonoBehaviour // Sign
    {
        public void SignUpQuestController(QuestController questControllerValue)
        {
            QuestController = questControllerValue;
            QuestController.Initialize();
        }
        public void SignDownQuestController()
        {
            QuestController = null;
        }
    }
}
