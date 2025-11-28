using Player;
using Environment.Interactable.Abstraction;
using UnityEngine;
using UnityEngine.Timeline;

namespace Environment.Interactable
{
    public class LockedDoor : RotatableObject, IInteractable, ILocked
    {
        [SerializeField] private Key requiredKey;

        public Key RequiredKey
        {
            get => requiredKey;
            set => requiredKey = value;
        }

        public Key Key { get; set; }
        
        public bool Interact(Interactor interactor)
        {
            if (requiredKey == Key)
            {
                Change();
            }

            return false;
        }
    }
}
