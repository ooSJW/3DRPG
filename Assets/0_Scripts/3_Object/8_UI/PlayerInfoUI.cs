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
    using TMPro;

    public partial class PlayerInfoUI : InfoUI // Data Field
    {
        [SerializeField] private TMP_Text levelText;
        [SerializeField] private TMP_Text expText;
        [SerializeField] private TMP_Text hpText;
    }
    public partial class PlayerInfoUI : InfoUI // Initialize
    {
        private void Allocate()
        {

        }
        public override void Initialize()
        {
            base.Initialize();

            Allocate();
            Setup();
        }
        private void Setup()
        {

        }
    }

    public partial class PlayerInfoUI : InfoUI // Main
    {
        protected override void Update()
        {

        }
    }
    public partial class PlayerInfoUI : InfoUI // Property
    {
        public override void SetLevelText(int levelValue)
        {
            levelText.text = $"LV. {levelValue}";
        }

        public override void SetExpText(float expValue, float maxExpValue)
        {
            expText.text = $"exp : {((expValue / maxExpValue) * 100).ToString("F2")}";
        }

        public override void SetHPText(float hpValue, float maxHPValue)
        {
            hpText.text = $"{hpValue} / {maxHPValue}";
        }
    }
}
