using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using Cameras;
using UnityEngine.Rendering;
using CameraState = Cameras.CameraState;

namespace Player
{
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager Instance { get; private set; }
        public IList<Player> Players { get; private set; }
        public Player CurrentPlayer { get; private set; }
        public int PlayerIndex { get; private set; }

        [SerializeField] private CinemachineVirtualCameraBase activeCamera;

        [SerializeField] private Volume volume;

        public CinemachineVirtualCameraBase ActiveCamera => activeCamera;
        public Volume Volume => volume;

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
            
            Players = new List<Player>();
            
            foreach (var player in GetComponentsInChildren<Player>())
            {
                Debug.Log(player.name);
                Players.Add(player);
            }
        }

        public void Start()
        {
            for (int i = 0; i < Players.Count; i++)
            {
                Players[i].TurnPlayerScripts(false);
            }

            if (Players.Count == 0)
            {
                Debug.Log("No players???");
            }
            else
            {
                CurrentPlayer = Players[0];
                PlayerIndex = 0;
                CurrentPlayer.TurnPlayerScripts(true);
                CheckCameraState();
            }
        }

        public void SwitchPlayer()
        {
            if (Players.Count > 1)
            {
                CurrentPlayer.TurnPlayerScripts(false);
                if (PlayerIndex + 1 >= Players.Count)
                {
                    CurrentPlayer = Players[0];
                    PlayerIndex = 0;
                }
                else
                {
                    CurrentPlayer = Players[++PlayerIndex];
                }
                CurrentPlayer.TurnPlayerScripts(true);
                SwitchActiveCamera(CurrentPlayer.CameraHandler.CurrentCamera);
                SwitchGlobalVolume(CurrentPlayer.CameraHandler.CurrentVolumeProfile);
                Debug.Log(CurrentPlayer.name);
            }
        }
        
        private void SwitchCameraSettings()
        {
            SwitchActiveCamera(CurrentPlayer.CameraHandler.CurrentCamera);
            SwitchGlobalVolume(CurrentPlayer.CameraHandler.CurrentVolumeProfile);
        }
        
        private void CheckCameraState()
        {
            switch (CurrentPlayer.CameraHandler.CurrentState)
            {
                case CameraState.Follow : 
                    SetupFollowCamera();
                    break;
            }
        }

        private void SetupFollowCamera()
        {
            var player = CurrentPlayer.transform;
            activeCamera.Follow = player;
            activeCamera.LookAt = player;
        }

        public void SwitchActiveCamera(CinemachineVirtualCameraBase newCamera)
        {
            if (activeCamera != newCamera)
            {
                activeCamera.gameObject.SetActive(false);
                activeCamera = newCamera;
                activeCamera.gameObject.SetActive(true);
            }
            CheckCameraState();
        }

        public void SwitchGlobalVolume(VolumeProfile newProfile) => volume.profile = newProfile;

        public void SwitchPlayerInputMap(string mapName) => CurrentPlayer.PlayerInput.SwitchCurrentActionMap(mapName);
        
    }
}
