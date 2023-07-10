using System;
using Player;
using UnityEngine;
using UnityEngine.Events;

namespace Environment
{
    public class OnTriggerEvent : MonoBehaviour
    {
        public CustomData customData;
        public UnityEvent onTriggerEvent;
        public CustomEvent onCustomTriggerEvent;

        private int _playersCount;
        private int _currPlayersCount;

        private void Start()
        {
            _playersCount = PlayerManager.Instance.Players.Count;
            _currPlayersCount = 0;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("PlayerMesh") && ++_currPlayersCount == _playersCount)
            {
                onTriggerEvent.Invoke();
                onCustomTriggerEvent.Invoke(customData);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("PlayerMesh"))
            {
                _currPlayersCount--;
            }
        }
    }
}