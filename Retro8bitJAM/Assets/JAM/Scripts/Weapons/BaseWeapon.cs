using System;
using System.Security.Cryptography;
using JAM.Scripts.Animations;
using JAM.Scripts.Commands;
using JAM.Scripts.Factory;
using JAM.Scripts.Input;
using JAM.Scripts.Interfaces;
using JAM.Scripts.Structs;
using UnityEngine;

namespace JAM.Scripts.Weapons
{
    public abstract class BaseWeapon : MonoBehaviour, IDestroyable
    {
        [SerializeField]
        private int _dmg = 0;
        public WeaponStruct myWeaponStruct;
        public abstract void Attack();
        public abstract void Skill();
        protected bool _canAttack;
        protected bool _canUseSkill;
        [SerializeField] protected float _attackCooldown;
        [SerializeField] protected float _skillCooldown = 3f;
        protected MoveCommand MoveCommand;
        protected PlayerMinionAnimations _playerMinionAnimations;

        public virtual void Initialize()
        {
            MoveCommand = GetComponentInParent<MoveCommand>();
            _playerMinionAnimations = GetComponentInParent<PlayerMinionAnimations>();
        }

        protected virtual void AttackCooldown()
        {
            MoveCommand.InvokeEventCanMove(gameObject);
            _canAttack = true;
            _canUseSkill = true;
        }

        public void CustomDestroy()
        {
            GameObject previousWeapon = WeaponPickupFactory.GetWeapon(myWeaponStruct.MyWeaponType).myWeaponPickup;
            Instantiate(previousWeapon, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}