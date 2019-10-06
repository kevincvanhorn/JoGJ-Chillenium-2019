using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAbility : MonoBehaviour
{
    public GameObject pillar;
    public EAbilityType abilityType = EAbilityType.EGreen;
    public float scaleAmount = 0.05f;
    public float maxScaleAmount = 20.0f;
    public int maxPillars = 3;


    private bool currentlyExpanding;
    private GameObject curBuildPillar;
    private List<GameObject> builtPillars;
    public delegate void OnPushDelegate();
    public static OnPushDelegate onPushDelegate; 

    public enum EAbilityType
    {
        EGreen,
        EPink,
        EBlue
    }


    // Start is called before the first frame update
    void Start()
    {
        currentlyExpanding = false;
        builtPillars = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(1) && currentlyExpanding && curBuildPillar.transform.localScale.y < maxScaleAmount)
        {
            curBuildPillar.transform.localScale += new Vector3(0, 0, scaleAmount);
        }
        else
        {
            currentlyExpanding = false;
            curBuildPillar = null;
        }
    }

    public void OnUseAbility(RaycastHit aimHit)
    {
        if (abilityType == EAbilityType.EGreen)
        {
            if (builtPillars.Count == maxPillars)
            {
                Destroy(builtPillars[0]);
                builtPillars.RemoveAt(0);
            }
            Debug.Log(aimHit.normal);
            if(aimHit.normal.y < 0.01f)
            {
                curBuildPillar = Instantiate(pillar, aimHit.point, Quaternion.LookRotation(aimHit.normal, Vector3.forward));
                //curBuildPillar.transform.parent = aimHit.transform;
            }
            else
            {
                curBuildPillar = Instantiate(pillar, aimHit.point, Quaternion.LookRotation(aimHit.normal, transform.forward));
                //curBuildPillar.transform.parent = aimHit.transform;
            }
            builtPillars.Add(curBuildPillar);
            currentlyExpanding = true;
        }
        else if (abilityType == EAbilityType.EPink)
        {
            Push();
        }
    }

    public void Push()
    {
        onPushDelegate();
    }
}
