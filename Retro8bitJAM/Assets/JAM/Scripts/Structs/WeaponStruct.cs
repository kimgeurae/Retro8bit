using System;
using UnityEngine;

namespace JAM.Scripts.Structs
{
    [Serializable] 
    public struct WeaponStruct
    {
        public enum WeaponType
        {
            Spear = 0,
            Bow = 1,
            Sword = 2
        }
        public WeaponType MyWeaponType;
    }
}