using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager Instance { get; private set; }
        public IList<PlayerInput> Players { get; private set; }
        public PlayerInput CurrentPlayer { get; private set; }
        public int PlayerIndex { get; private set; }

        [SerializeField] private CinemachineVirtualCameraBase activeCamera;

        private void Awake()
        {
            if (Instance != null && Instance != this) 
            { 
                Destroy(this); 
            } 
            else 
            { 
                Instance = this; 
            }
            
            Players = new List<PlayerInput>();
            
            foreach (var player in GetComponentsInChildren<PlayerInput>())
            {
                Debug.Log(player.name);
                Players.Add(player);
                player.enabled = false;
            }
        }

        public void Start()
        {
            if (Players.Count == 0)
            {
                Debug.Log("No players???");
            }
            else
            {
                CurrentPlayer = Players[0];
                PlayerIndex = 0;
                CurrentPlayer.enabled = true;
                SwitchCameraView();
            }
        }

        public void SwitchPlayer()
        {
            if (Players.Count > 1)
            {
                CurrentPlayer.enabled = false;
                if (PlayerIndex + 1 >= Players.Count)
                {
                    CurrentPlayer = Players[0];
                    PlayerIndex = 0;
                }
                else
                {
                    CurrentPlayer = Players[++PlayerIndex];
                }
                CurrentPlayer.enabled = true;
                SwitchCameraView();
                Debug.Log(CurrentPlayer.name);
            }
        }

        private void SwitchCameraView()
        {
            var player = CurrentPlayer.transform;
            activeCamera.Follow = player;
            activeCamera.LookAt = player;
        }
    }
}
