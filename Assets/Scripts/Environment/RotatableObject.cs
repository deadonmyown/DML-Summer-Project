using System.Collections;
using Environment.Interactable.Abstraction;
using UnityEngine;

namespace Environment
{
    public class RotatableObject : Changeable
    {
        [SerializeField] private bool rotateAtStart;
        [SerializeField] private Vector3 currentRotation;

        public Vector3 CurrentRotation => currentRotation;

        private void Start()
        {
            transform.eulerAngles = currentRotation;
            if (rotateAtStart)
                ChangeWith(changeableData);
        }

        public override void Change()
        {
            StartCoroutine(BaseSetup(changeableData));
        }

        public override void ChangeWith(ChangeableData data = null)
        {
            StartCoroutine(BaseSetup(data ?? changeableData));
        }

        private IEnumerator BaseSetup(ChangeableData data)
        {
            do
            {
                var startRotation = transform.rotation;
                var endVector = data.targetRotation;

                currentRotation += endVector;
                currentRotation.Set(currentRotation.x % 360, currentRotation.y % 360, currentRotation.z % 360);
                var endRotation = Quaternion.Euler(currentRotation);
                yield return Rotate(startRotation, endRotation, data);

                yield return new WaitForSeconds(data.pauseDuration);

            } while (data.isLoop);
        }

        private IEnumerator Rotate(Quaternion startRotation, Quaternion endRotation, ChangeableData data)
        {
            IsChanging = true;
            float timeElapsed = 0;

            while (timeElapsed < data.duration)
            {
                float t = timeElapsed / data.duration;

                t = data.animCurve.Evaluate(t);

                transform.rotation = Quaternion.Lerp(startRotation, endRotation, t);
                timeElapsed += Time.deltaTime;

                yield return null;
            }

            transform.rotation = endRotation;
            if (data.changeBack)
            {
                data.targetRotation = -data.targetRotation;
            }

            IsChanging = data.isLoop;
        }

        public void TurnLoop(bool loop) => changeableData.isLoop = loop;
    }
}
