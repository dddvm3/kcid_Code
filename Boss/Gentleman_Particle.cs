using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gentleman_Particle : MonoBehaviour
{
    public AudioSource Snap;
    public AudioSource Attack;
    public AudioSource Thunder;
    public GameObject Shoot;
    public GameObject CrowBar;
    public GameObject Summon;
    public GameObject HandPower;
    public GameObject TeleportInSmoke;
    public GameObject TeleportOutSmoke;
    void PlayShoot_Particle()
    {
        Shoot.SetActive(true);
    }
    void CloseShoot_Particle()
    {
        Shoot.SetActive(false);
    }
    void PalySummon_particle()
    {
        Snap.Play();
        StartCoroutine(GameSystem.DelayMethod(0.1f,()=>{
            Thunder.Play();
        }));
        Summon.SetActive(true);
    }
    void CloseSummon_Particle()
    {
        Summon.SetActive(false);
    }
    void PlayCrowBar()
    {
        CrowBar.SetActive(true);
    }
    public void AttackAudio()
    {        
        Attack.Play();
    }
    void CloseCrowBar()
    {
        CrowBar.SetActive(false);
    }
    void PlayHandPower()
    {
        HandPower.SetActive(true);
    }
    void CloseHandPower()
    {
        HandPower.SetActive(false);
    }
    void PlayTeleportSmoke()
    {
        GameObject Smoke =  Instantiate(TeleportInSmoke, transform);
        Smoke.transform.parent = null;
        Smoke.SetActive(true);
    }
    void PlayTeleportOutSmoke()
    {
        TeleportOutSmoke.SetActive(true);
    }
    void CloseTeleportOutSmoke()
    {
        TeleportOutSmoke.SetActive(false);
    }
}
