using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float turn = 1f;

    private float moveInput;
    private float turnInput;

    private void Update()
    {
        moveInput = Input.GetAxisRaw("Vertical");
        turnInput = Input.GetAxisRaw("Horizontal");
    }
    private void FixedUpdate()
    {

        transform.Translate(Vector3.forward * moveInput * moveSpeed);
        transform.Rotate(Vector3.up, turnInput * turn);
    }
}
