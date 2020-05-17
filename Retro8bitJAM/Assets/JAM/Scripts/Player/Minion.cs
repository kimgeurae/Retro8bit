using System.Collections;
using JAM.Scripts.Factory;
using JAM.Scripts.Input;
using JAM.Scripts.Interfaces;
using JAM.Scripts.Managers;
using JAM.Scripts.Structs;
using JAM.Scripts.Weapons;
using Unity.Mathematics;
using UnityEngine;

namespace JAM.Scripts.Player
{
    public class Minion : MonoBehaviour, ISelectedWeapon, IDamageable, IKilleable, ILife, IHealable
    {
        public BaseWeapon CurrentWeapon { get; private set; }
        public int _life { get; private set; }
        [SerializeField] private int _initialLife;
        [SerializeField] private int _maxLife;
        [SerializeField] private float _damageCooldown = 1f;
        private float _dmgTimer;
        [SerializeField] private GameObject _skeletonSummon;
        private SpriteRenderer _spriteRenderer;

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            _life = _initialLife;
            _dmgTimer = Time.time;
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void SetWeapon(WeaponStruct.WeaponType value)
        {
            if(CurrentWeapon != null) CurrentWeapon.CustomDestroy();
            var obj = Instantiate(WeaponFactory.GetWeapon(value), transform.position, quaternion.identity);
            obj.transform.parent = gameObject.transform;
            obj.GetComponent<BaseWeapon>().Initialize();
            CurrentWeapon = obj.GetComponent<BaseWeapon>();
        }

        public void Damage(int amount)
        {
            if (_dmgTimer > Time.time) return;
            _life = _life - amount > 0 ? _life -= amount : _life = 0;
            _dmgTimer = Time.time + _damageCooldown;
            StopCoroutine(nameof(DamageEffect));
            if(_life > 0) StartCoroutine(nameof(DamageEffect));
            if (_life > 0) return;
            StopCoroutine(nameof(DamageEffect));
            Kill();
        }

        private IEnumerator DamageEffect()
        {
            if(Application.isEditor) Debug.Log($"Damage Effect started", gameObject);
            _spriteRenderer.color = Color.red;
            for (int i = 0; i < 2; i++)
            {
                var spriteRendererColor = _spriteRenderer.color;
                spriteRendererColor.a = 0;
                _spriteRenderer.color = spriteRendererColor;
                yield return new WaitForSeconds(0.25f);
                spriteRendererColor.a = 1;
                _spriteRenderer.color = spriteRendererColor;
                yield return new WaitForSeconds(0.25f);
            }

            _spriteRenderer.color = Color.white;
        }

        public void Kill()
        {
            CharacterManager.RemoveCharacter(gameObject);
            Instantiate(_skeletonSummon, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        
        public void Heal(int amount)
        {
            _life = _life + amount < _maxLife ? _life += amount : _life = _maxLife;
        }
    }
}