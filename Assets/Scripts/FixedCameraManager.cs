using UnityEngine;

public class FixedCameraManager : MonoBehaviour
{
    public CameraBounds currentCameraBounds;

    public static FixedCameraManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        if (currentCameraBounds == null)
        {
            print("Need a Camera!");
        }
        else
        {
            //currentCameraBounds.fixedCamera.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchCamera(CameraBounds newCameraBounds)
    {
        currentCameraBounds.fixedCamera.gameObject.SetActive(false);
        currentCameraBounds = newCameraBounds;
        currentCameraBounds.fixedCamera.gameObject.SetActive(true);
    }
}
