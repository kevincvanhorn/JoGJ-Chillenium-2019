using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour
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
<<<<<<< HEAD
        Debug.Log(currentAmount);
        percentage = currentAmount * invCapacity;
=======
        //Debug.Log(currentAmount);
>>>>>>> 9cfb2a004a62d928058547ea0da302a401076cb3
        //return the amount of charge gained
        return ((capacity - oldAmount) > amount) ? amount : (capacity - oldAmount);
    }

    public float drainBattery(float amount) {
        float oldAmount = currentAmount;
        currentAmount -= amount;
        if(currentAmount < 0) currentAmount = 0;
<<<<<<< HEAD
        Debug.Log(currentAmount);
        percentage = currentAmount * invCapacity;
=======
        //Debug.Log(currentAmount);
>>>>>>> 9cfb2a004a62d928058547ea0da302a401076cb3
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
