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

    public PCameraInteract CameraParent;

    public enum EInteractType{
        EHoldable,
        EMoveable
    }

    public EInteractType InteractType;

     
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
            rigidbody.transform.position = camTrans.position + HoldOffset * camTrans.forward;
        }
    }

    public void OnInteract(PCameraInteract cameraParentIn)
    {
        CameraParent = cameraParentIn;
        if (!CameraParent) return;

        if (InteractType == EInteractType.EHoldable)
        {
            if (!rigidbody || !collider) return;
            isBeingHeld = !isBeingHeld;

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
    }

   public void OnBeginLooked()
    {
        GetComponent<Renderer>().materials[1].SetFloat("_Outline", targetWidth);
        isLookedAt = true;
    }

    public void OnEndLooked()
    {
        GetComponent<Renderer>().materials[1].SetFloat("_Outline", 0);
        isLookedAt = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (CameraParent)
        {
            if(InteractType == EInteractType.EHoldable && isBeingHeld)
            {
                UpdateTransform(CameraParent.transform);
            }
        }   
    }
}
