/*
	* Coder :
	* Last Update :
	* Information
*/
namespace project02
{
    using DG.Tweening;
    using FMOD;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using static project02.PlayerStatData;
    using static UnityEngine.Rendering.HableCurve;

    public partial class BossCombat : EnemyCombat // Data Property
    {
        public override SkillName BossSkill
        {
            get => bossSkill;
            set
            {
                if (bossSkill != value)
                {
                    bossSkill = value;
                    enemy.IsAttack = true;
                    switch (bossSkill)
                    {
                        case SkillName.BossWind:
                            enemy.EnemyMovement.SetTargeting(false);
                            enemy.EnemyMovement.StopNavSetting();
                            enemy.EnemyAnimation.UseSkill(SkillName.BossWind);
                            skillEnable = false;
                            break;

                        case SkillName.BossShootEnergy:
                            enemy.EnemyMovement.SetTargeting(false);
                            enemy.EnemyMovement.StopNavSetting();
                            enemy.EnemyAnimation.UseSkill(SkillName.BossShootEnergy);
                            skillEnable = false;
                            break;

                        default:
                            enemy.IsAttack = false;
                            break;
                    }
                }
                bossSkill = value;
            }
        }
    }
    public partial class BossCombat : EnemyCombat // Data Field
    {
        public Dictionary<SkillName, SkillBase> BossSkillDict { get; private set; }
        [SerializeField] private LayerMask layer;

        private List<Player> hitPlayerList;

        private float intervalTime = 0;
        private float skillCollingTime = 7f;

        public Material dangerMaterial;

        private GameObject dangerZone;

        private Material dangerZoneMaterial;
        private MeshFilter meshFilter;
        private MeshRenderer meshRenderer;
        private Mesh dangerMesh;


        public int segments = 30;

    }


    public partial class BossCombat : EnemyCombat // Initialize
    {
        private void Allocate()
        {
            BossSkillDict = new Dictionary<SkillName, SkillBase>();
            hitPlayerList = new List<Player>();

            meshFilter = gameObject.AddComponent<MeshFilter>();
            meshRenderer = gameObject.AddComponent<MeshRenderer>();

            dangerMaterial = new Material(Shader.Find("Unlit/Color"));
            dangerMaterial.color = new Color(1f, 0f, 0f, 0.5f);

            meshRenderer.material = dangerMaterial;
            dangerZoneMaterial = Resources.Load<Material>("Material/DangerZone");
        }
        public override void Initialize(Enemy enemyValue)
        {
            base.Initialize(enemyValue);

            Allocate();
            Setup();
            SkillInitialize();
        }
        private void Setup()
        {

        }
    }


    public partial class BossCombat : EnemyCombat // Main
    {
        public override void Progress()
        {
            if (skillEnable && enemy.Target != null)
            {
                intervalTime += Time.deltaTime;
                if (intervalTime >= skillCollingTime)
                {
                    RandomPattern();
                    intervalTime = 0;
                }
            }
            else
                intervalTime = 0;
        }
    }
    public partial class BossCombat : EnemyCombat // Private Property
    {
        private void SkillInitialize()
        {
            EnemyStatInformation statInfo = enemy.EnemyStatInformation;

            if (statInfo.useable_skill.Length > BossSkillDict.Count)
            {
                Type type = typeof(Enemy);
                string nameSpace = type.Namespace;
                for (int i = 0; i < statInfo.useable_skill.Length; i++)
                {
                    string skillName = statInfo.useable_skill[i];
                    Type skill = Type.GetType(nameSpace + "." + skillName);

                    if (skill != null)
                    {
                        var skillComponent = gameObject.AddComponent(skill);
                        if (skillComponent is SkillBase)
                        {
                            SkillBase skillBase = (SkillBase)skillComponent;
                            skillBase.Initialize(enemy);
                            BossSkillDict.Add(skillBase.GetSkillName(), skillBase);
                        }
                    }
                }
            }
        }
    }
    public partial class BossCombat : EnemyCombat // Property
    {
        public override void BaseAttackFilter()
        {
            EnemyStatInformation statInfo = enemy.EnemyStatInformation;

            Vector3 center = (statInfo.attackRange * 0.5f) * transform.forward + transform.position;
            Vector3 size = new Vector3(statInfo.attackRange, 2f, 2f);
            Collider[] targetCollider = Physics.OverlapBox(center, size * 0.5f, Quaternion.identity, layer);

            for (int i = 0; i < targetCollider.Length; ++i)
            {
                Player hitPlayer = targetCollider[i].GetComponent<Player>();
                if (hitPlayer != null)
                {
                    hitPlayerList.Add(hitPlayer);
                    Vector3 hitPoint = targetCollider[i].ClosestPoint(transform.position);
                    hitPoint.y += 0.8f;
                    MainSystem.Instance.PoolManager.Spawn(PoolObject.PlayerHitEffect.ToString(), null, hitPoint);
                }
            }
            SendDamage();
        }


