using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aircraft_AniMaster : MonoBehaviour
{
    public static Aircraft_AniMaster Instance;
    public delegate void AircraftAnimationHandler();
    public event AircraftAnimationHandler EventIdle;
    public event AircraftAnimationHandler EventMove;
    public event AircraftAnimationHandler EventLeaveMove;
    public event AircraftAnimationHandler EventMoveStop;
    public event AircraftAnimationHandler EventLeaveMoveStop;
    public event AircraftAnimationHandler EventLeaveFly;
    public event AircraftAnimationHandler EventFlying;
    public event AircraftAnimationHandler EventLanding;
    public event AircraftAnimationHandler EventCatch;
    public event AircraftAnimationHandler EventAttack;
    public event AircraftAnimationHandler EventHitted;
    public event AircraftAnimationHandler EventDead;
    void Awake()
    {
        Instance = this;
    }
    public void CallEventDead()
    {
        if(EventDead != null)
        {
            EventDead();
        }
    }
    public void CallEventBeHitted()
    {
        if(EventHitted != null)
        {
            EventHitted();
        }
    }
    public void CallEventIdle()
    {
        if(EventIdle != null)
        {
            EventIdle();
        }
    }
    public void CallEventMove()
    {
        if(EventMove != null)
        {
            EventMove();
        }
    }
    public void CallEventAttack()
    {
        if(EventAttack!=null)
        {
            EventAttack();
        }
    }
    public void CallEventLeaveMove()
    {
        if(EventLeaveMove != null)
        {
            EventLeaveMove();
        }
    }
    public void CallEventMoveStop()
    {
        if(EventMoveStop != null)
        {
            EventMoveStop();
        }
    }
    public void CallEventLeaveMoveStop()
    {
        if(EventLeaveMoveStop != null)
        {
            EventLeaveMoveStop();
        }
    }
    public void CallEventFlying()
    {
        if(EventFlying != null)
        {
            EventFlying();
        }
    }
    public void CallEventLeaveFly()
    {
        if(EventLeaveFly != null)
        {
            EventLeaveFly();
        }
    }
    public void CallEventLanding()
    {
        if(EventLanding != null)
        {
            EventLanding();
        }
    }
    public void CallEventCatch()
    {
        if(EventCatch != null)
        {
            EventCatch();
        }
    }
}
