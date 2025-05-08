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

    public partial class PlayerInput : MonoBehaviour // Data Field
    {
        private Player player;
        private Action commandProgress = null;
        public bool CanMove { get; set; } = true;
        private bool canEvade = true;
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

            commandProgress -= EvadeKeyPress;
            commandProgress += EvadeKeyPress;
            commandProgress -= WeaponKeyPress;
            commandProgress += WeaponKeyPress;
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
            commandProgress.Invoke();
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

            // 초기화 시 할당 (InputSystem사용)
            foreach (InputAction action in skillInput.CommandKey.Get())
            {
                action.performed += ctx => OnAnyKey();
            }
        }
        private void OnAnyKey()
        {
            bool canAttack = CanMove &&
                player.WeaponState == PlayerWeaponState.Equip &&
                !player.PlayerMovement.isEvade &&
                !player.PlayerCombat.IsAttack;

            // OnAnyKey 내부
            if (canAttack)
            {
                bool isMouse0Pressed = (inputBuffer & (int)InputKey.mouse0) != 0;
                if (commandDict.TryGetValue(inputBuffer, out SkillBase skill) && !skill.IsCoolTime)
                {
                    SkillName skillName = skill.GetSkillName();

                    if (skillName != SkillName.PistolBase)
                        player.PlayerSkill = skillName;
                    else
                        PistolBase();

                }
                else if (isMouse0Pressed)
                    PistolBase();
            }
        }

        private void PistolBase()
        {
            bool showCursor = MainSystem.Instance.UIManager.UIController.ShowCursor;

            if (!showCursor)
                player.PlayerSkill = SkillName.PistolBase;
        }

        public void SetCanMove(bool canMoveValue)
        {
            CanMove = canMoveValue;
        }
        public float GetAxisRawZ()
        {
            return CanMove ? Input.GetAxisRaw("Vertical") : 0;
        }

        public float GetAxisRawX()
        {
            return CanMove ? Input.GetAxisRaw("Horizontal") : 0;
        }

        public bool RunKeyPress()
        {
            if (CanMove && (Input.GetButton("Vertical") || Input.GetButton("Horizontal")) && Input.GetKey(KeyCode.LeftShift))
                return true;
            return false;
        }

        public bool SideWalkKeyPress()
        {
            if (CanMove && Input.GetButton("Horizontal") && !Input.GetKey(KeyCode.W))
                return true;
            return false;
        }

        public bool BackWalkKeyPress()
        {
            if (CanMove && Input.GetKey(KeyCode.S))
                return true;
            return false;
        }

        public void EvadeKeyPress()
        {
            if (CanMove && Input.GetKeyDown(KeyCode.Space) && canEvade)
            {
                float inputMag = new Vector3(GetAxisRawX(), 0, GetAxisRawZ()).normalized.magnitude;
                if (!Mathf.Approximately(inputMag, 0))
                {
                    player.State = PlayerState.Evade;
                    canEvade = false;
                }
            }
        }

        public void WeaponKeyPress()
        {
            if (!player.PlayerCombat.IsAttack && CanMove)
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
            if (!canEvade)
            {
                evadeIntervalTime += Time.fixedDeltaTime;
                if (evadeIntervalTime >= 1.5f)
                {
                    canEvade = true;
                    evadeIntervalTime = 0;
                }
            }
            else
                evadeIntervalTime = 0;
        }
    }
}