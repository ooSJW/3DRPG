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

    public partial class SoundController : MonoBehaviour
    {
        [field: SerializeField] public BackGroundMusic BackGroundMusic = default;
        [field: SerializeField] public SpecialEffects SpecialEffects = default;
    }
    public partial class SoundController : MonoBehaviour
    {
        private void Allocate()
        {

        }
        public void Initialize()
        {
            Allocate();
            Setup();
            BackGroundMusic.Initialize();
            SpecialEffects.Initialize();
        }
        private void Setup()
        {

        }
    }
}
