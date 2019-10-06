﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarsPink : MonoBehaviour
{

    public Material ActivatedMat;
    public Material DeactivatedMat;

    private Renderer renderer;

    private bool bIsActive = false;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {   
        if (collision.gameObject)
        {
            Interactable interactable = collision.gameObject.GetComponent<Interactable>();
            if (bIsActive)
            {
                renderer.material = ActivatedMat;
            }
            else
            {
                renderer.material = DeactivatedMat;
            }
            bIsActive = !bIsActive;
        }
    }
}
