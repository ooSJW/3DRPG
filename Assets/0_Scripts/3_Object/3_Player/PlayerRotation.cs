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
    using UnityEngine.UIElements;

    public partial class PlayerRotation : MonoBehaviour
    {
        private Player player;
        [SerializeField] private Transform cameraTransform;
    }
    public partial class PlayerRotation : MonoBehaviour
    {
        private void Allocate()
        {

        }
        public void Initialize(Player playerValue)
        {
            Allocate();
            Setup();

            player = playerValue;
        }
        private void Setup()
        {

        }
    }
    public partial class PlayerRotation : MonoBehaviour
    {
        public void Progress()
        {
            Rotate();
        }
    }
    public partial class PlayerRotation : MonoBehaviour
    {
        private void Rotate()
        {
            Quaternion rotate = cameraTransform.rotation;
            rotate.x = 0;
            rotate.z = 0;
            transform.rotation = rotate;
        }
    }
}
