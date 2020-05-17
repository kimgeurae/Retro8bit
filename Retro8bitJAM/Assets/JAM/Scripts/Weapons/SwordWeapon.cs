using UnityEngine;

namespace JAM.Scripts.Weapons
{
    public class SwordWeapon : BaseWeapon
    {
        public bool isBlockActive;
        
        public override void Initialize()
        {
            base.Initialize();
            _canAttack = true;
            _canUseSkill = true;
            isBlockActive = false;
            if(Application.isEditor) Debug.Log($"Initializing object {this}");
        }
        
        public override void Attack()
        {
            // Attack
            _canAttack = false;
            _canUseSkill = false;
            MoveCommand.InvokeEventCantMove(gameObject);
            Invoke(nameof(AttackCooldown), _attackCooldown);
            _playerMinionAnimations.TriggerAttack();
            if(Application.isEditor) Debug.Log($"Attack called at {this}");
        }

        public override void Skill()
        {
            // Defense
            _canAttack = false;
            _canUseSkill = false;
            isBlockActive = true;
            MoveCommand.InvokeEventCantMove(gameObject);
            Invoke(nameof(AttackCooldown), _skillCooldown);
            _playerMinionAnimations.TriggerAlternateAttack();
            if(Application.isEditor) Debug.Log($"Skill called at {this}");
        }

        protected override void AttackCooldown()
        {
            base.AttackCooldown();
            isBlockActive = false;
        }
    }
}