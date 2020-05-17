using JAM.Scripts.Animations;
using JAM.Scripts.Commands;
using JAM.Scripts.Input;
using JAM.Scripts.Managers;
using JAM.Scripts.Weapons;
using UnityEngine;
using UnityEngine.InputSystem;

namespace JAM.Scripts.Input
{
    public class PlayerMinionInput : MonoBehaviour, IMoveInput, IInteractInput, ISkillInput, IAttackInput
    {
        private PlayerInputActions _inputActions;

        #region Commands

        public Command interactInput;
        public Command moveInput;
        public Command attackInput;
        public Command skillInput;

        #endregion

        #region Interface Properties

        public bool IsPressingInteract { get; private set; }
        public Vector2 MoveDirection { get; private set; }
        public bool IsMoving { get; private set; }
        public bool IsFacingRight { get; private set; }
        public bool IsPressingSkill { get; private set; }
        public bool IsPressingAttack { get; private set; }
        #endregion

        private GameObject _activeIndicator;

        #region Animation

        private PlayerMinionAnimations _playerMinionAnimations;

        #endregion

        private void Awake()
        {
            _inputActions = new PlayerInputActions();
            _playerMinionAnimations = GetComponent<PlayerMinionAnimations>();
            IsMoving = false;
            IsFacingRight = true;
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
            _inputActions.Player.Attack.performed += OnAttackButton;
            _inputActions.Player.Skill.performed += OnSkillButton;
        }
        
        private void Start()
        {
            CharacterManager.AddCharacter(gameObject);
        }

        private void OnDisable()
        {
            var moveCommand = moveInput.GetComponent<MoveCommand>();
            moveCommand.InvokeEventCantMove(gameObject);
            _playerMinionAnimations.SetMovementParam(false);
            _activeIndicator.SetActive(false);
            _inputActions.Player.Skill.performed -= OnSkillButton;
            _inputActions.Player.Attack.performed -= OnAttackButton;
            _inputActions.Player.Movement.performed -= OnMovementButton;
            _inputActions.Player.Interact.performed -= OnInteractButton;
            _inputActions.Disable();
        }

        private void OnInteractButton(InputAction.CallbackContext context)
        {
            var value = context.ReadValue<float>();
            IsPressingInteract = value >= 0.15;
            if (interactInput != null && IsPressingInteract) interactInput.Execute();
        }

        private void OnMovementButton(InputAction.CallbackContext context)
        {
            var value = context.ReadValue<Vector2>();
            MoveDirection = value;
            if (MoveDirection.x > 0)
            {
                IsFacingRight = true;
            }
            else if (MoveDirection.x < 0)
            {
                IsFacingRight = false;
            }

            if (MoveDirection.x != 0 || MoveDirection.y != 0)
            {
                if (!IsMoving)
                {
                    IsMoving = true;
                    _playerMinionAnimations.SetMovementParam(true);
                }
            }
            else
            {
                if (IsMoving) IsMoving = false;
                _playerMinionAnimations.SetMovementParam(false);
            }

            if (moveInput != null) moveInput.Execute();
        }

        private void OnSkillButton(InputAction.CallbackContext context)
        {
            var value = context.ReadValue<float>();
            IsPressingSkill = value >= 0.15;
            if (skillInput != null && IsPressingSkill)
            {
                skillInput.Execute();
            }
        }

        private void OnAttackButton(InputAction.CallbackContext context)
        {
            var value = context.ReadValue<float>();
            IsPressingAttack = value >= 0.15;
            if (attackInput != null && IsPressingAttack )
            {
                attackInput.Execute();
                // _playerMinionAnimations.TriggerAttack();
            }
        }
    }
}