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

    public partial class UIManager : MonoBehaviour
    {
        public UIController UIController { get; private set; } = default;
    }
    public partial class UIManager : MonoBehaviour
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
    public partial class UIManager : MonoBehaviour // Sign
    {
        public void SignUpUIController(UIController uIControllerValue)
        {
            UIController = uIControllerValue;
            UIController.Initialize();
        }
        public void SignDownUIController()
        {
            UIController = null;
        }
    }
}
