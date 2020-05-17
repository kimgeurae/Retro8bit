using System;
using UnityEngine;

namespace JAM.Scripts.Animations
{
    public class PlayerNecromancerAnimations : MonoBehaviour
    {
        private Animator _animator;
        private String _movementParam = "IsMoving";

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void SetMovementParam(bool value)
        {
            _animator.SetBool(_movementParam, value);
        }
    }
}
