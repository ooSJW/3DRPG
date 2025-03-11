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

    public partial class SoundManager : MonoBehaviour // Data Field
    {
        public SoundController SoundController { get; private set; } = default;

    }
    public partial class SoundManager : MonoBehaviour // Initialize
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
    public partial class SoundManager : MonoBehaviour // Sign
    {
        public void SignUpSoundController(SoundController soundController)
        {
            SoundController = soundController;
            SoundController.Initialize();
        }
        public void SignDownSoundController()
        {
            SoundController = null;
        }
    }
}
