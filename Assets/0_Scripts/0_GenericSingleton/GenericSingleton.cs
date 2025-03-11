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

    public partial class GenericSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance;
        public static T Instance { get { Initialize(); return instance; } }
    }

    public partial class GenericSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static void Initialize()
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>();
                if (instance == null)
                {
                    GameObject gameObject = new GameObject();
                    gameObject.name = $"[SingleTon]_{typeof(T).Name}";
                    instance = gameObject.AddComponent<T>();
                }
                DontDestroyOnLoad(instance.gameObject);
            }
        }
    }
}
