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
    using UnityEngine.Analytics;

    public partial class MenuUI : MonoBehaviour
    {
        [SerializeField] private GameObject warningMsg;
    }
    public partial class MenuUI : MonoBehaviour
    {
        public void ContinueGame(string sceneName)
        {
            MainSystem.Instance.SceneManager.LoadScene(sceneName);
        }

        public void WarningMsg()
        {
            if (PlayerPrefs.HasKey("level"))
                warningMsg.SetActive(true);
            else
                NewGame(SceneName.TownScene.ToString());
        }
        public void RefuseWarningMsg()
        {
            warningMsg.SetActive(false);
        }

        public void NewGame(string sceneName)
        {
            float sfxVolume = 0.2f;
            float bgmVolume = 0.1f;

            if (PlayerPrefs.HasKey("sfxVolume"))
                sfxVolume = PlayerPrefs.GetFloat("sfxVolume");
            if ((PlayerPrefs.HasKey("bgmVolume")))
                bgmVolume = PlayerPrefs.GetFloat("bgmVolume");

            PlayerPrefs.DeleteAll();
            PlayerPrefs.SetFloat("sfxVolume", sfxVolume);
            PlayerPrefs.SetFloat("bgmVolume", bgmVolume);
            MainSystem.Instance.SceneManager.LoadScene(sceneName);
        }
        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
