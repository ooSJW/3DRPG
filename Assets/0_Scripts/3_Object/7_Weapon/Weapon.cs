/*
	* Coder :
	* Last Update :
	* Information
*/
namespace project02
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEditor;
    using UnityEngine;
    using UnityEngine.U2D;

    public partial class Weapon : MonoBehaviour // Data Field
    {
        [SerializeField] private CombatObjectBase owner;
        [SerializeField] private Vector3 lossyScale;

        public CombatObjectBase Owner { get => owner; private set { owner = value; } }
    }
    public partial class Weapon : MonoBehaviour // Initialize
    {
        private void Allocate()
        {

        }
        public void Initialize(CombatObjectBase owner)
        {
            Allocate();
            Setup();
        }
        private void Setup()
        {

        }
    }
}