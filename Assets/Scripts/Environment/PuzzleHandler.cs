using Environment.Interactable;
using UnityEngine;
using UnityEngine.Events;

namespace Environment
{
    public class PuzzleHandler : MonoBehaviour
    {
        [SerializeField] private Changeable[] targetObjects;
        [SerializeField] private Vector3 targetRotation;
        [SerializeField] private Vector3 targetPosition;
        private int _targetsCount;
        private bool _puzzleSolved = false;

        public CustomData customData;
        
        public UnityEvent onPuzzleSuccess;
        public CustomEvent onCustomPuzzleSuccess;
        
        private void Start()
        {
            if (targetObjects != null)
            {
                _targetsCount = targetObjects.Length;
            }
        }

        public void TryPuzzleInvoke()
        {
            if (!_puzzleSolved && TargetObjectsSuccess())
            {
                Debug.Log("Success");
                onPuzzleSuccess.Invoke();
                onCustomPuzzleSuccess.Invoke(customData);
                _puzzleSolved = true;
            }
        }

        private bool TargetObjectsSuccess()
        {
            for (int i = 0; i < _targetsCount; i++)
            {
                if (!CheckSuccess((dynamic)targetObjects[i]))
                    return false;
            }

            return true;
        }

        private bool CheckSuccess(RotatableObject rotatable)
        {
            return rotatable.CurrentRotation == targetRotation;
        }
        
        private bool CheckSuccess(RotatableRigidbodyObject rotatable)
        {
            return rotatable.CurrentRotation == targetRotation;
        }

        private bool CheckSuccess(MovableObject movable)
        {
            return movable.CurrentPosition == targetPosition;
        }
    }
    
    [System.Serializable]
    public class CustomData
    {
        public Color newColor;
        public Color backgroundColor;
    }

    [System.Serializable]
    public class CustomEvent : UnityEvent<CustomData> {}
}