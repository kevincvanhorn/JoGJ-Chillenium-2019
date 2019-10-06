using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    private Renderer renderer;
    public float Speed = 0.01f;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (renderer)
        {
            renderer.material.mainTextureOffset = new Vector2(renderer.material.mainTextureOffset.x, renderer.material.mainTextureOffset.y + Speed);
        }   
    }
}
