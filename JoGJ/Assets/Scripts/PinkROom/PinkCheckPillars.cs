using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PinkCheckPillars : MonoBehaviour
{
    public List<PillarsPink> Pillars;

    void Start()
    {
    }

    public void OnPillarChanged()
    {
        int numActive = 0;

        for(int i =0; i < Pillars.Count; ++i)
        {
            if (Pillars[i].bIsActive) numActive++;
        }

        gameObject.SetActive(numActive == 4);
    }
}
