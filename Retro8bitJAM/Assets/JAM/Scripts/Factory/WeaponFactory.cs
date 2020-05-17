using JAM.Scripts.ScriptableObjects;
using System;
using System.Linq;
using System.Reflection;
using JAM.Scripts.Structs;
using JAM.Scripts.Weapons;
using UnityEngine;

namespace JAM.Scripts.Factory
{
    public static class WeaponFactory
    {
        private static WeaponSO[] _weapons;

        static WeaponFactory()
        {
            _weapons = Resources.FindObjectsOfTypeAll<WeaponSO>();
        }

        public static GameObject GetWeapon(WeaponStruct.WeaponType weaponType)
        {
            return _weapons.FirstOrDefault(item => item.myWeaponStruck.MyWeaponType == weaponType).myWeapon;
        }
    }
}

// todo : Factory should be inject using Zenject.