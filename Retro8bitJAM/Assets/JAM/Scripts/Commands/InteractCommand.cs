using System;
using JAM.Scripts.Interfaces;
using UnityEditor;
using UnityEngine;

namespace JAM.Scripts.Commands
{
    public class InteractCommand : Command
    {
        private Transform _transform;
        private IInteractable _interactedWith;
        [Header("Raycast Properties")]
        [SerializeField] private Vector2 size;
        [SerializeField] private float angle;
        [SerializeField] private float distance;
        [SerializeField] private string layerName = "Interactable";

        private void Awake()
        {
            _transform = transform;
        }

        public override void Execute()
        {
            base.Execute();
            var pos = (Vector2) _transform.position;
            RaycastHit2D hit = Physics2D.BoxCast(pos, size, angle, Vector2.up, distance, LayerMask.GetMask(layerName));
            
            if(Application.isEditor) Debug.Log($"{gameObject.name} is trying to interact");
            if (hit.collider == null) return;
            if(Application.isEditor) Debug.Log($"{gameObject.name}, is interacting with {hit.collider.name}");
            
            _interactedWith = hit.collider.GetComponent<IInteractable>();
            _interactedWith?.Interact();
        }
    }
}