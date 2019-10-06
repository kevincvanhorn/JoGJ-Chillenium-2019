using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerOrbHolder : MonoBehaviour, IPowerSource
{
    protected Battery powerOrb; // a reference to the power orb this is holding
    protected PowerTransfer orbInteractable;

    void Update() {
        if(powerOrb != null) {
            if(!orbInteractable.isBeingHeld) {
                powerOrb.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
            else {
                powerOrb = null;
                orbInteractable = null;
            }
        }
    }

    public float drainPower(float amount)
    {
        return (powerOrb == null) ? 0f : powerOrb.drainBattery(amount * Time.deltaTime);
    }

    public float getCurrentAmount()
    {
        return (powerOrb == null) ? 0f : powerOrb.getCurrentAmount();
    }

    void OnTriggerEnter(Collider other) {
        Battery b = other.gameObject.GetComponent<Battery>();
        PowerTransfer p = other.gameObject.GetComponent<PowerTransfer>();

        if(powerOrb == null && b != null &&  p != null && p.InteractType == Interactable.EInteractType.EHoldable) {
            powerOrb = b;
            orbInteractable = p;

            //snap it into place
            Vector3 pos = this.transform.position;
            pos.y += 1f;
            powerOrb.gameObject.transform.position = pos;
            powerOrb.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }

    void OnTriggerExit(Collider other) {
        Battery b = other.gameObject.GetComponent<Battery>();
        PowerTransfer p = other.gameObject.GetComponent<PowerTransfer>();

        if(powerOrb == null && b != null &&  p != null && p.InteractType == Interactable.EInteractType.EHoldable) {
            powerOrb = null;
            orbInteractable = null;
        }
    }

    
}
