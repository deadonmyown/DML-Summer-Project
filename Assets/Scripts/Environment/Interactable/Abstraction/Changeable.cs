using UnityEngine;

namespace Environment.Interactable.Abstraction
{
    public abstract class Changeable : MonoBehaviour
    {
        public ChangeableData changeableData;
        
        protected bool IsChanging;
        
        public virtual bool IsChangingReadonly { get => IsChanging; }
        
        public abstract void Change();

        public abstract void ChangeWith(ChangeableData data = null);
    }
}