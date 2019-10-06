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
    private List<GameObject> pillarParents;
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
        pillarParents = new List<GameObject>();
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
                pillarParents.RemoveAt(0);
                /*for(int pillars = 1; pillars < maxPillars; pillars++)
                {
                    if(pillarParents == null)
                    {
                        Destroy(builtPillars[pillars]);
                        builtPillars.RemoveAt(pillars);
                        pillarParents.RemoveAt(pillars);
                    }
                }*/
                Debug.Log(pillarParents[0] + " " + pillarParents[1]);
            }
            Debug.Log(aimHit.normal);
            if(aimHit.normal.y < 0.01f)
            {
                curBuildPillar = Instantiate(pillar, aimHit.point, Quaternion.LookRotation(aimHit.normal, Vector3.forward));
            }
            else
            {
                curBuildPillar = Instantiate(pillar, aimHit.point, Quaternion.LookRotation(aimHit.normal,
                    Quaternion.AngleAxis(200, Vector3.up) * Vector3.one));
            }
            builtPillars.Add(curBuildPillar);
            pillarParents.Add(aimHit.transform.gameObject);
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
