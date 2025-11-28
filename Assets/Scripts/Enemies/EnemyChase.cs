using System;
using System.Collections;
using Enemies.Abstraction;
using Player;
using UnityEngine;

namespace Enemies
{
    public class EnemyChase : EnemyBehaviour
    {
        [SerializeField] private float chaseRadius;
        [SerializeField] private float speed;
        
        private Player.Player _playerToChase;
        private int _playersCount;


        private void Start()
        {
            _playersCount = PlayerManager.Instance.Players.Count;
        }

        public override IEnumerator StartBehaviourCoroutine()
        {
            while (CheckActive())
            {
                transform.position = Vector3.MoveTowards(transform.position, _playerToChase.transform.position, speed * Time.deltaTime);
                yield return null;
            }
        }

        public override bool CheckActive()
        {
            if (_playerToChase is not null)
            {
                if (Vector3.Distance(_playerToChase.transform.position, transform.position) > chaseRadius)
                {
                    _playerToChase = null;
                }
                else
                {
                    IsActive = true;
                    return IsActive;
                }
            }
            
            for (int i = 0; i < _playersCount; i++)
            {
                if (Vector3.Distance(PlayerManager.Instance.Players[i].transform.position, transform.position) <=
                    chaseRadius)
                {
                    _playerToChase = PlayerManager.Instance.Players[i];

                    IsActive = true;
                    return IsActive;
                }
            }

            IsActive = false;
            _playerToChase = null;
            return IsActive;
        }
    }
}