/*
	* Coder :
	* Last Update :
	* Information
*/
namespace project02
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using Unity.VisualScripting;
    using UnityEditor;
    using UnityEngine;

    public partial class PlayerInput : MonoBehaviour // Data Field
    {
        private Player player;
        private Action commandProgress = null;
        private string[] inputBuffer;
        private int inputBufferIndex = 0;

        private float initialTime = 0.2f;
        private float inputIntervalTime = 0;

        public Dictionary<string, SkillBase> commandDict;
        private bool canEvade = true;
        private float evadeIntervalTime = 0;

        public bool CanMove { get; set; } = true;

        private StringBuilder compareTarget;
        private string compareTargetStr;
    }
    public partial class PlayerInput : MonoBehaviour // Initialize
    {
        private void Allocate()
        {
            commandDict = new Dictionary<string, SkillBase>();
            compareTarget = new StringBuilder();
            inputBuffer = new string[6];

            commandProgress -= EvadeKeyPress;
            commandProgress += EvadeKeyPress;
            commandProgress -= WeaponKeyPress;
            commandProgress += WeaponKeyPress;
            commandProgress -= CompareCommand;
            commandProgress += CompareCommand;
            commandProgress -= InputTimer;
            commandProgress += InputTimer;
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
        private void CompareCommand()
        {
            bool canAttack = CanMove &&
                            player.WeaponState == PlayerWeaponState.Equip &&
                            !player.PlayerMovement.isEvade &&
                            !player.PlayerCombat.IsAttack;
            if (canAttack)
            {
                if (SaveInputKey())
                {
                    for (int i = 0; i + 1 < inputBuffer.Length; i++)
                    {
                        compareTarget.Clear();
                        compareTarget.Append(inputBuffer[i]);
                        compareTarget.Append(inputBuffer[i + 1]);
                        compareTargetStr = compareTarget.ToString();

                        if (commandDict.ContainsKey(compareTargetStr))
                        {
                            if (!commandDict[compareTargetStr].IsCoolTime)
                            {
                                player.PlayerSkill = commandDict[compareTargetStr].GetSkillName();
                                return;
                            }
                        }
                        // 예시로 LShift + Q 조합을 사용할 때 키를 누른 순서와 상관없이 탐색하기 위해 버퍼의 요소 순서를 바꿔서도 탐색.
                        // Sort를 사용해 한 번만 검사하려다 마우스 입력, shift , ctrl 키 등을 검사할 때 모호성이 생길 것 같아 이중 검사.

                        compareTarget.Clear();
                        compareTarget.Append(inputBuffer[i + 1]);
                        compareTarget.Append(inputBuffer[i]);
                        compareTargetStr = compareTarget.ToString();
                        if (commandDict.ContainsKey(compareTargetStr))
                        {
                            if (!commandDict[compareTargetStr].IsCoolTime)
                            {
                                player.PlayerSkill = commandDict[compareTargetStr].GetSkillName();
                                return;
                            }
                        }

                    }
                    bool showCursor = MainSystem.Instance.UIManager.UIController.ShowCursor;
                    if (!showCursor && Input.GetMouseButton(0))
                        player.PlayerSkill = SkillName.PistolBase;
                }
            }
        }


        private void InputTimer()
        {
            /* 
             intervalTime == LoopTime
             initialTime == 배열 요소 초기화 시간( 현재 0.2f )
             */
            inputIntervalTime += Time.deltaTime;

            if (inputIntervalTime >= initialTime)
            {
                Array.Clear(inputBuffer, 0, inputBuffer.Length);
                compareTarget.Clear();
                inputIntervalTime = 0;
                inputBufferIndex = 0;
            }
        }

        private bool SaveInputKey()
        {
            if (Input.anyKey)
            {
                foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
                {
                    if (Input.GetKey(keyCode))
                    {
                        string inputKey = keyCode.ToString().ToLower();
                        if (!inputBuffer.Contains(inputKey) && inputBufferIndex < inputBuffer.Length)
                        {
                            inputBuffer[inputBufferIndex] = inputKey;
                            inputBufferIndex++;
                            break;
                        }
                    }
                }
                return true;
            }
            return false;
        }

    }
}