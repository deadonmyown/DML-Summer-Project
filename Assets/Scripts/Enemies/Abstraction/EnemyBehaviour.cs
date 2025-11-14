using System.Collections;
using UnityEngine;

namespace Enemies.Abstraction
{
    public abstract class EnemyBehaviour : MonoBehaviour
    {
        [SerializeField] private int orderIndex;
        public int OrderIndex => orderIndex;
        
        public IEnumerator BehaviourCoroutine { get; protected set; }
        
        public bool IsActive { get; protected set; }

        public virtual void StartBehaviour()
        {
            BehaviourCoroutine = StartBehaviourCoroutine();
            StartCoroutine(BehaviourCoroutine);
        }

        public abstract IEnumerator StartBehaviourCoroutine();

        public virtual void StopCoroutines()
        {
            StopCoroutine(BehaviourCoroutine);
        }

        public virtual bool CheckActive()
        {
            IsActive = true;
            return IsActive;
        }
    }
}