using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPowerSource
{
    float getCurrentAmount();
    float drainPower(float amount);
}

public interface IPoweredDevice
{
    bool Powered { get; set; }

    float powerNeeded(); //returns power needed per second
}

public class Circuit : MonoBehaviour
{
    public List<GameObject> firstSegment;
    private List<IPowerSource> firstSegment_Power;
    private List<IPoweredDevice> firstSegment_Device;
    private float firstDeviceCost;
    public List<GameObject> secondSegment;
    private List<IPowerSource> secondSegment_Power;
    private List<IPoweredDevice> secondSegment_Device;
    private float secondDeviceCost;
    private List<IPowerSource> mergedPowerList;
    private List<IPoweredDevice> mergedDeviceList;
    private float mergedDeviceCost;
    private bool connected = false;

    //the triggers will set the connected variable in onTriggerEnter/Exit

    void Start() {
        //the power and device lists for the two segments
        DivideList(firstSegment, firstSegment_Power, firstSegment_Device);
        DivideList(firstSegment, firstSegment_Power, firstSegment_Device);

        //calculate device costs
        firstDeviceCost = 0f;
        foreach(var device in firstSegment_Device) {
            firstDeviceCost += device.powerNeeded();
        }

        secondDeviceCost = 0f;
        foreach (var device in secondSegment_Device)
        {
            secondDeviceCost += device.powerNeeded();
        }

        //calculate merged costs and list
        List<IPoweredDevice> mergedDeviceList = new List<IPoweredDevice>(firstSegment_Device);
        mergedDeviceList.AddRange(secondSegment_Device);
        mergedDeviceCost = 0f;
        foreach (var item in mergedDeviceList)
        {
            mergedDeviceCost += item.powerNeeded();
        }

        List<IPowerSource> mergedPowerList = new List<IPowerSource>(firstSegment_Power);
        mergedPowerList.AddRange(secondSegment_Power);
    }

    void Update() {
        //in update, we need to apply any power costs
        powerDevices();
    }

    public void setConnected(bool value) {
        connected = value;
        Debug.Log("Connected = " + connected);
    }

    public bool getConnected() {
        return connected;
    }

    //look through all of the array elements and add up 
    private void DivideList(List<GameObject> mainList, List<IPowerSource> powerList, List<IPoweredDevice> deviceList) {
        foreach (var item in mainList)
        {
            IPowerSource p = item.GetComponent<IPowerSource>();
            if(p != null) {
                powerList.Add(p);
            }
            
            IPoweredDevice s = item.GetComponent<IPoweredDevice>();
            if(s != null) {
                deviceList.Add(s);
            }
        }
    }

    //traverse the list, accumulating the cost and power available
    //if the cost can be paid, decrement the power and set the devices to powered
    public void powerDevices() {

        float totalPower_first = 0f;
        foreach (var item in firstSegment_Power)
        {
            totalPower_first += item.getCurrentAmount();
        }

        float totalPower_second = 0f;
        foreach (var item in secondSegment_Power)
        {
            totalPower_second += item.getCurrentAmount();
        }

        if(connected) {

            //add up the powers and the costs
            float totalPower = totalPower_first + totalPower_second;
            float totalCost = firstDeviceCost + secondDeviceCost;

            if(totalCost > totalPower) return; //don't power anything

            foreach (var item in mergedDeviceList)
            {
                    item.Powered = true;
            }

            if(totalPower < float.MaxValue / 100f) { //use this as the check for infinite power source
                ApplyCost(totalCost, totalPower, mergedPowerList);
            }
        } //end of connected
        else {//perform calculations on each list individually
            //first segment
            if(totalPower_first > firstDeviceCost) { //go ahead and power the first segment
                foreach (var item in firstSegment_Device)
                {
                    item.Powered = true;
                }

                if(totalPower_first < float.MaxValue / 100f) {
                    ApplyCost(firstDeviceCost, totalPower_first, firstSegment_Power);
                }
            }

            //second segment
            if(totalPower_second > secondDeviceCost) { //go ahead and power the second segment
                foreach (var item in secondSegment_Device)
                {
                    item.Powered = true;
                }

                if(totalPower_second < float.MaxValue / 100f) {
                    ApplyCost(secondDeviceCost, totalPower_second, secondSegment_Power);
                }
            }
        }
    }

    //Takes summed up power and cost and applies the cost to the power
    private void ApplyCost(float cost, float power, List<IPowerSource> powerList) {

        //distribute the cost among the power sources
        float distributedCost = cost / powerList.Count;
        //changing from per second cost to cost for this delta time
        distributedCost *= Time.deltaTime;
        cost *= Time.deltaTime;
        while(cost > 0f) {
            foreach (var item in powerList)
            {
                cost -= item.drainPower(distributedCost); //subtract as much of the distributed cost as you can
            }
        }
    }
}
