﻿using System.Collections;
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

    public float chargeBattery(float amount) {
        float oldAmount = currentAmount;
        currentAmount += amount;
        if(currentAmount > capacity) currentAmount = capacity;
        //Debug.Log(currentAmount);
        //return the amount of charge gained
        return ((capacity - oldAmount) > amount) ? amount : (capacity - oldAmount);
    }

    public float drainBattery(float amount) {
        float oldAmount = currentAmount;
        currentAmount -= amount;
        if(currentAmount < 0) currentAmount = 0;
        //Debug.Log(currentAmount);
        //return the amount of charge drained
        return (oldAmount > amount) ? amount : oldAmount;
    }

    public float getCurrentAmount() {
        return currentAmount;
    }

    public void reset() {
        currentAmount = capacity;
    }
}
