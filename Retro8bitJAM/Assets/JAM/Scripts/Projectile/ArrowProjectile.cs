using UnityEngine;

namespace JAM.Scripts.Projectile
{
    public class ArrowProjectile : Projectiles
    {
        [SerializeField] private GameObject _arrowPickup;
        [SerializeField] private float _force = 3;
        private bool hasAlreadySpawned = false;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer(_wallsLayer) || other.gameObject.layer == LayerMask.NameToLayer(_enemiesLayer))
            {
                // if(other.GetComponent<Enemy>() != null) other.GetComponent<Enemy>().ApplyDamage();
                // PlaySomeEffect
                SpawnArrowPickup(other);
                Destroy(gameObject);
            }
        }

        private void SpawnArrowPickup(Collider2D other)
        {
            if (hasAlreadySpawned) return;
            var obj = Instantiate(_arrowPickup, transform.position, Quaternion.Euler(0, 0, 90));
            Vector2 dir = other.transform.position - transform.position;
            dir = -dir.normalized;
            dir.y = Random.Range(-1, 1);
            var force = Random.Range(_force - 2, _force + 2);
            obj.GetComponent<Rigidbody2D>().AddForce(dir * force, ForceMode2D.Impulse);
            hasAlreadySpawned = true;
        }
    }
}