        public override void DrawDangerZone()
        {
            SkillInformation skillInfo = BossSkillDict[bossSkill].SkillInfo;

            /*구버전/ShaderGraph로 변경
            dangerMesh = new Mesh();
            List<Vector3> vertices = new List<Vector3>();
            List<int> triangles = new List<int>();

            Vector3 bossPosition = transform.position;
            bossPosition.y += heightOffset;
            vertices.Add(Vector3.zero);

            float startAngle = -angleRange / 2f;
            float endAngle = angleRange / 2f;

            for (int i = 0; i <= segments; i++)
            {
                float angle = Mathf.Lerp(startAngle, endAngle, (float)i / segments);
                Vector3 dir = Quaternion.Euler(0, angle, 0) * Vector3.forward;
                Vector3 point = dir * radius;
                vertices.Add(point);
            }

            for (int i = 1; i < vertices.Count - 1; i++)
            {
                triangles.Add(0);
                triangles.Add(i);
                triangles.Add(i + 1);
            }

            dangerMesh.vertices = vertices.ToArray();
            dangerMesh.triangles = triangles.ToArray();
            dangerMesh.RecalculateNormals();

            meshFilter.mesh = dangerMesh;

            meshFilter.transform.position = bossPosition;
            meshFilter.transform.rotation = transform.rotation;
            meshRenderer.enabled = true;
            StartCoroutine(StartDrawDangerZone());
            */
            float radius = skillInfo.range;
            float yawAngle = skillInfo.angle_range;

            dangerZone = MainSystem.Instance.PoolManager.Spawn(PoolObject.DangerZone.ToString(), transform);
            DangerZone zone = dangerZone.GetComponent<DangerZone>();

            zone.Initialize(radius, yawAngle, 0.9f);
            zone.RequestDrawZone(transform.rotation.eulerAngles, yawAngle);
        }

        private IEnumerator StartDrawDangerZone()
        {
            SkillInformation skillInfo = BossSkillDict[bossSkill].SkillInfo;

            float radius = skillInfo.range;
            float angleRange = skillInfo.angle_range;
            DangerZone zone = MainSystem.Instance.PoolManager.Spawn(PoolObject.DangerZone.ToString(), transform).GetComponent<DangerZone>();
            zone.Initialize(radius, angleRange);
            dangerZoneMaterial.SetFloat("Radius", radius);
            dangerZoneMaterial.SetFloat("Angle", angleRange);

            yield break;
        }

        public override void ClearDangerZone()
        {
            //meshRenderer.enabled = false;
            MainSystem.Instance.PoolManager.Despawn(dangerZone.gameObject);
        }

        public override void SkillFilter()
        {
            SkillInformation skillInfo = BossSkillDict[bossSkill].SkillInfo;
            Vector3 center = transform.position - (transform.forward * 0.5f);
            Collider[] targetCollider = Physics.OverlapSphere(center, skillInfo.range, layer);

            for (int i = 0; i < targetCollider.Length; ++i)
            {
                Vector3 direction = targetCollider[i].transform.position - center;

                float angle = Vector3.SignedAngle(transform.forward, direction, Vector3.up);

                if (Mathf.Abs(angle) <= skillInfo.angle_range * 0.5f)
                {
                    hitPlayerList.Add(targetCollider[i].GetComponent<Player>());
                    Vector3 hitPoint = targetCollider[i].ClosestPoint(transform.position);
                    hitPoint.y += 0.8f;
                    MainSystem.Instance.PoolManager.Spawn(PoolObject.PlayerHitEffect.ToString(), null, hitPoint);
                }
            }
            SendDamage();
        }


        public void SendDamage()
        {
            EnemyStatInformation statInfo = enemy.EnemyStatInformation;
            float skillDamage = 0;

            if (BossSkillDict.ContainsKey(bossSkill))
                skillDamage = BossSkillDict[bossSkill].SkillDamage;

            for (int i = 0; i < hitPlayerList.Count; ++i)
            {
                enemy.SendDamage(hitPlayerList[i], statInfo, skillDamage);
            }
            hitPlayerList.Clear();
        }

        public override void WindSkill()
        {
            Vector3 direction = (enemy.Target.position - transform.position).normalized;
            Vector3 destPosition;
            if (Physics.Raycast(transform.position, direction, out RaycastHit hit, 15f, LayerMask.GetMask("Ground", "BossRoomDoor")))
                destPosition = hit.point;
            else
                destPosition = transform.position + direction * 15f;

            transform.DOMove(destPosition, 1f);
        }

        public override void RandomPattern()
        {
            int randomSkill = UnityEngine.Random.Range((int)SkillName.BossWind, (int)SkillName.BossShootEnergy + 1);
            switch (randomSkill)
            {
                case (int)SkillName.BossWind:
                    BossSkill = SkillName.BossWind;
                    break;
                case (int)SkillName.BossShootEnergy:
                    BossSkill = SkillName.BossShootEnergy;
                    break;
            }
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            if (!Application.isPlaying) return;

            Gizmos.color = Color.red;

            Vector3 origin = transform.position;
            Vector3 forward = transform.forward;
            SkillInformation skillInfo = BossSkillDict[SkillName.BossShootEnergy].SkillInfo;
            float angle = skillInfo.angle_range;
            float radius = skillInfo.range;
            float halfAngle = angle * 0.5f;
            float step = angle / segments;

            for (int i = 0; i <= segments; i++)
            {
                float currentAngle = -halfAngle + step * i;
                Quaternion rotation = Quaternion.Euler(0, currentAngle, 0);
                Vector3 dir = rotation * forward;

                Vector3 endPoint = origin + dir.normalized * radius;
                Gizmos.DrawLine(origin, endPoint);

                // 선으로 호 연결 (부채꼴 외곽선)
                if (i > 0)
                {
                    float prevAngle = -halfAngle + step * (i - 1);
                    Vector3 prevDir = Quaternion.Euler(0, prevAngle, 0) * forward;
                    Vector3 prevPoint = origin + prevDir.normalized * radius;

                    Gizmos.DrawLine(prevPoint, endPoint);
                }
            }
        }
#endif
    }
}
