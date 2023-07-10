using System;
using UnityEngine;
using System.Collections;
using UnityEngine.Serialization;

namespace Environment
{
    public class CameraHandler : MonoBehaviour
    {
        [SerializeField] private float colorDuration;
        private Camera _objectCamera;

        private void Start()
        {
            _objectCamera = GetComponent<Camera>();
        }

        public void SetColor(CustomData customData)
        {
            StartCoroutine(ChangeColor(_objectCamera.backgroundColor, customData.backgroundColor, colorDuration));
        }

        private IEnumerator ChangeColor(Color start, Color end, float duration)
        {
            float timeElapsed = 0;

            while (timeElapsed < duration)
            {
                _objectCamera.backgroundColor = Color.Lerp(start, end, timeElapsed / duration);
                timeElapsed += Time.deltaTime;

                yield return null;
            }

            _objectCamera.backgroundColor = end;
        }
    }
}