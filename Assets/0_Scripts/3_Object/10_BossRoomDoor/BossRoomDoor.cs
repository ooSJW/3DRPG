/*
	* Coder :
	* Last Update :
	* Information
*/
namespace project02
{
    using DG.Tweening;
    using System.Collections;
    using System.Collections.Generic;
    using Unity.VisualScripting;
    using UnityEngine;
    using UnityEngine.Assertions.Must;

    public partial class BossRoomDoor : MonoBehaviour // Data Field
    {
        [SerializeField] private Collider doorDetector;
        [SerializeField] private Collider doorCollider;
        [SerializeField] private GameObject doorEffect;

        private bool isBossDead = false;
        public bool IsBossDead
        {
            get => isBossDead;
            set
            {
                isBossDead = value;
                if (isBossDead)
                    gameObject.SetActive(false);
            }
        }
    }
    public partial class BossRoomDoor : MonoBehaviour // Initialize
    {
        private void Allocate()
        {
            doorDetector.isTrigger = true;
            doorDetector.enabled = true;
            doorCollider.isTrigger = false;
            doorCollider.enabled = false;
            doorEffect.SetActive(false);
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

    public partial class BossRoomDoor : MonoBehaviour // Trigger Event
    {
        private void Start()
        {
            Initialize();
        }
    }
    public partial class BossRoomDoor : MonoBehaviour // Trigger Event
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                doorEffect.SetActive(true);
                doorCollider.enabled = true;
            }
        }
    }
}
