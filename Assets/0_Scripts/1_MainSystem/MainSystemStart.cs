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

    public class MainSystemStart : MonoBehaviour
    {
        private void Awake()
        {
            MainSystem.Instance.MainSystemStart();
        }
    }
}
