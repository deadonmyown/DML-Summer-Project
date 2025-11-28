using Player;
using Environment.Interactable.Abstraction;
using UnityEngine;

namespace Environment.Interactable
{
    public class Door : RotatableObject, IInteractable
    {
        public bool Interact(Interactor interactor)
        {
            Change();
        
            return false;
        }
    }
}
