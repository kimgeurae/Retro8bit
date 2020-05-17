using System;
using JAM.Scripts.Animations;
using JAM.Scripts.Factory;
using JAM.Scripts.Input;
using JAM.Scripts.Interfaces;
using JAM.Scripts.Managers;
using JAM.Scripts.Player;
using JAM.Scripts.Structs;
using JAM.Scripts.Weapons;
using UnityEngine;

namespace JAM.Scripts.Pickups
{
    public class WeaponPickup : MonoBehaviour, IPickable, IInteractable, IDestroyable
    {
        [SerializeField]
        private WeaponStruct _weaponStruct;
        private PlayerMinionAnimations _playerMinionAnimations;
        private Minion _minion;
        private BaseWeapon _weapon;

        public void Pick()
        {
            _playerMinionAnimations = CharacterManager.ActiveCharacter().GetComponent<PlayerMinionAnimations>();
            _minion = CharacterManager.ActiveCharacter().GetComponent<Minion>();
            _minion.SetWeapon(_weaponStruct.MyWeaponType);
            _playerMinionAnimations.Pick(_weaponStruct.MyWeaponType);
            CustomDestroy();
        }

        public void Interact()
        {
            if(Application.isEditor) Debug.Log($"Interact in {gameObject} is being called");
            Pick();
        }

        public void CustomDestroy()
        {
            Destroy(gameObject);
        }
    }
}