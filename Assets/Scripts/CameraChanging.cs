using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraChanging : MonoBehaviour
{
    [SerializeField] private List<CinemachineVirtualCamera> cameras;
   
    public void ChangeCamera(CameraType cameraType)
    {
        SetCameraPrioritiesToZero();
        cameras[(int)cameraType].Priority = 1;
    }

    private void SetCameraPrioritiesToZero()
    {
        foreach (var cam in cameras)
            cam.Priority = 0;
    }
}

public enum CameraType
{
    Main,
    Finish
}
