using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCameraInteract : MonoBehaviour
{
    public float rayDistance = 1000.0f; // Distance the player ray can go
    public float interactDistance = 10.0f; // Distance the player can interact with objects
    private Interactable curInteractable;
    private CharacterAbility characterAbility;
    private bool primaryInteract;
    private bool secondaryInteract;
    private RaycastHit aimHit;

    // Start is called before the first frame update
    void Start()
    {
        curInteractable = null;
        characterAbility = GetComponent<CharacterAbility>();

       // StartCoroutine("CastTick");
    }

    IEnumerator CastTick()
    {
        while (true)
        {
            PlayerInteraction();
            yield return new WaitForSeconds(0.01f);
        }
    }

    private void PlayerInteraction()
    {
        if (curInteractable != null && curInteractable.isBeingHeld) // Stick with the object being held
        {
            return;
        }
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.position + rayDistance * transform.forward);
        Debug.DrawLine(transform.position, transform.position + rayDistance * transform.forward, Color.yellow);
        if (Physics.Raycast(ray, out hit))
        {
            GameObject hitGameObject = hit.collider.gameObject;
            //Debug.Log(hitGameObject.name);
            //Debug.LogWarning("Hit dist: " + hit.distance);

            Interactable hitObject = hitGameObject.GetComponent<Interactable>();

            if (hitObject != null && hit.distance < interactDistance) // Object is interactable and within interact distance
            {
                if (hitObject != curInteractable) // Object is a new interactable
                {
                    hitObject.OnBeginLooked();
                    if(curInteractable != null)
                    {
                        curInteractable.OnEndLooked();
                    }
                    curInteractable = hitObject;
                }
                else if (hitObject == curInteractable)
                {

                }
                else
                {
                    if (curInteractable != null) hitObject.OnEndLooked();
                    curInteractable = null;
                }
            }
            else if (curInteractable != null)
            {
                curInteractable.OnEndLooked();
                curInteractable = null;
            }

            if(hitObject == null)
            {
                aimHit = hit;
                //Debug.Log(aimHit.point);
            }
                
            
        }
        else if (curInteractable != null)
        {
            curInteractable.OnEndLooked();
            curInteractable = null;
            //switch (curInteractable.InteractType)
            //{
            //    case Interactable.EInteractType.EHoldable:
            //        if(!curInteractable.isBeingHeld)
            //        {
            //            curInteractable.OnEndLooked();
            //            curInteractable = null;
            //        }
            //        break;
            //    default:
            //        curInteractable.OnEndLooked();
            //        curInteractable = null;
            //        break;
            //}
        }
    }

    private void Update()
    {
        //primaryInteract = Input.GetButtonDown("Interact");
        //secondaryInteract = Input.GetButtonDown("Secondary Interact");
        primaryInteract = Input.GetMouseButtonDown(0);
        secondaryInteract = Input.GetMouseButtonDown(1);
    }


    private void FixedUpdate()
    {
        if (curInteractable)
        {
            if (primaryInteract)
            {
                curInteractable.OnInteract(this);
            }
            else if(secondaryInteract)
            {
                curInteractable.OnSecondaryInteract(this);
            }
            else
            {
                PlayerInteraction();
            }
        }
        else
        {
            /*if (primaryInteract)
            {
                characterAbility.OnUseAbility(aimHit);
            }*/
            if (secondaryInteract)
            {
                characterAbility.OnUseAbility(aimHit);
            }

            PlayerInteraction();
        }
    }
}


