using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EHighlighted : MonoBehaviour
{
    public Material ActivatedMaterial;
    public Material DeactivatedMaterial;
    private Renderer renderer;
    public void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    public void UpdateMaterial(bool bPowered)
    {
        if (bPowered)
        {
            renderer.material = ActivatedMaterial;
        }
        else
        {
            renderer.material = DeactivatedMaterial;
        }
    }
}
