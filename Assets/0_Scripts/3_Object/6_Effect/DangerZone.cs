using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerZone : MonoBehaviour
{
    [SerializeField] private Renderer renderer;
    private Material dangerZone;
    private Vector3 offset = new Vector3(0, 0.3f, 0);
    private float angle; // dangerZone의 각도 크기
    private float fillSpeed; // dangerZone이 base영역을 채우는 속도

    /// <summary>
    /// DangerZone Scale, materrial에 적용할 값 설정
    /// </summary>
    /// <param name="radius">dangerzone 범위 및 plane크기에 적용</param>
    /// <param name="angle">부채꼴의 각도</param>
    /// <param name="fillSpeed">부채꼴을 채울 속도</param>
    public void Initialize(float radius = 0, float angle = 0, float fillSpeed = 0.5f)
    {
        this.angle = angle;
        this.fillSpeed = fillSpeed;

        offset = new Vector3(0, 0.3f, 0);

        // plane은 x,z scale 1당 10m, 중앙에서 부터 그림.
        // 미터로 사용하기 위해 0.1을 곱하고, 중앙에서 한 방향으로만 그리기 때문에 2를 다시 곱해 사용
        Vector3 scale = Vector3.one * (radius * 0.2f);
        scale.y = 1;

        transform.localScale = scale;

        if (renderer is not null)
            dangerZone = renderer.material;
    }
    public void RequestDrawZone(float yawAngle)
    {
        StartCoroutine(ActiveDangerZone(yawAngle));
    }
    IEnumerator ActiveDangerZone(float yawAngle)
    {   // 범위 각도를 매개변수로 받은 후 그대로 적용
        if (dangerZone is null) yield break;

        float yawRotation = (yawAngle * 0.5f) - 90;
        Quaternion rotation = Quaternion.Euler(0, yawRotation, 0);
        transform.localRotation = rotation;

        dangerZone.SetFloat("_Radius", 0);
        dangerZone.SetFloat("_BaseRadius", 1);
        dangerZone.SetFloat("_SkillAngle", angle);

        yield return LerpDangerZoneArea();
    }

    IEnumerator LerpDangerZoneArea()
    {
        float value = 0;
        while (value < 1)
        { 
            // 전체 공격 범위를 보여주기 위해 반 투명 지역을 두고, 타이밍을 알 수 있도록 불투명한 색으로 내부를 채움.
            value = Mathf.MoveTowards(value, 1, fillSpeed * Time.smoothDeltaTime);
            dangerZone.SetFloat("_Radius", value);
            yield return null;
        }
    }
}
