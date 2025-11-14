using Environment.Interactable.Abstraction;
using Player;
using UnityEngine;

namespace UI
{
    public class UIInteractable : MonoBehaviour, IInteractable
    {
        [SerializeField] private Vector3 offset;
        [SerializeField] private string text;
        [SerializeField] private int charPerSec = 4;
        [SerializeField] private bool useTextTyping;
        
        public bool Interact(Interactor interactor)
        {
            UIManager.Instance.TryInteract(text, useTextTyping, transform.position + offset, charPerSec);
            
            return true;
        }
    }
}