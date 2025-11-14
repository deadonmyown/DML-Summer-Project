using System.Collections;
using Environment;
using Environment.Interactable.Abstraction;
using Player;
using UnityEngine;

namespace Environment
{
    public class MovableObject : Changeable
    {
        [SerializeField] private bool moveAtStart;

        private Vector3 _currentPosition;

        public Vector3 CurrentPosition => _currentPosition;

        private void Start()
        {
            _currentPosition = transform.localPosition;

            if (moveAtStart)
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
                var startPosition = transform.localPosition;
                var addPosition = data.targetPosition;

                var endPosition = startPosition + addPosition;
                _currentPosition = endPosition;
                yield return Move(startPosition, endPosition, data);

                yield return new WaitForSeconds(data.pauseDuration);

            } while (data.isLoop);
        }

        private IEnumerator Move(Vector3 startPosition, Vector3 endPosition, ChangeableData data)
        {
            IsChanging = true;
            float timeElapsed = 0;

            while (timeElapsed < data.duration)
            {
                float t = timeElapsed / data.duration;

                t = data.animCurve.Evaluate(t);

                transform.localPosition = Vector3.Lerp(startPosition, endPosition, t);
                timeElapsed += Time.fixedDeltaTime;

                yield return new WaitForFixedUpdate();
            }

            transform.localPosition = endPosition;
            if (data.changeBack)
            {
                data.targetPosition = -data.targetPosition;
            }

            IsChanging = data.isLoop;
        }

        public void TurnLoop(bool loop) => changeableData.isLoop = loop;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("PlayerMesh"))
            {
                other.gameObject.transform.parent.parent = transform;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("PlayerMesh"))
            {
                other.gameObject.transform.parent.parent = PlayerManager.Instance.transform;
            }
        }
    }
}
