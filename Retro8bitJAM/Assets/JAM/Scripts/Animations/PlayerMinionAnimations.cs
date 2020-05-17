using System;
using JAM.Scripts.Structs;
using UnityEngine;

namespace JAM.Scripts.Animations
{
    public class PlayerMinionAnimations : MonoBehaviour
    {
        private Animator _animator;
        private String _movementParam = "IsMoving";
        private String _spearParam = "Spear";
        private String _swordParam = "Sword";
        private String _bowParam = "Bow";
        private String _changedParam = "Changed";
        private String _attackParam = "Attack";
        private String _alternateAttackParam = "AlternateAttack";

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void SetMovementParam(bool value)
        {
            _animator.SetBool(_movementParam, value);
        }

        public void Pick(WeaponStruct.WeaponType weaponType)
        {
            DropWeapon();
            _animator.SetBool(weaponType.ToString(), true);
            TriggerWeaponChange();
        }

        public void DropWeapon()
        {
            _animator.SetBool(_spearParam, false);
            _animator.SetBool(_swordParam, false);
            _animator.SetBool(_bowParam, false);
        }

        public void TriggerWeaponChange()
        {
            _animator.SetTrigger(_changedParam);
        }

        public void TriggerAttack()
        {
            _animator.SetTrigger(_attackParam);
        }

        public void TriggerAlternateAttack()
        {
            _animator.SetTrigger(_alternateAttackParam);
        }

        public void TriggerSkill()
        {
            
        }
    }
}