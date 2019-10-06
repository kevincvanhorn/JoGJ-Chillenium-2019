using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerTransfer : Interactable
{
    [SerializeField]
    protected Battery linkedBattery; //transfer power to/from this battery
    public float transferRate = 2.0f; //power transfer rate per second

    //how to reference the player? use the current player in player manager
    public void DrainLinkedBattery() {
        //charges the current player's battery by the amount that is able to be drained from the linked battery
        CurrentPlayer.instance.currentPlayer.GetComponent<Battery>().chargeBattery(linkedBattery.drainBattery(transferRate * Time.deltaTime));
        Debug.Log("Charging Player");
    }

    public void ChargeLinkedBattery() {
        //charges the linked battery by the amount that is able to be drained from the current player's battery
        linkedBattery.chargeBattery(CurrentPlayer.instance.currentPlayer.GetComponent<Battery>().drainBattery(transferRate * Time.deltaTime));
        Debug.Log("Draining Player");
    }
}
