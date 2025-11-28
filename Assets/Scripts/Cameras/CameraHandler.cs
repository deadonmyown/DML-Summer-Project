using Cinemachine;
using Player;
using UnityEngine;
using UnityEngine.Rendering;

namespace Cameras
{
    public class CameraHandler : MonoBehaviour
    {
        public CameraState CurrentState { get; private set; } = CameraState.Follow;
        public CinemachineVirtualCameraBase CurrentCamera { get; private set; }
        public VolumeProfile CurrentVolumeProfile { get; private set; }

        [SerializeField] private CinemachineVirtualCameraBase startCamera;
        [SerializeField] private Volume startVolume;

        private void Awake()
        {
            CurrentState = CameraState.Follow;
            CurrentCamera = startCamera;
            CurrentVolumeProfile = startVolume.profile;
        }

        public void ChangeState(CameraState newState)
        {
            CurrentState = newState;
        }

        public void ChangeCamera(CinemachineVirtualCameraBase newCamera)
        {
            CurrentCamera = newCamera;
            if (this == PlayerManager.Instance.CurrentPlayer.CameraHandler)
            {
                PlayerManager.Instance.SwitchActiveCamera(newCamera);
            }
        }

        public void ChangeVolumeProfile(VolumeProfile newProfile)
        {
            CurrentVolumeProfile = newProfile;
            if (this == PlayerManager.Instance.CurrentPlayer.CameraHandler)
            {
                PlayerManager.Instance.SwitchGlobalVolume(newProfile);
            }
        }
    }
}
