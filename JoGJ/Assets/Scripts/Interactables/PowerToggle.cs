using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerToggle : Interactable, IPoweredDevice
{
    protected bool powered;
    public bool Powered { get {return powered;} set {powered = value;} }
    public float powerDrainAmount = 2.0f;


    public float powerNeeded()
    {
        return powerDrainAmount;
    }

    //override to add check for if powered
    protected override void PerformToggleAction() {

        if(powered) {
            Debug.Log("Toggling");
            isToggled = !isToggled;
            if(toggleDelegate != null) toggleDelegate(isToggled); //make all of the callbacks that have been registered
        }
        else {
        //else give some negative sound effect
            Debug.Log("No power");
        }
    }
}
