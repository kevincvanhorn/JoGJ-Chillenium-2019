using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircuitTrigger : MonoBehaviour
{
    public List<MetallicInteractable> objectsInTriggerZone; //Metallic objects?
    public CircuitTrigger companionTrigger; //this is the other trigger that works with this one to detect a connection
    public List<MetallicInteractable> connectingObjects;
    public Circuit circuit;

    void OnTriggerEnter(Collider other) {
        MetallicInteractable m = other.gameObject.GetComponent<MetallicInteractable>();
        if(m != null) {
            objectsInTriggerZone.Add(m);

            //check if there is now a connection
            if(companionTrigger.objectsInTriggerZone.Contains(m)) {
                circuit.setConnected(true);
                connectingObjects.Add(m);
                companionTrigger.objectsInTriggerZone.Add(m);
            }
        }
    }

    void OnTriggerExit(Collider other) {
        MetallicInteractable m = other.gameObject.GetComponent<MetallicInteractable>();
        if(m != null) {
            objectsInTriggerZone.Remove(m);
            if(connectingObjects.Contains(m)) { //m no longer connects the trigger zones
                connectingObjects.Remove(m);
                companionTrigger.connectingObjects.Remove(m);
                if(connectingObjects.Count == 0) circuit.setConnected(false); //no connecting objects means the circuit is not connected
            }

        }
    }
}
