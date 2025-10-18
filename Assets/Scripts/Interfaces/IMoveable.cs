using UnityEngine;

public interface IMoveable
{
    void Move(Vector3 direction);
    void SetSpeed(float speed);
    void Stop();
}