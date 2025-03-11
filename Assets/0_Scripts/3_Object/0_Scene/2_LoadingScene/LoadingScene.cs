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

    public partial class LoadingScene : MonoBehaviour
    {
        [SerializeField] private Image progressBar;
    }
    public partial class LoadingScene : MonoBehaviour
    {
        private void Allocate()
        {

        }
        public  void Initialize()
        {
            Allocate();
            Setup();
        }
        private void Setup()
        {

        }
    }
    public partial class LoadingScene : MonoBehaviour // Main
    {
        private void Start()
        {
            StartCoroutine(LoadSceneProcess());
        }
    }

    public partial class LoadingScene : MonoBehaviour // Coroutine
    {
        IEnumerator LoadSceneProcess()
        {
            string sceneName = MainSystem.Instance.SceneManager.LoadSceneName;
            AsyncOperation loadScene = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);
            loadScene.allowSceneActivation = false;
            float timer = 0f;
            while (!loadScene.isDone)
            {
                yield return null;
                if (loadScene.progress < 0.9f)
                {
                    progressBar.fillAmount = loadScene.progress;
                }
                else
                {
                    timer += Time.unscaledDeltaTime;
                    progressBar.fillAmount = Mathf.Lerp(0.9f, 1f, timer);
                    if (progressBar.fillAmount >= 1f)
                    {
                        loadScene.allowSceneActivation = true;
                        yield break;
                    }
                }
            }
        }
    }
}
