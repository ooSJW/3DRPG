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

    public partial class BackGroundMusic : MonoBehaviour // Data Field
    {
        [SerializeField] private AudioClip bgm;
        [SerializeField] private Slider bgmSlider;
        private AudioSource bgmPlayer;
        private float bgmVolume = 0.1f;
    }
    public partial class BackGroundMusic : MonoBehaviour // Initialize
    {
        private void Allocate()
        {

        }
        public void Initialize()
        {
            Allocate();
            Setup();
            bgmPlayer.Play();
        }
        private void Setup()
        {
            bgmPlayer = gameObject.AddComponent<AudioSource>();

            if (PlayerPrefs.HasKey("bgmVolume"))
                bgmVolume = PlayerPrefs.GetFloat("bgmVolume");

            bgmPlayer.loop = true;
            bgmPlayer.playOnAwake = true;
            bgmPlayer.clip = bgm;
            bgmPlayer.volume = bgmVolume;
            bgmSlider.value = bgmVolume;
        }
    }
    public partial class BackGroundMusic : MonoBehaviour // property
    {
        public void SetBgmVolume(float volume)
        {
            bgmVolume = volume;
            bgmPlayer.volume = volume;
            PlayerPrefs.SetFloat("bgmVolume", bgmVolume);
        }
    }
}
