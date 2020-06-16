using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockeShoot_Standard : MonoBehaviour
{
    private LockeShootMaster lockeShootMaster;
    private float nextAttack;
    private Transform mytransform;
    private float Oncharge;
    private float ChargeRange;
    [Tooltip("集氣時間")]
    public float PowerRange = 1.5f;
    public GameObject Effect,TwoStepEffect,Shoot;


    private void Start()
    {
        SetInitialReferences();
    }

    private void Update()
    {
        CheckPowerRange();
    }

    void SetInitialReferences()
    {
        lockeShootMaster = GetComponent<LockeShootMaster>();
        mytransform = transform;
        lockeShootMaster.IsLockeShootLoaded = true;
    }



    void CheckPowerRange()
    {
        if (Input.GetButtonDown((Input_Collection.m.coll_list.Find(x => x.FeatureName == "Attack").ButtonName)))
        {
            Oncharge = Time.time;
        }
        if (Input.GetButton((Input_Collection.m.coll_list.Find(x => x.FeatureName == "Attack").ButtonName)))
        {
            ChargeRange = Time.time - Oncharge;
            if(ChargeRange >= PowerRange)
            {
            }
        }
        if (Input.GetButtonUp((Input_Collection.m.coll_list.Find(x => x.FeatureName == "Attack").ButtonName)))
        {
            if (ChargeRange >= PowerRange)
            {
                lockeShootMaster.CallEventPlayerInput();
            }
            else
            {
                lockeShootMaster.CallEventPlayerInput();
            }
        }
    }
}
