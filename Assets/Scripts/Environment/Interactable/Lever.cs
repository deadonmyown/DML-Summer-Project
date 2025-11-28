using Player;
using Environment.Interactable.Abstraction;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Environment.Interactable
{
    public class Lever : CustomDataHandler, IInteractable
    {
        [SerializeReference] private ChangeableHandler[] changeableHandlers;
        [SerializeField] private PuzzleHandler puzzleHandler;
        private int _changeableHandlersCount;
        
        private void Start()
        {
            if (changeableHandlers != null)
            {
                _changeableHandlersCount = changeableHandlers.Length;
            }
        }

        public bool Interact(Interactor interactor)
        {
            if (CheckChangeableHandlers())
            {
                for (int i = 0; i < _changeableHandlersCount; i++)
                {
                    changeableHandlers[i].ChangeWith(useCustomData ? changeableData : null);
                }

                if (puzzleHandler)
                {
                    puzzleHandler.TryPuzzleInvoke();
                }

                return true;
            }

            return false;
        }

        private bool CheckChangeableHandlers()
        {
            for (int i = 0; i < _changeableHandlersCount; i++)
            {
                if (changeableHandlers[i].CheckOnChanging())
                    return false;
            }

            return true;
        }
    }


#if UNITY_EDITOR
    [CustomEditor(typeof(Lever))]
    [CanEditMultipleObjects]
    public class LeverEditor : CustomDataHandlerEditor
    {
    }
#endif
}