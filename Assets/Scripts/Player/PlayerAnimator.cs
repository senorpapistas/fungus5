using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public bool moving;
    public Animator animator;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        CheckMove();
        if (moving) { animator.SetBool("Walking",true); }
        else { animator.SetBool("Walking", false); }
    }

    //this is so chopped
    void CheckMove()
    {
        if (Input.GetKey(KeyCode.W)) { moving = true; }
        else if (Input.GetKey(KeyCode.A)) { moving = true; }
        else if (Input.GetKey(KeyCode.S)) { moving = true; }
        else if (Input.GetKey(KeyCode.D)) { moving = true; }
        else { moving = false; }
    }
}
