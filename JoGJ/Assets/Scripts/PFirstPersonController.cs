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

    private Battery battery;
    public float movementBatteryDrain = 10.0f;

    private GameObject checkpoint; //the last checkpoint the player visited
    private Vector3 initialPostion; //where the character starts the game

    private float DefaultPlayerJumpHeight, DefaultPlayerSprintSpeed, DefaultPlayerSpeed;

    public float airFactor = 1;
    public float batterySlowDown = 0.5f;
    private bool bHitThisFrame;

    UIManager uiManager;

    // Start is called before the first frame update
    void Start()
    {
        battery = this.GetComponent<Battery>();
        uiManager = FindObjectOfType<UIManager>();

        initialPostion = gameObject.transform.position;

        Cursor.lockState = CursorLockMode.Locked;

        // Get Components attached to this object.
        rigidBody = GetComponent<Rigidbody>();
        Debug.Log("start");

        DefaultPlayerJumpHeight = PlayerJumpHeight;
        DefaultPlayerSprintSpeed = PlayerSprintSpeed;
        DefaultPlayerSpeed = PlayerSpeed;
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

        //check for dead battery
        if(battery.getCurrentAmount() <= 0.01f){
            PlayerJumpHeight = PlayerSprintSpeed = PlayerSpeed = 0;
        }
        else { //use normal values
            PlayerJumpHeight = DefaultPlayerJumpHeight;
            PlayerSprintSpeed = DefaultPlayerSprintSpeed;
            PlayerSpeed = DefaultPlayerSpeed;
        }

        PlayerDirZ = Input.GetAxis("Vertical") * rigidBody.transform.forward;
        PlayerDirX = Input.GetAxis("Horizontal") * rigidBody.transform.right;
        PlayerJumpInput = Input.GetButton("Jump");
        PlayerSprintInput = Input.GetButton("Sprint");

        Vector3 velocity;

        if (PlayerSprintInput)
        {
            velocity = PlayerSprintSpeed * (PlayerDirZ + PlayerDirX) ;
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

        if (!onGround)
        {
            float yCache = velocity.y;
            velocity.x = rigidBody.velocity.x * (1-airFactor) + velocity.x * airFactor;
            velocity.z = rigidBody.velocity.z *(1-airFactor) + velocity.z* airFactor;
        }

        //rigidBody.velocity = velocity;
        rigidBody.velocity = velocity;


        //drain battery
        float amount = Mathf.Abs(Input.GetAxis("Vertical")) + Mathf.Abs(Input.GetAxis("Horizontal"));
        amount *= (PlayerSprintInput) ? PlayerSprintSpeed : PlayerSpeed;
        amount /= PlayerSpeed;
        if (amount > 0.01f)
        {
            battery.drainBattery(amount * movementBatteryDrain * Time.deltaTime);
        }

        if (uiManager) uiManager.drainBattery(this, battery.percentage);
    }

    //use when the robot runs out of battery
    public void reset() {
        gameObject.transform.position = (checkpoint != null) ? checkpoint.transform.position : initialPostion;
        battery.reset();
    }

    public void setCheckpoint(GameObject checkpoint) {
        this.checkpoint = checkpoint;
    }
}
