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

    public partial class PortalNpc : MonoBehaviour
    {
        private bool canUsePortal = false;
        [SerializeField] private GameObject portalUI;

        private void Start()
        {
            portalUI.SetActive(false);
        }
        private void Update()
        {
            if (canUsePortal)
            {
                if (Input.GetKeyDown(KeyCode.R))
                {
                    portalUI.SetActive(true);
                    try
                    {
                        MainSystem.Instance.SceneManager.ActiveScene.FollowCamera.StartDialouge(transform);
                        MainSystem.Instance.UIManager.UIController.ShowCursor = true;
                    }
                    catch (System.Exception ex)
                    {
                        Debug.LogException(ex);
                    }
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
                canUsePortal = true;
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
                canUsePortal = false;
        }
    }
}
