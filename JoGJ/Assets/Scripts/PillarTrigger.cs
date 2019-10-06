using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarTrigger : MonoBehaviour
{
    public bool isColliding;
    // Start is called before the first frame update
    void Start()
    {
        isColliding = false;
    }
        
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject != gameObject)
        {
            if(other.gameObject.layer == 2)
            {

            }
            else
            {
                isColliding = true;
            }
        }
    }
}
