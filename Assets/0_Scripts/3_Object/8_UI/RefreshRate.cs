/*
	* Coder :
	* Last Update :
	* Information
*/
namespace project02
{
    using System.Collections;
    using System.Collections.Generic;
    using TMPro;
    using UnityEngine;

    public class RefreshRate : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI refreshRate;

        private void Update()
        {
            refreshRate.text = $"{Screen.currentResolution.refreshRate}";
        }
    }
}
