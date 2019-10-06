using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporterpink : MonoBehaviour
{
    public Vector3 Offset;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void OnTriggerStay(Collider other)
    {
        if (other)
        {
            if (other.GetComponent<PFirstPersonController>())
            {
                other.gameObject.transform.position += Offset;
            }
        }
    }
}
