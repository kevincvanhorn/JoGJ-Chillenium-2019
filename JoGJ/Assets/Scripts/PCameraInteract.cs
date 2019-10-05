using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCameraInteract : MonoBehaviour
{
    public float interactDistance = 10.0f;
    protected Camera camera;
    private Interactable previousLook;
    private Interactable currentLook;
    

    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponent<Camera>();
        previousLook = null;
        currentLook = null;

        StartCoroutine("CastTick");
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
                    if (hitObject != previousLook)
                    {
                        hitObject.OnBeginInteract();
                        previousLook = hitObject;
                    }
                    else if (hitObject == previousLook)
                    {

                    }
                    else
                    {
                        if (previousLook != null) hitObject.OnEndInteract();
                        previousLook = null;
                    }
                }
                else if (previousLook != null)
                {
                    previousLook.OnEndInteract();
                    previousLook = null;
                }
                
            }
            //Debug.Log("Hit dist: " + hit.distance);
        }
    }
}
