using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableButton : MonoBehaviour
{
    public GameObject linkedObject;
    public GameObject buttonObject;
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
            buttonObject.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
            isTriggered = false;
            if (linkedObject.GetComponent<RaiseablePlatform>() != null)
            {
                linkedObject.GetComponent<RaiseablePlatform>().SwitchLocation();
            }
            else
            {
                linkedObject.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        buttonObject.transform.localPosition = new Vector3(1.0f, 0.0f, 0.0f);
        triggeredCollider = other;
        isTriggered = true;
        if (linkedObject.GetComponent<RaiseablePlatform>() != null)
        {
            linkedObject.GetComponent<RaiseablePlatform>().SwitchLocation();
        }
        else
        {
            linkedObject.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        buttonObject.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
        isTriggered = false;
        triggeredCollider = null;
        if (linkedObject.GetComponent<RaiseablePlatform>() != null)
        {
            linkedObject.GetComponent<RaiseablePlatform>().SwitchLocation();
        }
        else
        {
            linkedObject.SetActive(true);
        }
    }
}
