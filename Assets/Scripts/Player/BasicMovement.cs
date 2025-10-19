using UnityEngine;

public class BasicMovement : MonoBehaviour, IMoveable
{
    public float moveSpeed = 5f;
    public float turnSpeed = 120f;

    private Rigidbody rb;
    private float moveInput;
    private float turnInput;
    private bool canMove = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

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
            //Move(Vector3.forward * moveInput);
            //transform.Rotate(Vector3.up, turnInput * turnSpeed);

            Vector3 moveDirection = transform.forward * moveInput * moveSpeed;
            //rb.AddForce(moveDirection, ForceMode.Force);
            rb.linearVelocity = moveDirection;

            float rotation = turnInput * turnSpeed;
            Quaternion turnRotation = Quaternion.Euler(0f, rotation, 0f);
            rb.MoveRotation(rb.rotation * turnRotation);
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

    private void OnEnable()
    {
        UpgradeEvents.PlayerMSIncreased += IncreaseSpeed;
    }

    private void OnDisable()
    {
        UpgradeEvents.PlayerMSIncreased -= IncreaseSpeed;
    }

    private void IncreaseSpeed(float amount)
    {
        moveSpeed += amount;
    }
}
