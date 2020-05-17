using JAM.Scripts.Commands;
using JAM.Scripts.Input;
using JAM.Scripts.Projectile;
using JAM.Scripts.Structs;
using UnityEngine;

namespace JAM.Scripts.Weapons
{
    public class SpearWeapon : BaseWeapon
    {
        private PlayerMinionInput _playerMinionInput;
        [SerializeField] private Transform leftArrowSpawnPoint;
        [SerializeField] private Transform rightArrowSpawnPoint;
        [SerializeField] private GameObject projectile;
        
        public override void Initialize()
        {
            base.Initialize();
            _canAttack = true;
            _canUseSkill = true;
            _playerMinionInput = transform.GetComponentInParent<PlayerMinionInput>();
            if(Application.isEditor) Debug.Log($"Initializing object {this}");
        }
        
        public override void Attack()
        {
            // Attack
            if (!_canAttack) return;
            _canAttack = false;
            _canUseSkill = false;
            MoveCommand.InvokeEventCantMove(gameObject);
            Invoke(nameof(AttackCooldown), _attackCooldown);
            _playerMinionAnimations.TriggerAttack();
            if(Application.isEditor) Debug.Log($"Attack called at {this}");
        }

        public override void Skill()
        {
            // Throw
            if (!_canUseSkill) return;
            _playerMinionAnimations.TriggerAlternateAttack();
            Invoke(nameof(DestroyWeapon), _attackCooldown - _attackCooldown/3);
            if(Application.isEditor) Debug.Log($"Skill called at {this}");
        }
        
        private void SpawnProjectile()
        {
            if (_playerMinionInput.IsFacingRight)
            {
                // true
                var obj = Instantiate(projectile, rightArrowSpawnPoint.position, Quaternion.Euler(0, 0, -45));
                obj.GetComponent<Projectiles>().Initialize(true);
            }
            else
            {
                // false
                var obj = Instantiate(projectile, leftArrowSpawnPoint.position, Quaternion.Euler(0, 0, 45));
                obj.GetComponent<Projectiles>().Initialize(false);
            }
        }

        private void DestroyWeapon()
        {
            SpawnProjectile();
            _playerMinionAnimations.DropWeapon();
            _playerMinionAnimations.TriggerWeaponChange();
            Destroy(gameObject);
        }
    }
}