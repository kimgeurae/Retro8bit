using System;
using System.Collections.Generic;
using System.Linq;
using JAM.Scripts.Factory;
using JAM.Scripts.Interfaces;
using JAM.Scripts.ScriptableObjects;
using JAM.Scripts.Structs;
using UnityEngine;

namespace JAM.Scripts.Chest
{
    public class LootChest : MonoBehaviour, IOpenable, IInteractable, ISpawnable
    {
        #region Loot
        [Header("Loot")]
        // new system
        [SerializeField]
        private WeaponStruct _mWeaponStruct;
        [SerializeField]
        private WeaponSO[] _weapons;
        private WeaponSO _loot;
        // new system end
        [SerializeField] private Vector3 spawnOffset = new Vector3(0, -0.2f, 0);
        private bool _isOpened = false;
        #endregion

        #region Visual
        [Header("Chest Visuals")]
        [SerializeField] private Sprite[] _spriteChestTypeOne;
        [SerializeField] private Sprite[] _spriteChestTypeTwo;
        private SpriteRenderer _spriteRenderer;
        private enum ChestType
        {
            One = 0,
            Two = 1
        };
        [SerializeField] private ChestType _chestType;
        private readonly int _chestClosed = 0;
        private readonly int _chestOpened = 1;
        #endregion

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            SetupChest();
        }

        public void Open()
        {
            // Visuals
            _spriteRenderer.sprite =
                _chestType == ChestType.One
                    ? _spriteRenderer.sprite = _spriteChestTypeOne[_chestOpened]
                    : _spriteRenderer.sprite = _spriteChestTypeTwo[_chestOpened];
            // Loot
            Spawn();
            _isOpened = true;
            GetComponent<BoxCollider2D>().enabled = false;
        }

        public void Interact()
        {
            if (_isOpened) return;
            Open();
        }

        private void SetupChest()
        {
            // Visual
            _spriteRenderer.sprite =
                _chestType == ChestType.One
                    ? _spriteRenderer.sprite = _spriteChestTypeOne[_chestClosed]
                    : _spriteRenderer.sprite = _spriteChestTypeTwo[_chestClosed];
            // Loot
            _loot = WeaponPickupFactory.GetWeapon(_mWeaponStruct.MyWeaponType);
        }

        public void Spawn()
        {
            Instantiate(_loot.myWeaponPickup, transform.position - spawnOffset, Quaternion.identity);
        }
    }
}