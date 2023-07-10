using System;
using UnityEngine;
using System.Collections;
using UnityEngine.Serialization;

namespace Environment
{
    public class LightHandler : MonoBehaviour
    {
        [SerializeField] private float colorDuration;
        private Light _objectLight;

        private void Start()
        {
            _objectLight = GetComponent<Light>();
        }

        public void SetColor(CustomData customData)
        {
            StartCoroutine(ChangeColor(_objectLight.color, customData.newColor, colorDuration));
        }

        private IEnumerator ChangeColor(Color start, Color end, float duration)
        {
            float timeElapsed = 0;

            while (timeElapsed < duration)
            {
                _objectLight.color = Color.Lerp(start, end, timeElapsed / duration);
                timeElapsed += Time.deltaTime;

                yield return null;
            }

            _objectLight.color = end;
        }
    }
}