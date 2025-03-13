/*
	* Coder :
	* Last Update :
	* Information
*/
namespace project02
{
    using System.Collections;
    using System.Collections.Generic;
    using Unity.VisualScripting;
    using UnityEngine;
    using static project02.EnemyData;
    using static project02.ZoneData;

    public partial class ZoneObject : MonoBehaviour // Data Property
    {
        protected bool closeToPlayer = false;
        public virtual bool CloseToPlayer
        {
            get => closeToPlayer;
            set
            {
                if (closeToPlayer != value)
                {
                    if (value == true)
                        Spawn();
                    else
                    {
                        int currentCount = AllFieldEnemyObjectList.Count;
                        for (int i = 0; i < currentCount; i++)
                        {
                            Despawn(AllFieldEnemyObjectList[0]);
                        }
                    }
                }
                closeToPlayer = value;
            }
        }
    }


    public partial class ZoneObject : MonoBehaviour // Data Field
    {
        public List<Enemy> spawnableEnemyList = default;
        public List<Enemy> AllFieldEnemyObjectList { get; set; } = default;
        public int SpawnableMaxCount { get; private set; } = 0;

        [SerializeField] protected float radius = 15;
        [SerializeField] protected ZoneDetector zoneDetector = default;
        [SerializeField] protected LayerMask groundLayer;
        protected ZoneInformation zoneInformation = default;

        protected float respawnTime;
        protected float intervalTime;
    }


    public partial class ZoneObject : MonoBehaviour // Initialize
    {
        private void Allocate()
        {
            AllFieldEnemyObjectList = new List<Enemy>();
            spawnableEnemyList = new List<Enemy>();
        }
        public virtual void Initialize(ZoneInformation zoneInformationValue)
        {
            zoneInformation = zoneInformationValue;

            Allocate();
            Setup();
            zoneDetector.Initialize(this);
        }
        private void Setup()
        {
            SpawnableMaxCount = zoneInformation.max_spawn_count;
            respawnTime = zoneInformation.respawn_time;

            for (int i = 0; i < zoneInformation.spawnable_enemy_array.Length; ++i)
            {
                EnemyInformation enemyInformation =
                    MainSystem.Instance.DataManager.EnemyData.enemyDataDict[zoneInformation.spawnable_enemy_array[i].ToString()];
                spawnableEnemyList.Add(Resources.Load<Enemy>(enemyInformation.name));
            }
        }
    }
    public partial class ZoneObject : MonoBehaviour // Main
    {
        protected virtual void Update()
        {
            if (MainSystem.Instance.SceneManager.ActiveScene.name != SceneName.DungeonScene.ToString())
            {
                if (CloseToPlayer)
                {
                    if (AllFieldEnemyObjectList.Count < SpawnableMaxCount * 0.2f)
                    {
                        intervalTime += Time.deltaTime;
                        if (intervalTime >= respawnTime)
                        {
                            Spawn();
                            intervalTime = 0;
                        }
                    }
                }
            }
        }
    }

    public partial class ZoneObject : MonoBehaviour // Property
    {
        public Vector3 GetRandomPositionInZone()
        {
            Vector3 resultPosition = transform.position + UnityEngine.Random.onUnitSphere * (radius * 0.5f);
            resultPosition.y = transform.position.y;

            if (Physics.Raycast(resultPosition, Vector3.down, out RaycastHit hit, Mathf.Infinity, groundLayer))
            {
                resultPosition = hit.point;
                return resultPosition;
            }
            else
                return default;
        }

        public void Despawn(Enemy enemyValue)
        {
            AllFieldEnemyObjectList.Remove(enemyValue);
            MainSystem.Instance.PoolManager.Despawn(enemyValue.gameObject);
        }
    }

    public partial class ZoneObject : MonoBehaviour // Private Property
    {
        protected void Spawn()
        {
            while (AllFieldEnemyObjectList.Count < SpawnableMaxCount)
            {
                Vector3 resultPos = GetRandomPositionInZone();

                Enemy enemy =
                MainSystem.Instance.PoolManager.
                    Spawn(spawnableEnemyList[UnityEngine.Random.Range(0, spawnableEnemyList.Count)].name, transform, resultPos).GetComponent<Enemy>();


                MainSystem.Instance.EnemyManager.SignUpEnemy(enemy);
                enemy.transform.position = resultPos;
                enemy.OriginPosition = resultPos;

                enemy.SetZone(this);
                AllFieldEnemyObjectList.Add(enemy);
            }
        }
    }
    public partial class ZoneObject : MonoBehaviour // DrawGizmos
    {
        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, radius);
            Debug.DrawRay(transform.position, -transform.up, Color.yellow);
        }
    }
}
