using System;
using JAM.Scripts.Commands;
using JAM.Scripts.Managers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace JAM.Scripts.Input
{
    public class CharacterManagerInput : MonoBehaviour, IIsPressingSwitchCharacter
    {
        private PlayerInputActions _inputActions;
        public bool IsPressingSwitchCharacter { get; private set; }
        [SerializeField] private Command switchCharacterInput;

        private void Awake()
        {
            _inputActions = new PlayerInputActions();
        }

        private void OnEnable()
        {
            _inputActions.Enable();
            _inputActions.Player.SwitchCharacter.performed += OnInteractButton;
        }

        private void OnDisable()
        {
            _inputActions.Player.SwitchCharacter.performed -= OnInteractButton;
            _inputActions.Disable();
        }
        
        private void OnInteractButton(InputAction.CallbackContext context)
        {
            var value = context.ReadValue<float>();
            IsPressingSwitchCharacter = value >= 0.15;
            if (switchCharacterInput != null && IsPressingSwitchCharacter) switchCharacterInput.Execute();
        }
    }
}