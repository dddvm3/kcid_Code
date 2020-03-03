using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status_HealerHand : MonoBehaviour
{
    private StatusMaster statusMaster;
    public GameObject HandObject;
    public GameObject HealthParticle;
    public AudioSource healaudio;
    void OnEnable() 
    {
        SetInitialReferences();
        SetHandObject();
    }
    void SetInitialReferences()
    {
        statusMaster = GetComponent<StatusMaster>();
    }
    void SetHandObject()
    {
        statusMaster.HandObject = HandObject;
    }
    void HealtoHp()
    {
        Property_Class.m.NowHp += 1;
    }
    void HealtoHPStart()
    {
        
        healaudio.Play();
    }
    void PlayHealing()
    {
        HealthParticle.SetActive(true);
    }
    void ColseHealing()
    {
        // HandObject.SetActive(false);
        HealthParticle.SetActive(false);
    }
}
