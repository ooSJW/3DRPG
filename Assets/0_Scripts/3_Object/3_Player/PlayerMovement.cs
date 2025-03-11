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
    using static project02.PlayerStatData;

    public partial class PlayerMovement : MonoBehaviour // Data Field
    {
        private float moveSpeed;
        private float originSpeed;
        private float maxSpeed;
        private float acceleration;
        private float evadeSpeed;
        private float gravity = -9.81f;
        public bool isEvade = false;
        private Vector3 evadeDirection = Vector3.zero;

        [SerializeField] private Rigidbody rigid;
        [SerializeField] private CharacterController characterController;

        [SerializeField] private LayerMask groundLayer;
        private Player player;

        private float vertical;
        private float horizontal;

    }
    public partial class PlayerMovement : MonoBehaviour // Initialize
    {
        private void Allocate()
        {

        }
        public void Initialize(Player playerValue)
        {
            player = playerValue;

            Allocate();
            Setup();
        }
        private void Setup()
        {
            PlayerStatInformation statInfo = player.PlayerStatInformation;

            moveSpeed = statInfo.moveSpeed;
            maxSpeed = statInfo.maxSpeed;
            originSpeed = moveSpeed;
            acceleration = statInfo.acceleration;
            evadeSpeed = statInfo.evadeSpeed;
        }
    }
    public partial class PlayerMovement : MonoBehaviour // Progress
    {
        public void Progress()
        {
            GetInputValue();
        }
        public void FixedProgress()
        {
            Movement();
            OnEvade();
        }
    }

    public partial class PlayerMovement : MonoBehaviour // Property
    {
        public void Evade()
        {
            isEvade = true;
            evadeDirection = transform.localToWorldMatrix * new Vector3(horizontal, 0, vertical).normalized;
            MainSystem.Instance.SoundManager.SoundController.SpecialEffects.PlaySfx(SoundClipName.PlayerEvade);
        }
    }
    public partial class PlayerMovement : MonoBehaviour // Private Property
    {
        private void GetInputValue()
        {
            PlayerInput playerInput = player.PlayerInput;
            vertical = playerInput.GetAxisRawZ();
            horizontal = playerInput.GetAxisRawX();
        }

        private void Movement()
        {
            switch (player.State)
            {
                case PlayerState.Idle:
                    moveSpeed = originSpeed;
                    if (horizontal != 0 || vertical != 0)
                        player.State = PlayerState.Move;
                    break;

                case PlayerState.Move:
                    OnMove();
                    break;

                case PlayerState.Attack:
                    break;
            }
            characterController.Move(new Vector3(0, gravity, 0));
        }

        private void OnEvade()
        {
            if (isEvade)
            {
                Vector3 velocity = evadeDirection * evadeSpeed * Time.deltaTime;
                velocity.y = gravity * Time.deltaTime;
                if (Physics.Raycast(transform.position + Vector3.up + evadeDirection, Vector3.down, out RaycastHit hit, 2f, groundLayer))
                    characterController.Move(velocity);
            }
        }
        private void OnMove()
        {
            PlayerInput playerInput = player.PlayerInput;
            if (playerInput.RunKeyPress())
            {
                if (moveSpeed < maxSpeed)
                    moveSpeed += acceleration;
                else
                    moveSpeed = maxSpeed;
            }
            else
                moveSpeed = originSpeed;

            Vector3 direction = transform.localToWorldMatrix * new Vector3(horizontal, 0, vertical).normalized;
            Vector3 velocity = direction * moveSpeed * Time.deltaTime;

            if (playerInput.BackWalkKeyPress())
                velocity *= 0.5f;
            else if (playerInput.SideWalkKeyPress())
                velocity *= 0.75f;

            velocity.y = gravity * Time.deltaTime;

            if (Physics.Raycast(transform.position + Vector3.up + direction, Vector3.down, out RaycastHit hit, 2f, groundLayer))
                characterController.Move(velocity * Time.deltaTime);


            if (Mathf.Approximately(characterController.velocity.x, 0) && Mathf.Approximately(characterController.velocity.z, 0))
                player.State = PlayerState.Idle;
        }
    }
}
