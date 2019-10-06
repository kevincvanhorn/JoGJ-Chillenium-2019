using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour, IPowerSource
{
    private float currentAmount;
    public float percentage;
    public float capacity = 100f;
    public float invCapacity;

    // Start is called before the first frame update
    void Start()
    {
        currentAmount = capacity;
        percentage = currentAmount * invCapacity;
        invCapacity = 1 / capacity;
    }

    public float chargeBattery(float amount) {
        float oldAmount = currentAmount;
        currentAmount += amount;
        if(currentAmount > capacity) currentAmount = capacity;
        Debug.Log(currentAmount);
        percentage = currentAmount * invCapacity;
        //return the amount of charge gained
        return ((capacity - oldAmount) > amount) ? amount : (capacity - oldAmount);
    }

    public float drainBattery(float amount) {
        float oldAmount = currentAmount;
        currentAmount -= amount;
        if(currentAmount < 0) currentAmount = 0;
        Debug.Log(currentAmount);
        percentage = currentAmount * invCapacity;
        //return the amount of charge drained
        return (oldAmount > amount) ? amount : oldAmount;
    }

    public float getCurrentAmount() {
        return currentAmount;
    }

    public float drainPower(float amount) {
        return drainBattery(amount);
    }

    public void reset() {
        currentAmount = capacity;
    }
}
