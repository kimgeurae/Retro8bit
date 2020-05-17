using System;
using JAM.Scripts.Player;
using UnityEngine;

namespace JAM.Scripts.Enemies
{
    public class Saw : MonoBehaviour
    {
        [SerializeField] private int _dmg;
        [SerializeField] private String _playerLayer = "Player";
        private void OnTriggerEnter2D(Collider2D other)
        {
            if(Application.isEditor) Debug.Log($"Saw on trigger enter 2D Called");
            if (other.gameObject.layer == LayerMask.NameToLayer(_playerLayer))
            {
                if (other.gameObject.GetComponent<Minion>() != null)
                {
                    other.gameObject.GetComponent<Minion>().Damage(_dmg);
                }
                if (other.gameObject.GetComponent<Necromancer>() != null)
                {
                    other.gameObject.GetComponent<Necromancer>().Kill();
                }
            }
        }
    }
}