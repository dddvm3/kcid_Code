using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Status_Num
{
    Normal = 0,
    Aircraft = 1,
    Boxer = 2,
    Bombthrower = 3,
    LightningMode = 4,
    Healer = 5,
    TimeLine = 6,
}

public class Player_Master : MonoBehaviour
{
    public static Player_Master Instnace;
    public Status_Num myStatus;
    public int status_Num;
    public bool Had_Status;
    public float Durability;
    public float InHealtime;
    public GameObject Healer_HandObject;
    public Animator Healer_Animator;
    public GameObject NormalMode;
    public GameObject AircraftMode;
    public bool Had_Object;
    public GameObject BoxerMode;

    public MonoBehaviour[] Normal_status;
    public MonoBehaviour[] Aircraft_status;
    public MonoBehaviour[] Boxer_status;
    public MonoBehaviour[] Bombthrower_status;
    public MonoBehaviour[] LightningMode_status;
    public MonoBehaviour[] Healer;

    public delegate void GeneralEventHandler();
    public event GeneralEventHandler EventHandsEmpty;
    public event GeneralEventHandler EventInventoryChanged;

    public delegate void GeneralStatusHandler();
    public event GeneralStatusHandler EventstatusChange;
    public event GeneralStatusHandler EventStatusDurable;

    public delegate void StatusEvntHandler(string StatusName);
    public event StatusEvntHandler EventPickedUpStatus;
    public event StatusEvntHandler EventThrowStatus;

    public delegate void PlayerStatusHandler();
    public event PlayerStatusHandler NormalStatus;
    public event PlayerStatusHandler AircraftStatus;
    public event PlayerStatusHandler BoxerStatus;

    public delegate void PlayerHealthEvent(int healthChange);
    public event PlayerHealthEvent EventPlayerHealthDeduction;
    public event PlayerHealthEvent EventPlayerHealthIncrease;
    void Awake()
    {
        Instnace = this;
    }

    private void OnEnable()
    {
        // myStatus = Status_Num.Normal;
        // status_Num = 0;
    }

    public void CallEventHandsEmpty()
    {
        if (EventHandsEmpty != null)
        {
            EventHandsEmpty();
        }
    }

    public void CallEventInventoryChanged()
    {
        if(EventInventoryChanged != null)
        {
            EventInventoryChanged();
        }
    }

    public void CallEventstatusChange()
    {
        if(EventstatusChange != null)
        {
            EventstatusChange();
        }
    }

    public void CallEventStatusDurable()
    {
        if(EventStatusDurable != null)
        {
            EventStatusDurable();
        }
    }

    public void CallNormalStatus()
    {
        if(NormalStatus != null)
        {
            NormalStatus();
        }
    }

    public void CallAircraftStatus()
    {
        if(AircraftStatus != null)
        {
            AircraftStatus();
        }
    }

    public void CallBoxerStatus()
    {
        if (BoxerStatus != null)
        {
            BoxerStatus();
        }
    }
    public void CallEventPickedUpStatus(string statusName)
    {
        if(EventPickedUpStatus != null)
        {
            EventPickedUpStatus(statusName);
        }
    }

    public void CallEventPlayerHealthDeduction(int dmg)
    {
        if(EventPlayerHealthDeduction != null)
        {
            EventPlayerHealthDeduction(dmg);
        }
    }

    public void CallEventPlayerHealthIncrease(int increase)
    {
        if(EventPlayerHealthIncrease != null)
        {
            EventPlayerHealthIncrease(increase);
        }
    }  
}
