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
    private Queue<GameObject> builtPillars;

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
        builtPillars = new Queue<GameObject>();
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
        if(builtPillars.Count == maxPillars)
        {
            Destroy(builtPillars.Dequeue());
        }
        curBuildPillar = Instantiate(pillar, aimHit.point, Quaternion.LookRotation(aimHit.normal, Vector3.forward));
        builtPillars.Enqueue(curBuildPillar);
        currentlyExpanding = true;
    }
}
