using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableButton : MonoBehaviour
{
    public GameObject linkedObject;
    public BoxCollider trigger;
    private bool isTriggered;
    private Collider triggeredCollider;
    

    // Start is called before the first frame update
    void Start()
    {
        isTriggered = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isTriggered && !triggeredCollider)
        {
            transform.localPosition += new Vector3(-1.0f, 0.0f, 0.0f);
            trigger.size += new Vector3(1.0f, 0.0f, 0.0f);
            isTriggered = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        transform.localPosition += new Vector3(1.0f, 0.0f, 0.0f);
        trigger.size += new Vector3(-1.0f, 0.0f, 0.0f);
        triggeredCollider = other;
        linkedObject.SetActive(false);
        isTriggered = true;
    }

    private void OnTriggerExit(Collider other)
    {
        transform.localPosition += new Vector3(-1.0f, 0.0f, 0.0f);
        trigger.size += new Vector3(1.0f, 0.0f, 0.0f);
        linkedObject.SetActive(true);
        isTriggered = false;
    }
}
