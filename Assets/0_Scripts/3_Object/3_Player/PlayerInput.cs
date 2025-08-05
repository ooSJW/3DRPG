/*
	* Coder :
	* Last Update :
	* Information
*/
namespace project02
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.InputSystem;
    using static project02.PlayerStatData;

    public partial class PlayerInput : MonoBehaviour // Data Field
    {
        private Player player;
        private Action OnUpdate = null;
        public bool Moveable { get; set; } = true;
        private bool evadeAble = true;
        private float evadeIntervalTime = 0;

        public Dictionary<int, SkillBase> commandDict;
        private PlayerSkillInput skillInput;
        private int inputBuffer;
    }
    public partial class PlayerInput : MonoBehaviour // Initialize
    {
        private void Allocate()
        {
            commandDict = new Dictionary<int, SkillBase>();

            OnUpdate -= EvadeKeyPress;
            OnUpdate += EvadeKeyPress;
            OnUpdate -= WeaponKeyPress;
            OnUpdate += WeaponKeyPress;
            OnUpdate -= Heal;
            OnUpdate += Heal;
            InitSkillInput();
        }
        public void Initialize(Player playerValue)
        {
            player = playerValue;

            Allocate();
            Setup();
        }
        private void Setup()
        {

        }
    }
    public partial class PlayerInput : MonoBehaviour // Progress
    {
        public void Progress()
        {
            OnUpdate?.Invoke();
        }
        public void FixedProgress()
        {
            EvadeTimer();
        }
    }
    public partial class PlayerInput : MonoBehaviour // Property
    {
        private void InitSkillInput()
        {
            // 초기화시 비트마스크를 통한 키값 검사를 위해 InputSystem에 식 할당
            skillInput = new PlayerSkillInput();
            skillInput.Enable();

            skillInput.CommandKey.e.performed += ctx => inputBuffer |= (int)InputKey.e;
            skillInput.CommandKey.e.canceled += ctx => inputBuffer &= ~(int)InputKey.e;

            skillInput.CommandKey.f.performed += ctx => inputBuffer |= (int)InputKey.f;
            skillInput.CommandKey.f.canceled += ctx => inputBuffer &= ~(int)InputKey.f;

            skillInput.CommandKey.s.performed += ctx => inputBuffer |= (int)InputKey.s;
            skillInput.CommandKey.s.canceled += ctx => inputBuffer &= ~(int)InputKey.s;

            skillInput.CommandKey.q.performed += ctx => inputBuffer |= (int)InputKey.q;
            skillInput.CommandKey.q.canceled += ctx => inputBuffer &= ~(int)InputKey.q;

            skillInput.CommandKey.lShift.performed += ctx => inputBuffer |= (int)InputKey.leftshift;
            skillInput.CommandKey.lShift.canceled += ctx => inputBuffer &= ~(int)InputKey.leftshift;

            skillInput.CommandKey.mouseLeft.performed += ctx => inputBuffer |= (int)InputKey.mouse0;
            skillInput.CommandKey.mouseLeft.canceled += ctx => inputBuffer &= ~(int)InputKey.mouse0;

            skillInput.CommandKey.mouseRight.performed += ctx => inputBuffer |= (int)InputKey.mouse1;
            skillInput.CommandKey.mouseRight.canceled += ctx => inputBuffer &= ~(int)InputKey.mouse1;

            // 모든 전투 키에서 공통으로 사용될 기능은 한 번에 할당
            foreach (InputAction action in skillInput.CommandKey.Get())
            {
                action.performed += ctx => OnAnyKey();
            }
        }
        private void OnAnyKey()
        {
            bool attackAble = player.State != PlayerState.Death &&
                Moveable &&
                player.WeaponState == PlayerWeaponState.Equip &&
                !player.PlayerMovement.isEvade &&
                !player.PlayerCombat.IsAttacking;

            // OnAnyKey 내부
            if (attackAble)
            {
                bool isMouse0Pressed = (inputBuffer & (int)InputKey.mouse0) != 0;
                if (commandDict.TryGetValue(inputBuffer, out SkillBase skill) && !skill.IsCoolTime)
                {
                    SkillName skillName = skill.GetSkillName();

                    if (skillName != SkillName.PistolBase)
                    {
                        player.PlayerSkill = skillName;
                        return;
                    }
                }
                if (isMouse0Pressed)
                    PistolBase();
            }
        }

        private void Heal()
        {
            if (player.CloseToHealer)
            {
                if (Input.GetKeyDown(KeyCode.R))
                    player.Hp = player.PlayerStatInformation.maxHp;
            }
        }
        private void PistolBase()
        {
            bool showCursor = MainSystem.Instance.UIManager.UIController.ShowCursor;

            if (!showCursor)
                player.PlayerSkill = SkillName.PistolBase;
        }

        public void SetMoveable(bool moveableValue)
        {
            Moveable = moveableValue;
        }
        public float GetAxisRawZ()
        {
            return Moveable ? Input.GetAxisRaw("Vertical") : 0;
        }

        public float GetAxisRawX()
        {
            return Moveable ? Input.GetAxisRaw("Horizontal") : 0;
        }

        public bool RunKeyPress()
        {
            if (Moveable && (Input.GetButton("Vertical") || Input.GetButton("Horizontal")) && Input.GetKey(KeyCode.LeftShift))
                return true;
            return false;
        }

        public bool SideWalkKeyPress()
        {
            if (Moveable && Input.GetButton("Horizontal") && !Input.GetKey(KeyCode.W))
                return true;
            return false;
        }

        public bool BackWalkKeyPress()
        {
            if (Moveable && Input.GetKey(KeyCode.S))
                return true;
            return false;
        }

        public void EvadeKeyPress()
        {
            if (Moveable && Input.GetKeyDown(KeyCode.Space) && evadeAble)
            {
                float inputMag = new Vector3(GetAxisRawX(), 0, GetAxisRawZ()).normalized.magnitude;
                if (!Mathf.Approximately(inputMag, 0))
                {
                    player.State = PlayerState.Evade;
                    evadeAble = false;
                }
            }
        }

        public void WeaponKeyPress()
        {
            if (!player.PlayerCombat.IsAttacking && Moveable)
            {
                switch (player.WeaponState)
                {
                    case PlayerWeaponState.None:
                        break;
                    case PlayerWeaponState.Equip:
                        if (Input.GetKeyDown(KeyCode.Tab))
                            player.WeaponState = PlayerWeaponState.Unequip;
                        break;
                    case PlayerWeaponState.Unequip:
                        if (Input.GetKeyDown(KeyCode.Tab))
                            player.WeaponState = PlayerWeaponState.Equip;
                        break;
                }
            }
        }
    }
    public partial class PlayerInput : MonoBehaviour // Private Property
    {
        private void EvadeTimer()
        {
            if (!evadeAble)
            {
                evadeIntervalTime += Time.fixedDeltaTime;
                if (evadeIntervalTime >= 1.5f)
                {
                    evadeAble = true;
                    evadeIntervalTime = 0;
                }
            }
            else
                evadeIntervalTime = 0;
        }
    }
}