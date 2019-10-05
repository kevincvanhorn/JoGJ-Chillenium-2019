using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour
{
    private float currentAmount;
    public float capacity = 100f;

    // Start is called before the first frame update
    void Start()
    {
        currentAmount = capacity;
    }

    public void chargeBattery(float amount) {
        currentAmount += amount;
        if(currentAmount > capacity) currentAmount = capacity;
        Debug.Log(currentAmount);
    }

    public void drainBattery(float amount) {
        currentAmount -= amount;
        if(currentAmount < 0) currentAmount = 0;
        Debug.Log(currentAmount);
    }
}
