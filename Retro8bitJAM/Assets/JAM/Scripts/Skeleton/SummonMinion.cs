using JAM.Scripts.Interfaces;
using Unity.Mathematics;
using UnityEngine;

namespace JAM.Scripts.Skeleton
{
    public class SummonMinion : MonoBehaviour, IDestroyable, IInteractable, ISpawnable
    {
        [SerializeField]
        private GameObject _minion;
        
        public void CustomDestroy()
        {
            Destroy(gameObject);
        }

        public void Interact()
        {
            Spawn();
            CustomDestroy();
        }

        public void Spawn()
        {
            Instantiate(_minion, transform.position, quaternion.identity);
        }
    }
}