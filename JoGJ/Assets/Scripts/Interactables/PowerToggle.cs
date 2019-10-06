using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerToggle : Interactable, IPoweredDevice
{
    protected bool powered;
    public bool Powered { get {return powered;} set {powered = value;} }
    public float powerDrainAmount = 2.0f;

    public RaiseablePlatform platform;

    public Material ActivatedMaterial;
    public Material DeactivatedMaterial;
    private Renderer renderer;
    public void Start()
    {
        renderer = GetComponent<Renderer>();
    }
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
            if (isToggled)
            {
                renderer.material = ActivatedMaterial;
            }
            else
            {
                renderer.material = DeactivatedMaterial;
            }
            if (platform) platform.SwitchLocation();
        }
        else {
        //else give some negative sound effect
            Debug.Log("No power");
            renderer.material = DeactivatedMaterial;
        }
    }
}
