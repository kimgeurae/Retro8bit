using System;
using System.Security.Cryptography;
using JAM.Scripts.Animations;
using JAM.Scripts.Input;
using JAM.Scripts.Player;
using JAM.Scripts.Projectile;
using UnityEngine;

namespace JAM.Scripts.Weapons
{
    public class BowWeapon : BaseWeapon
    {
        [SerializeField] private float bowDashDistance = 4f;
        private bool _hasSkillTriggered;
        private Rigidbody2D _rb2d;
        private Transform _parentTransform;
        private PlayerMinionInput _playerMinionInput;
        [SerializeField] private Transform leftArrowSpawnPoint;
        [SerializeField] private Transform rightArrowSpawnPoint;
        [SerializeField] private GameObject projectile;
        [SerializeField] private float _arrowCount = 3;
        

        public override void Initialize()
        {
            base.Initialize();
            _rb2d = transform.GetComponentInParent<Rigidbody2D>();
            _parentTransform = transform.GetComponentInParent<Transform>();
            _playerMinionInput = transform.GetComponentInParent<PlayerMinionInput>();
            _canAttack = true;
            _canUseSkill = true;
            if(Application.isEditor) Debug.Log($"Initializing object {this}");
        }
        
        public override void Attack()
        {
            // If has arrows remaining Shoot Arrow
            // Else Hit with bow to push enemies away
            if (!_canAttack) return;
            _canAttack = false;
            _canUseSkill = false;
            MoveCommand.InvokeEventCantMove(gameObject);
            if (_arrowCount > 0)
            {
                _playerMinionAnimations.TriggerAttack();
                Invoke(nameof(AttackCooldown), _attackCooldown);
                Invoke(nameof(SpawnProjectile), _attackCooldown - _attackCooldown/10);
            }
            else
            {
                // PushEnemiesAway
                Invoke(nameof(AttackCooldown), _attackCooldown/2);
                _playerMinionAnimations.TriggerAlternateAttack();
            }

            if(Application.isEditor) Debug.Log($"Attack called at {this}");
        }

        private void SpawnProjectile()
        {
            if (_playerMinionInput.IsFacingRight)
            {
                // true
                var obj = Instantiate(projectile, rightArrowSpawnPoint.position, Quaternion.identity);
                obj.GetComponent<Projectiles>().Initialize(true);
            }
            else
            {
                // false
                var obj = Instantiate(projectile, leftArrowSpawnPoint.position, Quaternion.identity);
                obj.GetComponent<Projectiles>().Initialize(false);
            }
            RemoveArrow();
        }

        public bool AddArrow()
        {
            if (_arrowCount < 3)
            {
                _arrowCount++;
                return true;
            }
            else
            {
                return false;
            }
        }

        public void RemoveArrow()
        {
            if(_arrowCount > 0) _arrowCount--;
        }

        public override void Skill()
        {
            // Dash
            if(Application.isEditor) Debug.Log($"Skill called at {this}");
            if (!_canUseSkill) return;
            _hasSkillTriggered = true;
            _canUseSkill = false;
            Invoke(nameof(SkillCooldown), _skillCooldown);
        }

        private void SkillCooldown()
        {
            _canUseSkill = true;
        }

        private void FixedUpdate()
        {
            if (_hasSkillTriggered)
            {
                var moveDir = new Vector3(_playerMinionInput.MoveDirection.x, _playerMinionInput.MoveDirection.y, 0);
                if (moveDir != Vector3.zero)
                {
                    _rb2d.MovePosition(_parentTransform.position + moveDir * bowDashDistance);
                    _hasSkillTriggered = false;
                    if (Application.isEditor) Debug.Log($"Dash Triggered");
                }
            }
        }
    }
}