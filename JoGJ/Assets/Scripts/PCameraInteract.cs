using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCameraInteract : MonoBehaviour
{
    public float interactDistance = 10.0f;
    protected Camera camera;
    private Interactable curInteractable;
    private bool mouseClick;

    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponent<Camera>();
        curInteractable = null;

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
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.position + interactDistance * transform.forward);
        Debug.DrawLine(transform.position, transform.position + interactDistance * transform.forward, Color.yellow);
        if (Physics.Raycast(ray, out hit))
        {
            GameObject hitGameObject = hit.collider.gameObject;
            Debug.Log(hitGameObject.name);
            Interactable hitObject = hitGameObject.GetComponent<Interactable>();
            if (hit.distance < interactDistance)
            {
                Debug.Log("Hitting");

                if (hitObject != null) // Object is interactable
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
                
            }
            //Debug.LogWarning("Hit dist: " + hit.distance);
        }
        else if (curInteractable != null)
        {
            switch (curInteractable.InteractType)
            {
                case Interactable.EInteractType.EHoldable:
                    if(!curInteractable.isBeingHeld)
                    {
                        curInteractable.OnEndLooked();
                        curInteractable = null;
                    }
                    break;
                default:
                    curInteractable.OnEndLooked();
                    curInteractable = null;
                    break;
            }
        }
    }

    private void Update()
    {
        mouseClick = Input.GetMouseButtonDown(0);
    }


    private void FixedUpdate()
    {
        if (curInteractable)
        {
            if (mouseClick)
            {
                curInteractable.OnInteract(this);
            }
            else
            {
                PlayerInteraction();
            }
        }
        else
        {
            PlayerInteraction();
        }
    }
}


