using System;
using Player;
using UnityEngine;

public class LevelTrigger : MonoBehaviour
{
    [SerializeField] private LevelLoader loadLevel;
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
            loadLevel.LoadNextLevel();
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
