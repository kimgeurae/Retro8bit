using System;
using JAM.Scripts.Input;
using JAM.Scripts.Weapons;
using UnityEngine;
using UnityEngine.Events;

namespace JAM.Scripts.Commands
{
    public class MoveCommand : Command
    {
        private Rigidbody2D _rb2d;
        private IMoveInput _moveInput;
        private SpriteRenderer _spriteRenderer;
        [SerializeField] private float _speed = 1;
        private Vector2 _velocity;
        private bool _canMove;
        public UnityAction eventCanMove;
        public UnityAction eventCantMove;
        
        private void Awake()
        {
            _rb2d = GetComponent<Rigidbody2D>();
            _moveInput = GetComponent<IMoveInput>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _canMove = true;
            eventCanMove += CanMove;
            eventCantMove += CantMove;
        }

        public override void Execute()
        {
            base.Execute();
            _velocity = _moveInput.MoveDirection;
        }

        private void FixedUpdate()
        {
            HandleMovement();
        }

        private void HandleMovement()
        {
            if(_canMove)
            {
                if (_velocity.x != 0)
                    _spriteRenderer.flipX =
                        _velocity.x > 0 ? _spriteRenderer.flipX = false : _spriteRenderer.flipX = true; // Sistema de flip de img, vou ter que repensar ele
                _rb2d.velocity = _velocity * _speed;
            }
            else
            {
                _rb2d.velocity = Vector2.zero;
            }
        }

        private void CantMove()
        {
            _canMove = false;
        }

        private void CanMove()
        {
            _canMove = true;
        }

        public void InvokeEventCantMove(GameObject caller)
        {
            if(Application.isEditor) Debug.Log($"Event called by {caller}");
            eventCantMove?.Invoke();
        }
        
        public void InvokeEventCanMove(GameObject caller)
        {
            if(Application.isEditor) Debug.Log($"Event called by {caller}");
            eventCanMove?.Invoke();
        }
    }
}