using UnityEngine;

public class BasicMovement : MonoBehaviour, IMoveable
{
    public float moveSpeed = 5f;
    public float turnSpeed = 120f;

    private float moveInput;
    private float turnInput;
    private bool canMove = true;

    private void Update()
    {
        if (canMove)
        {
            moveInput = Input.GetAxisRaw("Vertical");
            turnInput = Input.GetAxisRaw("Horizontal");
        }
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            Move(Vector3.forward * moveInput);
            transform.Rotate(Vector3.up, turnInput * turnSpeed);
        }
    }

    public void Move(Vector3 direction)
    {
        transform.Translate(direction * moveSpeed);
    }

    public void SetSpeed(float speed)
    {
        moveSpeed = speed;
    }

    public void Stop()
    {
        canMove = false;
        moveInput = 0;
        turnInput = 0;
    }
}
