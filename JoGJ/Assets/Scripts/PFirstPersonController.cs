using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PFirstPersonController : MonoBehaviour
{
    // Input Variables:

    // Movement Variables
    public float PlayerSpeed = 10.0f;
    public float PlayerSprintSpeed = 50.0f;
    public float PlayerJumpHeight = 5.0f;
    protected Vector3 PlayerDirection;

    protected Rigidbody rigidBody;
    private Vector3 PlayerDirX, PlayerDirZ;
    private bool PlayerJumpInput;
    private bool PlayerSprintInput;
    private bool onGround;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        // Get Components attached to this object.
        rigidBody = GetComponent<Rigidbody>();
        Debug.Log("start");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Grounded");
        onGround = true;
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Airborne");
        onGround = false;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (!rigidBody) return;

        PlayerDirZ = Input.GetAxis("Vertical") * rigidBody.transform.forward;
        PlayerDirX = Input.GetAxis("Horizontal") * rigidBody.transform.right;
        PlayerJumpInput = Input.GetButton("Jump");
        PlayerSprintInput = Input.GetButton("Sprint");

        Vector3 velocity;

        if (PlayerSprintInput)
        {
            velocity = PlayerSprintSpeed * (PlayerDirZ + PlayerDirX);
        }
        else
        {
            velocity = PlayerSpeed * (PlayerDirZ + PlayerDirX);
        }

        velocity.y = rigidBody.velocity.y;

        Debug.Log("OnGround: " + onGround);
        if (PlayerJumpInput)
        {
            if (onGround)
            {
                velocity = new Vector3(rigidBody.velocity.x, PlayerJumpHeight, rigidBody.velocity.z);
            }
        }

        if (velocity.y > 0 && !(PlayerJumpInput))
        {
            velocity += Vector3.up * Physics.gravity.y * (2 - 1) * Time.deltaTime;
        }

        rigidBody.velocity = velocity;
    }
}
