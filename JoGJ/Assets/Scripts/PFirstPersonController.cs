using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PFirstPersonController : MonoBehaviour
{
    // Input Variables:

    // Movement Variables
    public float PlayerSpeed = 10.0f;
    protected Vector3 PlayerDirection;

    protected Rigidbody rigidBody;
    private Vector3 PlayerDirX, PlayerDirZ;

    private Battery battery;
    public float movementBatteryDrain = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        battery = this.GetComponent<Battery>();

        Cursor.lockState = CursorLockMode.Locked;

        // Get Components attached to this object.
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!rigidBody) return;

        PlayerDirZ = Input.GetAxis("Vertical") * rigidBody.transform.forward;
        PlayerDirX = Input.GetAxis("Horizontal") * rigidBody.transform.right;

        Vector3 velocity = PlayerSpeed * (PlayerDirZ + PlayerDirX);
        velocity.y = rigidBody.velocity.y;

        rigidBody.velocity = velocity;

        //drain battery
        float amount = Mathf.Abs(Input.GetAxis("Vertical")) + Mathf.Abs(Input.GetAxis("Horizontal"));
        if(amount > 0.01f) battery.drainBattery(amount * movementBatteryDrain * Time.deltaTime);
    }
}
