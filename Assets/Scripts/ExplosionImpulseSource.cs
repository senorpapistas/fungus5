using Unity.Cinemachine;
using UnityEngine;

public class ExplosionImpulseSource : MonoBehaviour
{
    public CinemachineImpulseSource impulseSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Vector3 velocity = new Vector3(UnityEngine.Random.Range(-.5f, .5f), UnityEngine.Random.Range(-.5f, .5f), UnityEngine.Random.Range(-.5f, .5f));
        impulseSource.GenerateImpulse();
    }
}
