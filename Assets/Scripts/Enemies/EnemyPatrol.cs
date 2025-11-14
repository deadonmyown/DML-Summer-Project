using System.Collections;
using Enemies.Abstraction;
using UnityEngine;

namespace Enemies
{
    public class EnemyPatrol : EnemyBehaviour
    {
        public IEnumerator MoveCoroutine { get; private set; }
        
        [SerializeField] private Vector3[] patrolPoints;
        [SerializeField] private float speed;
        private int _patrolPointsCount;
        private int _currentPatrolIndex;

        private const float Buffer = 0.2f;

        private void Start()
        {
            if (patrolPoints != null)
            {
                _patrolPointsCount = patrolPoints.Length;
                _currentPatrolIndex = 0;
            }
            
            //StartBehaviour();
        }
        
        public override IEnumerator StartBehaviourCoroutine()
        {
            while (CheckActive())
            {
                MoveCoroutine = MoveToPatrolPoint(patrolPoints[_currentPatrolIndex]);
                yield return MoveCoroutine;
                ChangeIndex();
            }
        }

        private IEnumerator MoveToPatrolPoint(Vector3 endPosition)
        {
            while (Vector3.Distance(transform.position, endPosition) > Buffer)
            {
                transform.position = Vector3.MoveTowards(transform.position, endPosition, speed * Time.deltaTime);
                yield return null;
            }
        }

        private void ChangeIndex()
        {
            _currentPatrolIndex++;
            if (_currentPatrolIndex == _patrolPointsCount)
                _currentPatrolIndex = 0;
        }

        public override void StopCoroutines()
        {
            base.StopCoroutines();
            StopCoroutine(MoveCoroutine);
        }
    }
}