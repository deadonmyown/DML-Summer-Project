using System;
using UnityEngine;

namespace Environment
{
    //If add fields add them in button custom editor!!!
    [Serializable]
    public class ChangeableData
    {
        public bool changeBack;
        public bool isLoop;
        public Vector3 targetPosition;
        public Vector3 targetRotation;
        public AnimationCurve animCurve;
        public float duration;
        public float pauseDuration;
    }
}