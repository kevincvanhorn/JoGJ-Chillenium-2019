using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerField : MonoBehaviour
{
    public float chargeAmount = 2.0f;

    private void OnTriggerStay(Collider other) {
        Battery objectBattery = other.gameObject.GetComponent<Battery>();
        if(objectBattery != null) {
            objectBattery.chargeBattery(chargeAmount * Time.deltaTime);
        }
    }
}
