using System;
using Cinemachine;
using Player;
using UnityEngine;
using UnityEngine.Rendering;

namespace Cameras
{
    public class CameraTrigger : MonoBehaviour
    {
        [SerializeField] private VolumeProfile newProfile;
        [SerializeField] private CinemachineVirtualCameraBase newCamera;

        public CameraState newState;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("PlayerMesh"))
            {
                var player = other.GetComponentInParent<Player.Player>();
                if (player.CameraHandler.CurrentState != newState)
                {
                    player.CameraHandler.ChangeState(newState);
                    player.CameraHandler.ChangeCamera(newCamera);
                    player.CameraHandler.ChangeVolumeProfile(newProfile);
                }
            }
        }
    }
}