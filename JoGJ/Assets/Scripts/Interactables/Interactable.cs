using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float targetWidth = 5.0f;
    private bool isLookedAt;

    public bool isBeingHeld = false;
    public float HoldOffset;

    protected Rigidbody rigidbody;
    protected Collider collider;
     
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().materials[1].SetFloat("_Outline", 0);
        isLookedAt = false;
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
        isBeingHeld = false;
    }

    public void UpdateTransform(Transform camTrans)
    {
        if (rigidbody)
        {
            rigidbody.transform.position = camTrans.position + HoldOffset* camTrans.forward;
        }
    }

    public void OnHoldInteraction(bool bHeld)
    {
        if (!rigidbody || !collider) return;

        if (isBeingHeld && rigidbody && collider)
        {
            rigidbody.isKinematic = true;
            collider.enabled = false;
        }
        else if (!isBeingHeld)
        {
            rigidbody.isKinematic = false;
            collider.enabled = true;
        }
    }

   public void OnBeginInteract()
    {
        GetComponent<Renderer>().materials[1].SetFloat("_Outline", targetWidth);
        isLookedAt = true;
    }

    public void OnEndInteract()
    {
        GetComponent<Renderer>().materials[1].SetFloat("_Outline", 0);
        isLookedAt = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
