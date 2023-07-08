using UnityEngine;

public class Interactor : MonoBehaviour
{
    public const int ColliderLength = 5;

    public int InteractedCount { get; private set; }
    public Collider[] interactedColliders = new Collider[ColliderLength];

    [SerializeField] private Transform interactorCheck;
    [SerializeField] private float interactorCheckRadius = 0.8f;
    [SerializeField] private LayerMask interactorMask;



    public void TryInteract()
    {
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
        return Physics.OverlapSphereNonAlloc(interactorCheck.position, interactorCheckRadius, interactedColliders, interactorMask);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(interactorCheck.position, interactorCheckRadius);
    }
}
