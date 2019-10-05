using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCameraInteract : MonoBehaviour
{
    public float interactDistance = 10.0f;
    protected Camera camera;
    private Interactable curInteractable;

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
        Debug.DrawLine(transform.position, transform.position + interactDistance * transform.forward, Color.red);
        if (Physics.Raycast(ray, out hit))
        {
            GameObject hitGameObject = hit.collider.gameObject;
            Debug.Log(hitGameObject.name);
            Interactable hitObject = hitGameObject.GetComponent<Interactable>();
            if (hit.distance < interactDistance)
            {
                Debug.Log("Hitting");

                if (hitObject != null)
                {
                    if (hitObject != curInteractable)
                    {
                        hitObject.OnBeginInteract();
                        curInteractable = hitObject;
                    }
                    else if (hitObject == curInteractable)
                    {

                    }
                    else
                    {
                        if (curInteractable != null) hitObject.OnEndInteract();
                        curInteractable = null;
                    }
                }
                else if (curInteractable != null)
                {
                    curInteractable.OnEndInteract();
                    curInteractable = null;
                }
                
            }
            Debug.LogWarning("Hit dist: " + hit.distance);
        }
    }


    private void FixedUpdate()
    {
        if (curInteractable)
        {
            if (Input.GetMouseButtonDown(0))
            {
                curInteractable.isBeingHeld = !curInteractable.isBeingHeld;
                curInteractable.OnHoldInteraction(curInteractable.isBeingHeld);
            }

            if (curInteractable.isBeingHeld)
            {
                curInteractable.UpdateTransform(this.transform);
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


