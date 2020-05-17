using System;
using UnityEngine;
using UnityEngine.U2D;

namespace JAM.Scripts.Projectile
{
    public abstract class Projectiles : MonoBehaviour
    {
        protected Rigidbody2D _rb2d;
        protected Vector2 _direction;
        [SerializeField] protected float _speed = 1;
        protected SpriteRenderer _spriteRenderer;
        protected bool _hasInitialized;
        [SerializeField] protected String _enemiesLayer;
        [SerializeField] protected String _wallsLayer;
        [SerializeField] protected int _damage;

        protected virtual void Awake()
        {
            _rb2d = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        protected virtual void Start()
        {
            Invoke(nameof(DestroyMe), 10f);
        }

        protected virtual void OnDestroy()
        {
            CancelInvoke(nameof(DestroyMe));
        }

        protected virtual void DestroyMe()
        {
            Destroy(gameObject);
        }

        public virtual void Initialize(bool right)
        {
            _direction = right ? _direction = Vector2.right : Vector2.left;
            _spriteRenderer.flipX = right ? _spriteRenderer.flipX = false : _spriteRenderer.flipX = true;
            _hasInitialized = true;
        }

        protected virtual void FixedUpdate()
        {
            if (!_hasInitialized) return;
            _rb2d.velocity = _direction * _speed;
        }
    }
}