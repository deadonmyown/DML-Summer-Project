using System;
using Environment.Interactable.Abstraction;
using UnityEngine;

namespace Player
{
    public class Interactor : MonoBehaviour
    {
        public const int ColliderLength = 5;

        public int InteractedCount { get; private set; }
        public Collider[] interactedColliders = new Collider[ColliderLength];

        [SerializeField] private Transform interactorCheck;
        [SerializeField] private float interactorCheckRadius = 0.8f;
        [SerializeField] private LayerMask interactorMask;

        private static IInteractable _cachedInteractable = null;

        public void TurnInteractor(bool turn)
        {
            if (turn)
            {
                PlayerInputHandler.OnInteraction += TryInteract;
            }
            else
            {
                PlayerInputHandler.OnInteraction -= TryInteract;
            }
        }

        public void UpdateCachedInteractable(IInteractable interactable)
        {
            _cachedInteractable = interactable;
        }

        public void TryInteract()
        {
            if (_cachedInteractable is not null)
            {
                _cachedInteractable.Interact(this);
                return;
            }

            InteractedCount = CheckInteraction();

            if (InteractedCount > 0)
            {
                if (interactedColliders[0].TryGetComponent(out IInteractable interactable))
                {
                    interactable.Interact(this);
                }
            }
        }

        private int CheckInteraction()
        {
            return Physics.OverlapSphereNonAlloc(interactorCheck.position, interactorCheckRadius, interactedColliders,
                interactorMask);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(interactorCheck.position, interactorCheckRadius);
        }

        private void OnDestroy()
        {
            PlayerInputHandler.OnInteraction -= TryInteract;
        }
    }
}
