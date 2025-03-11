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
    using UnityEngine.UI;

    public partial class InfoUI : MonoBehaviour // Data Field
    {
        [SerializeField] private Image fillImage;
        Camera mainCamera;
    }
    public partial class InfoUI : MonoBehaviour // Initialize
    {
        private void Allocate()
        {
            fillImage.fillAmount = 1;
            mainCamera = Camera.main;
        }
        public virtual void Initialize()
        {
            Allocate();
            Setup();
        }
        private void Setup()
        {

        }
    }
    public partial class InfoUI : MonoBehaviour // Main
    {
        protected virtual void Update()
        {
            transform.LookAt(mainCamera.transform.position);
        }
    }
    public partial class InfoUI : MonoBehaviour // Property
    {
        public void SetHpUI(float currentHP, float maxHP)
        {
            fillImage.fillAmount = currentHP / maxHP;
        }
    }
    public partial class InfoUI : MonoBehaviour // Virtual Property
    {
        public virtual void SetLevelText(int levelValue) { }
        public virtual void SetExpText(float expValue, float maxExpValue) { }
        public virtual void SetHPText(float hpValue, float maxHPValue) { }
    }
}
