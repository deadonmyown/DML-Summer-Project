using UnityEngine;

namespace Environment.Interactable
{
    public abstract class Changeable : MonoBehaviour
    {
        protected bool IsChanging;
        
        public virtual bool IsChangingReadonly { get => IsChanging; }
        
        public abstract void Change();

        public abstract void ChangeWith(Button interactable = null);
    }
}