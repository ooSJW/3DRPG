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

    public partial class LineRendererTest : MonoBehaviour
    {
        [SerializeField] LineRenderer lineRenderer;
        public int numberOfSegments;
        public float radius;
        public float angle;
    }
    public partial class LineRendererTest : MonoBehaviour
    {
        private void Allocate()
        {
            numberOfSegments = 20;
            radius = 17.0f;
            angle = 60.0f;
            lineRenderer.positionCount = numberOfSegments + 2;
            lineRenderer.useWorldSpace = true;
        }
        public void Initialize()
        {
            Allocate();
            Setup();
        }
        private void Setup()
        {

        }

        private void Start()
        {
            Initialize();
            float angleIncrement = angle / numberOfSegments;
            float currentAngle = -angle / 2.0f;

            for (int i = 0; i <= numberOfSegments; i++)
            {
                float x = Mathf.Cos(Mathf.Deg2Rad * currentAngle) * radius;
                float z = Mathf.Sin(Mathf.Deg2Rad * currentAngle) * radius;

                Vector3 point = new Vector3(x, 0.0f, z);
                lineRenderer.SetPosition(i, point);

                currentAngle += angleIncrement;
            }

            // 부채꼴의 끝점을 잇기 위해 마지막 점을 중심점으로 설정
            lineRenderer.SetPosition(numberOfSegments + 1, Vector3.zero);
        }
    }
}
