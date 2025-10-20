using System.Collections;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public float duration = 1.0f;
    public AnimationCurve curve;
    public bool start = false;
    public Transform defaultCamPos;

    private void OnEnable()
    {
        Enemy.EnemyDeathEvent += Shake;
        PlayerHealth.PlayerChangeHealthEvent += Shake;
    }

    private void OnDisable()
    {
        Enemy.EnemyDeathEvent -= Shake;
        PlayerHealth.PlayerChangeHealthEvent -= Shake;
    }

    private IEnumerator Shaking()
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float strength = curve.Evaluate(elapsedTime / duration);
            transform.position = transform.position + Random.insideUnitSphere * strength;
            yield return null;
            transform.position = defaultCamPos.transform.position;
        }
    }

    private void Shake ()
    {
        start = true;
        if (start)
        {
            start = false;
            StopAllCoroutines();
            StartCoroutine(Shaking());
        }
    }

    private void Shake(int bruh)
    {
        start = true;
        if (start)
        {
            start = false;
            StopAllCoroutines();
            StartCoroutine(Shaking());
        }
    }
}
