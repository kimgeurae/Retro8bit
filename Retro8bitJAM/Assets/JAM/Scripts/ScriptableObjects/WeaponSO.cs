using JAM.Scripts.Structs;
using UnityEngine;

namespace JAM.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "ScriptableObjects/Weapon/NewWeapon", order = 1)]
    public class WeaponSO : ScriptableObject
    {
        public WeaponStruct myWeaponStruck;
        public GameObject myWeaponPickup;
        public GameObject myWeapon;
    }
}