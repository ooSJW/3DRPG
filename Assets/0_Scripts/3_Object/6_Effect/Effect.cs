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

    public partial class Effect : MonoBehaviour // DataField
    {
        private float lifeTime = 0.3f;
        private float intervalTime = 0;
    }
    public partial class Effect : MonoBehaviour // Main
    {
        private void Update()
        {
            intervalTime += Time.deltaTime;
            if (intervalTime >= lifeTime)
            {
                intervalTime = 0;
                MainSystem.Instance.PoolManager.Despawn(this.gameObject);
            }
        }
    }
}
