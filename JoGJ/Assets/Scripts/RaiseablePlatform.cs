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
            //elapsed = 0;
            StartCoroutine(LerpLocation());
        }
    }

    private void Update()
    {
        elapsed += Time.deltaTime;
        if(elapsed < TimeToLerp)
        {
            Vector3.Lerp(this.transform.position, TargetLoc, elapsed / TimeToLerp);

        }
    }

    IEnumerator LerpLocation(){
        bool valid = true;
        if (Locations.Count < 2) valid = false;
        float time = 0;

        while(valid){
            time += 0.05f;
            this.transform.position = Vector3.Lerp(this.transform.position, TargetLoc, elapsed / TimeToLerp);
            if (Vector3.Distance(this.transform.position, TargetLoc) < 0.05f) valid = false;
            yield return new WaitForSeconds(0.05f);
        }

        yield return null;
    }
}
