using System.Collections;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public float duration = 1.0f;
    public AnimationCurve curve;
    public bool start = false;

    private void OnEnable()
    {
        Enemy.EnemyDeathEvent += OnEnemyDeathEvent;
    }

    private void OnDisable()
    {
        Enemy.EnemyDeathEvent -= OnEnemyDeathEvent;
    }

    private IEnumerator Shaking()
    {
        Vector3 startPos = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float strength = curve.Evaluate(elapsedTime / duration);
            transform.position = startPos + Random.insideUnitSphere * strength;
            yield return null;
        }

        transform.position = startPos;
    }

    private void Shake ()
    {
        start = true;
        if (start)
        {
            start = false;
            StartCoroutine(Shaking());
        }
    }

    public void OnEnemyDeathEvent() 
    {
        Shake();
    }
}
