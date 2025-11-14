using Environment.Interactable.Abstraction;
using Player;
using UnityEngine;

namespace Environment.Interactable
{
    public class Key : MonoBehaviour, IInteractable
    {
        [SerializeField] private ILocked[] _lockeds;
        public bool Interact(Interactor interactor)
        {
            if (_lockeds != null)
            {
                var len = _lockeds.Length;
                for (int i = 0; i < len; i++)
                {
                    _lockeds[i].Key = this;
                }

                return true;
            }

            return false;
        }
    }
}
