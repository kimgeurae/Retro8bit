using JAM.Scripts.Factory;
using JAM.Scripts.Structs;
using UnityEngine;

namespace JAM.Scripts.Projectile
{
    public class SpearProjectile : Projectiles
    {
        [SerializeField] private float _force = 3;
        private bool hasAlreadySpawned = false;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer(_wallsLayer) || other.gameObject.layer == LayerMask.NameToLayer(_enemiesLayer))
            {
                // if(other.GetComponent<Enemy>() != null) other.GetComponent<Enemy>().ApplyDamage(_damage);
                // PlaySomeEffect
                SpawnSpearPickup(other);
                Destroy(gameObject);
            }
        }

        private void SpawnSpearPickup(Collider2D other)
        {
            if (hasAlreadySpawned) return;
            var obj = Instantiate(WeaponPickupFactory.GetWeapon(WeaponStruct.WeaponType.Spear).myWeaponPickup, transform.position, Quaternion.Euler(0, 0, 0));
            Vector2 dir = other.transform.position - transform.position;
            dir = -dir.normalized;
            dir.y = Random.Range(-1, 1);
            var force = Random.Range(_force - 2, _force + 2);
            obj.GetComponent<Rigidbody2D>().AddForce(dir * force, ForceMode2D.Impulse);
            hasAlreadySpawned = true;
        }
    }
}