using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaiseablePlatform : MonoBehaviour
{
    public List<Vector3> Locations;


    private int locationIndex = 0;
    private Vector3 TargetLoc;

    public void SwitchLocation()
    {
        locationIndex++;

        if (locationIndex >= Locations.Count)
        {
            locationIndex = 0;
        }
        if (locationIndex < Locations.Count)
        {
            TargetLoc = Locations[locationIndex];
        }
    }

    IEnumerator LerpLocation(){
        bool valid = true;
        if (Locations.Count < 2) valid = false;

        while(valid){
            Vector3.Lerp(this.transform.position, TargetLoc, 0.05f);
            if (Vector3.Distance(this.transform.position, TargetLoc) < 0.05f) valid = false;
            yield return new WaitForSeconds(0.05f);
        }
    }
}
