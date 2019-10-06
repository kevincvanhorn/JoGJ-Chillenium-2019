using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void ToggleMethod(bool value);

public class Interactable : MonoBehaviour
{
    public static ToggleMethod toggleDelegate;

    public float targetWidth = 0.3f;
    public float throwStrength = 20.0f;
    private bool isLookedAt;
    public float pushStrength = 20.0f;
    public float pushDistance = 10.0f;

    public bool isBeingHeld = false;
    public bool isToggled = false;
    public float HoldOffset;

    protected Rigidbody rigidbody;
    protected Collider collider;

    public PCameraInteract CameraParent;

    public enum EInteractType{
        EHoldable,
        EMoveable,
        EToggleable,
        EPushable,
        EStationaryCharger
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
        CharacterAbility.onPushDelegate += OnPush;  
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
        else if (InteractType == EInteractType.EToggleable)
        {
            PerformToggleAction();
        }
    }

    public void OnSecondaryInteract(PCameraInteract cameraParentIn)
    {
        if(InteractType == EInteractType.EHoldable)
        {
            if(isBeingHeld)
            {
                rigidbody.isKinematic = false;
                collider.enabled = true;
                isBeingHeld = false;
                rigidbody.AddForce(cameraParentIn.transform.forward * throwStrength, ForceMode.Impulse);
            }
        }
    }

   public void OnBeginLooked()
    {
        if(GetComponent<Renderer>().materials.Length > 1)
        {
            GetComponent<Renderer>().materials[1].SetFloat("_Outline", targetWidth);
        }
        isLookedAt = true;
    }

    public void OnEndLooked()
    {
        if (GetComponent<Renderer>().materials.Length > 1)
        {
            GetComponent<Renderer>().materials[1].SetFloat("_Outline", 0);
        }
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

    public void OnPush()
    {
        float objectDistance = Vector3.Distance(Camera.main.transform.position, transform.position);
        CurrentPlayer curPlayer = FindObjectOfType<CurrentPlayer>(); 
        if(objectDistance < pushDistance)
        {
            rigidbody.AddExplosionForce(pushStrength, Camera.main.transform.position, pushDistance, 0.0f, ForceMode.Impulse);
        }
    }

    protected virtual void PerformToggleAction() {
        isToggled = !isToggled;
        if(toggleDelegate != null) toggleDelegate(isToggled); //make all of the callbacks that have been registered
    }
}
