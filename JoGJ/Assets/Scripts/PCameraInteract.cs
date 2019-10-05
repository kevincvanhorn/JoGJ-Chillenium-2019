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
        Debug.DrawLine(transform.position, transform.position + interactDistance * transform.forward, Color.yellow);
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
                        hitObject.OnBeginLooked();
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
            Debug.LogWarning("Hit dist: " + hit.distance);
        }
    }


    private void FixedUpdate()
    {
        if (curInteractable)
        {
            if (Input.GetMouseButtonDown(0))
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


