using JAM.Scripts.ScriptableObjects;
using System;
using System.Linq;
using System.Reflection;
using JAM.Scripts.Structs;
using JAM.Scripts.Weapons;
using UnityEngine;

namespace JAM.Scripts.Factory
{
    public static class WeaponPickupFactory
    {
        private static WeaponSO[] _weapons;

        static WeaponPickupFactory()
        {
            _weapons = Resources.FindObjectsOfTypeAll<WeaponSO>();
        }

        public static WeaponSO GetWeapon(WeaponStruct.WeaponType weaponType)
        {
            return _weapons.FirstOrDefault(item => item.myWeaponStruck.MyWeaponType == weaponType);
        }
    }
}

// todo : Factory should be inject using Zenject.