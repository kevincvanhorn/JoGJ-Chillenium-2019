using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float targetWidth = 5.0f;
    private bool isLookedAt;
     
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().materials[1].SetFloat("_Outline", 0);
        isLookedAt = false;
    }

   public void OnBeginInteract()
    {
        GetComponent<Renderer>().materials[1].SetFloat("_Outline", targetWidth);
        isLookedAt = true;
    }

    public void OnEndInteract()
    {
        GetComponent<Renderer>().materials[1].SetFloat("_Outline", 0);
        isLookedAt = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
