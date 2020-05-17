using JAM.Scripts.Interfaces;
using JAM.Scripts.Managers;
using JAM.Scripts.Weapons;
using UnityEngine;

namespace JAM.Scripts.Pickups
{
    public class ArrowPickup : MonoBehaviour, IPickable, IInteractable, IDestroyable
    {
        private BowWeapon _bowWeapon;

        public void Pick()
        {
            if (CharacterManager.ActiveCharacter().GetComponentInChildren<BowWeapon>() == null) return;
            _bowWeapon = CharacterManager.ActiveCharacter().GetComponentInChildren<BowWeapon>();
            if (!_bowWeapon.AddArrow()) return;
            CustomDestroy();
        }

        public void Interact()
        {
            Pick();
        }

        public void CustomDestroy()
        {
            Destroy(gameObject);
        }
    }
}