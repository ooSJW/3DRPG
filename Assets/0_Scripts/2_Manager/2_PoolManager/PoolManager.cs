/*
	* Coder :
	* Last Update :
	* Information
*/
namespace project02
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;


    public partial class PoolManager : MonoBehaviour // Pool객체를 관리할 매니저 클래스
    {
        private Dictionary<string, Pool> poolDictionary = default;
    }
    public partial class PoolManager  // Initialize
    {
        private void Allocate()
        {
            poolDictionary = new Dictionary<string, Pool>();
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
    public partial class PoolManager  
    {
        public void Register()
        {
            poolDictionary.Clear();
            List<GameObject> poolableList = MainSystem.Instance.SceneManager.ActiveScene.poolableObject;
            for (int i = 0; i < poolableList.Count; i++)
            {
                Pool pool = new Pool(poolableList[i]);
                pool.Register();
                poolDictionary.Add(poolableList[i].name, pool);
            }
        }

        public GameObject RequestSpawn(string name, Transform parent = null, Vector3 spawnPosition = default)
        {
            return poolDictionary[name].Spawn(parent, spawnPosition);
        }

        public void RequestDespawn(GameObject poolableObject)
        {
            poolDictionary[poolableObject.name].Despawn(poolableObject);
        }
    }

    public partial class PoolManager  // Inner Class
    {
        public class Pool
        {
            private Transform parent;
            private GameObject originalPrefab;
            private Queue<GameObject> poolingObjectQueue;
            private int initialCount;

            public Pool(GameObject originalPrefabValue, int initialCountValue = 10)
            {
                poolingObjectQueue = new Queue<GameObject>();
                originalPrefab = originalPrefabValue;
                parent = new GameObject() { name = $"Root_{originalPrefab.name}" }.transform;
                initialCount = initialCountValue;
            }

            public void Register()
            {
                for (int i = 0; i < initialCount; ++i)
                {
                    GameObject poolableObject = Instantiate(originalPrefab, parent.transform.position, Quaternion.identity, parent);
                    poolableObject.name = originalPrefab.name;
                    poolableObject.SetActive(false);
                    poolingObjectQueue.Enqueue(poolableObject);
                }
            }

            public GameObject Spawn(Transform parentValue = null, Vector3 spawnPosition = default)
            {
                GameObject poolableObject = null;
                if (poolingObjectQueue.Count > 0)
                {
                    poolableObject = poolingObjectQueue.Dequeue();

                    poolableObject.transform.SetParent(parentValue);
                    poolableObject.transform.position = spawnPosition == default ? parentValue.position : spawnPosition;

                    poolableObject.SetActive(true);
                }
                else
                {
                    if (spawnPosition != default)
                        poolableObject = Instantiate(originalPrefab, spawnPosition, Quaternion.identity, parentValue);
                    else
                        poolableObject = Instantiate(originalPrefab, parentValue);

                    poolableObject.name = originalPrefab.name;
                    poolableObject.transform.SetParent(parentValue);
                }
                return poolableObject;
            }

            public void Despawn(GameObject poolObject)
            {
                poolObject.SetActive(false);
                poolingObjectQueue.Enqueue(poolObject);
                poolObject.transform.SetParent(parent);
            }
        }
    }
}