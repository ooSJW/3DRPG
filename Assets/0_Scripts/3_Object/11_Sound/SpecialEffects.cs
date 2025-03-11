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
    using UnityEngine.UI;

    public partial class SpecialEffects : MonoBehaviour
    {
        [SerializeField] private AudioClip[] sfxClips;
        [SerializeField] private Slider sfxSlider;
        private AudioSource[] sfxPlayers;
        private float sfxVolume = 0.1f;

    }
    public partial class SpecialEffects : MonoBehaviour
    {
        private void Allocate()
        {
            sfxPlayers = new AudioSource[sfxClips.Length];
        }
        public void Initialize()
        {
            Allocate();
            Setup();
        }
        private void Setup()
        {
            if (PlayerPrefs.HasKey("sfxVolume"))
                sfxVolume = PlayerPrefs.GetFloat("sfxVolume");

            for (int i = 0; i < sfxPlayers.Length; i++)
            {
                sfxPlayers[i] = gameObject.AddComponent<AudioSource>();
                sfxPlayers[i].loop = false;
                sfxPlayers[i].clip = sfxClips[i];
                sfxPlayers[i].playOnAwake = false;
                sfxPlayers[i].volume = sfxVolume;
            }
            sfxSlider.value = sfxVolume;
            sfxPlayers[(int)SoundClipName.PlayerMelee].time = 0.2f;
            sfxPlayers[(int)SoundClipName.PlayerEvade].time = 0.3f;
        }
    }
    public partial class SpecialEffects : MonoBehaviour
    {
        public void PlaySfx(SoundClipName clipName)
        {
            sfxPlayers[(int)clipName].Play();
        }
        public void SetSfxVolume(float volume)
        {
            sfxVolume = volume;
            for (int i = 0; i < sfxPlayers.Length; i++)
            {
                sfxPlayers[i].volume = volume;
            }
            PlayerPrefs.SetFloat("sfxVolume", sfxVolume);
        }
    }
}
