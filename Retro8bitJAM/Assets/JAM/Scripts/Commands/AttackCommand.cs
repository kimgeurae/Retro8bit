using System;
using JAM.Scripts.Player;
using JAM.Scripts.Weapons;
using UnityEngine;

namespace JAM.Scripts.Commands
{
    public class AttackCommand : Command
    {
        private Minion _minion;

        private void Awake()
        {
            _minion = GetComponent<Minion>();
        }

        public override void Execute()
        {
            base.Execute();
            _minion.CurrentWeapon.Attack();
        }
    }
}