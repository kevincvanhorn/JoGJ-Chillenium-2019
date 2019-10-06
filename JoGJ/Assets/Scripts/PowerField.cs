using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerField : MonoBehaviour, IPowerSource
{
    public float chargeAmount = 2.0f;

    //Power can be drained from the field without limit
    public float drainPower(float amount)
    {
        return amount;
    }

    public float getCurrentAmount()
    {
        return float.MaxValue / 100f;
    }

    private void OnTriggerStay(Collider other) {
        Battery objectBattery = other.gameObject.GetComponent<Battery>();
        if(objectBattery != null) {
            objectBattery.chargeBattery(chargeAmount * Time.deltaTime);
        }
    }
}
