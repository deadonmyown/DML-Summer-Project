using System;
using UnityEngine;

namespace Environment.Interactable
{
    public class ChangeableHandler : MonoBehaviour
    {
        [SerializeField] private Changeable[] changeables;
        private int _changeableCount;

        private void Start()
        {
            if (changeables != null)
            {
                _changeableCount = changeables.Length;
            }
        }

        public bool TryChangeWith(Button interactable = null)
        {
            if (!CheckOnChanging())
            {
                for (int i = 0; i < _changeableCount; i++)
                {
                    changeables[i].ChangeWith(interactable);
                }

                return true;
            }

            return false;
        }

        public void ChangeWith(Button interactable = null)
        {
            for (int i = 0; i < _changeableCount; i++)
            {
                changeables[i].ChangeWith(interactable);
            }
        }

        public bool CheckOnChanging()
        {
            for (int i = 0; i < _changeableCount; i++)
            {
                if (changeables[i].IsChangingReadonly)
                    return true;
            }

            return false;
        }
    }
}