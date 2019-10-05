using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCameraController : MonoBehaviour
{
    public float CamSensitivity;
    public float CamSmoothing;

    public Rigidbody PlayerRigidbody;

    private Vector2 mouseLook;
    private Vector2 smoothV;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!PlayerRigidbody) return;

        Vector2 CamInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        CamInput = Vector2.Scale(CamInput, new Vector2(CamSensitivity * CamSmoothing, CamSensitivity * CamSmoothing));

        smoothV.x = Mathf.Lerp(smoothV.x, CamInput.x, 1f / CamSmoothing);
        smoothV.y = Mathf.Lerp(smoothV.y, CamInput.y, 1f / CamSmoothing);
        mouseLook += smoothV;

        // vector3.right means the x-axis
        transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
        PlayerRigidbody.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, PlayerRigidbody.transform.up);
    }
}
