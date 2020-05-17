using System;
using JAM.Scripts.Animations;
using JAM.Scripts.Commands;
using JAM.Scripts.Input;
using JAM.Scripts.Managers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace JAM.Scripts.Input
{
    public class PlayerNecromancerInput : MonoBehaviour, IInteractInput, IMoveInput
    {
        private PlayerInputActions _inputActions;

        #region Commands
        public Command interactInput;
        public Command moveInput;
        #endregion

        #region Interface Properties
        public bool IsPressingInteract { get; private set; }
        public Vector2 MoveDirection { get; private set; }
        public bool IsMoving { get; private set; }
        public bool IsFacingRight { get; private set; }
        #endregion

        private GameObject _activeIndicator;

        #region Animation
        private PlayerNecromancerAnimations _playerNecromancerAnimations;
        #endregion

        private void Awake()
        {
            _inputActions = new PlayerInputActions();
            _playerNecromancerAnimations = GetComponent<PlayerNecromancerAnimations>();
            IsMoving = false;
            _activeIndicator = transform.GetChild(0).gameObject;
        }

        private void OnEnable()
        {
            var moveCommand = moveInput.GetComponent<MoveCommand>();
            moveCommand.InvokeEventCanMove(gameObject);
            MoveDirection = Vector2.zero;
            _activeIndicator.SetActive(true);
            _inputActions.Enable();
            _inputActions.Player.Interact.performed += OnInteractButton;
            _inputActions.Player.Movement.performed += OnMovementButton;
        }

        private void Start()
        {
            CharacterManager.AddCharacter(gameObject);
        }

        private void OnDisable()
        {
            var moveCommand = moveInput.GetComponent<MoveCommand>();
            moveCommand.InvokeEventCantMove(gameObject);
            _playerNecromancerAnimations.SetMovementParam(false);
            _activeIndicator.SetActive(false);
            _inputActions.Player.Movement.performed -= OnMovementButton;
            _inputActions.Player.Interact.performed -= OnInteractButton;
            _inputActions.Disable();
        }

        private void OnInteractButton(InputAction.CallbackContext context)
        {
            var value = context.ReadValue<float>();
            IsPressingInteract = value >= 0.15;
            if(interactInput != null && IsPressingInteract) interactInput.Execute();
        }
        
        private void OnMovementButton(InputAction.CallbackContext context)
        {
            var value = context.ReadValue<Vector2>();
            MoveDirection = value;
            if (MoveDirection.x != 0 || MoveDirection.y != 0)
            {
                if (!IsMoving)
                {
                    IsMoving = true;
                    _playerNecromancerAnimations.SetMovementParam(true);
                }
            }
            else
            {
                if(IsMoving) IsMoving = false;
                _playerNecromancerAnimations.SetMovementParam(false);
            }
            if(moveInput != null) moveInput.Execute();
        }
    }
}
