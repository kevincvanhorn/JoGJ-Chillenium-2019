using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaiseablePlatform : MonoBehaviour
{
    public List<Vector3> Locations;
    public float TimeToLerp;

    private int locationIndex = 0;
    private Vector3 TargetLoc;
    private float elapsed;

    private void Start()
    {
        elapsed = TimeToLerp;
    }
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
            elapsed = 0;
        }
    }

    private void FixedUpdate()
    {
        elapsed += Time.deltaTime;
        if(elapsed <= TimeToLerp)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, TargetLoc, elapsed / TimeToLerp);
        }
    }
}
