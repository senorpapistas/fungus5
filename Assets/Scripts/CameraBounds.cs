using Unity.Cinemachine;
using UnityEngine;

public class CameraBounds : MonoBehaviour
{
    public CinemachineCamera fixedCamera { get; private set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
            fixedCamera = GetComponentInChildren<CinemachineCamera>();
            fixedCamera.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            //print("player entered " + name);
            FixedCameraManager.Instance.SwitchCamera(this);
        }
    }
}
