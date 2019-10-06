using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextTrigger : MonoBehaviour
{
    public string message;
    private UIManager uIManager;
    
    void OnTriggerEnter(Collider other) {
        if(other.gameObject.GetComponent<PFirstPersonController>() == null) return;
        if(uIManager == null) uIManager = FindObjectOfType<UIManager>();
        uIManager.DisplayMessage(message);
        Debug.Log("Displaying message: " + message);
    }
}
