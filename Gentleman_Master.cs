using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gentleman_Master : MonoBehaviour
{
    public AudioSource SmokeBoom;
    public bool SaluteShoot;
    public bool Can_Teleport;
    public delegate void GeneralGentlemanHandller();
    public event GeneralGentlemanHandller EventSummonBombMoster;
    public event GeneralGentlemanHandller EventGetlemanShoot;
    public event GeneralGentlemanHandller EventSwingstick;
    public event GeneralGentlemanHandller EventBeforeTeleport;
    public event GeneralGentlemanHandller EventTeleport;
    public event GeneralGentlemanHandller EventAfterTeleport;
    public event GeneralGentlemanHandller EventGentlemanDie;
    public delegate void HealthEventHandler(int Health);
    public event HealthEventHandler EventEnemyDeductHealth;
    
    public void CallSummonBombMoster()
    {
        if(EventSummonBombMoster != null)
        {
            EventSummonBombMoster();
        }
    }
    public void CallEventGetlemanShoot()
    {
        if (EventGetlemanShoot != null)
        {
            EventGetlemanShoot();
        }
    }
    public void CallSwingstick()
    {
        if(EventSwingstick != null)
        {
            EventSwingstick();
        }
    }
    public void CallBeforeTeleport()
    {
        SmokeBoom.Play();
        if(EventBeforeTeleport != null)
        {
            EventBeforeTeleport();
        }
    }
    public void CallTeleport()
    {
        if(EventTeleport != null)
        {
            EventTeleport();
        }
    }
    public void CallAfterTeleport()
    {
        if(EventAfterTeleport != null)
        {
            EventAfterTeleport();
        }
    }
    public void CallGentlemanDie()
    {
        if(EventGentlemanDie != null)
        {
            EventGentlemanDie();
        }
    }

    public void CallEventEnemyDeductHealth(int Health)
    {
        if(EventEnemyDeductHealth != null)
        {
            EventEnemyDeductHealth(Health);
        }
    }
}
