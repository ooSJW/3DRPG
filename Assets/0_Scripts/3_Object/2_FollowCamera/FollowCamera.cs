/*
	* Coder :
	* Last Update :
	* Information
*/
namespace project02
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading;
    using UnityEngine;

    public partial class FollowCamera : MonoBehaviour
    {
        [SerializeField] private LayerMask mask;
        [SerializeField] private Transform targetTransform;
        [SerializeField] private Vector3 offset;
        [SerializeField] private float sensitivityX;
        [SerializeField] private float sensitivityY;

        private Transform cameraTransform;
        private Vector3 OriginalOffset;
        private float mouseX;
        private float mouseY;
    }
    public partial class FollowCamera : MonoBehaviour
    {
        private void Allocate()
        {
            cameraTransform = transform.GetChild(0);
            OriginalOffset = offset;
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
    public partial class FollowCamera : MonoBehaviour
    {
        private void Start()
        {
            Initialize();
        }

        private void LateUpdate()
        {
            offset = (Vector3)(transform.localToWorldMatrix * OriginalOffset);
            Rotate();
            SpringArm();
        }
    }
    public partial class FollowCamera : MonoBehaviour
    {
        private void Follow()
        {
            transform.position = targetTransform.position;
            cameraTransform.position = Vector3.Lerp(cameraTransform.position, targetTransform.position + offset, 5 * Time.deltaTime);
        }

        private void SpringArm()
        {
            Debug.DrawRay(targetTransform.position, offset.normalized * offset.magnitude, Color.blue);
            if (Physics.Raycast(targetTransform.position, offset.normalized, out RaycastHit hit, offset.magnitude, mask))
                cameraTransform.position = hit.point;
            else
                Follow();
        }

        private void Rotate()
        {
            bool showCursor = MainSystem.Instance.UIManager.UIController.ShowCursor;
            if (!showCursor)
            {
                mouseX += Input.GetAxisRaw("Mouse X") * sensitivityX * Time.deltaTime;
                mouseY += Input.GetAxisRaw("Mouse Y") * sensitivityY * Time.deltaTime;

                // Mathf.Clamp(타겟 , 최소값 , 최대값);
                mouseY = Mathf.Clamp(mouseY, -60f, 90f);
                transform.eulerAngles = new Vector3(-mouseY, mouseX, 0);
            }
        }
    }
}
