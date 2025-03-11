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
    using UnityEngine;

    public partial class DamageUI : MonoBehaviour // Data Field
    {
        [SerializeField] private TextMesh damageText;

        [SerializeField] private Color originColor;
        [SerializeField] private Color criticalColor;
        private Vector3 destPos = Vector3.zero;
    }

    public partial class DamageUI : MonoBehaviour // Initialize
    {
        private void Allocate()
        {
            transform.localScale = Vector3.one;
            destPos = transform.position + transform.up * 0.8f;
        }
        public void Initialize()
        {
            Allocate();
            SetUp();
        }
        private void SetUp()
        {

        }
    }
    public partial class DamageUI : MonoBehaviour // Main
    {
        private void Update()
        {
            if (Mathf.Approximately(transform.position.y, destPos.y))
                MainSystem.Instance.PoolManager.Despawn(gameObject);

            Vector3 resultPos = transform.position;
            resultPos.y = destPos.y;
            transform.position = Vector3.MoveTowards(transform.position, resultPos, 1.5f * Time.deltaTime);
            transform.LookAt(Camera.main.transform.position);
        }
    }
    public partial class DamageUI : MonoBehaviour // Property
    {
        public void SetDamageText(int damage, bool isCritical = false)
        {
            if (isCritical)
                damageText.color = criticalColor;
            else
                damageText.color = originColor;

            damageText.text = damage.ToString();
        }
    }
}
