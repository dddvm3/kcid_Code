using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aircraft_Animation : MonoBehaviour
{
    private Aircraft_AniMaster aircraft_AniMaster;
    private Animator Ani;
    
    void OnEnable()
    {
        SetInitialReferences();
        aircraft_AniMaster.EventMove += Move;
        aircraft_AniMaster.EventLeaveMove += LeaveMove;
        aircraft_AniMaster.EventFlying += Flying;
        aircraft_AniMaster.EventAttack += Attack;
        aircraft_AniMaster.EventHitted += Hitted;
        aircraft_AniMaster.EventLeaveFly += LeaveFly;
        aircraft_AniMaster.EventMoveStop += MoveStop;
        aircraft_AniMaster.EventLeaveMoveStop += LeaveMoveStop;
        aircraft_AniMaster.EventDead += Dead;
    }
    void OnDisable()
    {
        aircraft_AniMaster.EventMove -= Move;
        aircraft_AniMaster.EventLeaveMove -= LeaveMove;
        aircraft_AniMaster.EventFlying -= Flying;
        aircraft_AniMaster.EventAttack -= Attack;
        aircraft_AniMaster.EventHitted -= Hitted;
        aircraft_AniMaster.EventLeaveFly -= LeaveFly;
        aircraft_AniMaster.EventMoveStop -= MoveStop;
        aircraft_AniMaster.EventLeaveMoveStop -= LeaveMoveStop;
        aircraft_AniMaster.EventDead -= Dead;
    }
    void SetInitialReferences()
    {
        aircraft_AniMaster = GetComponent<Aircraft_AniMaster>();
        Ani = GetComponent<Animator>();
    }
    void Dead()
    {
        Ani.SetBool("Dead",true);
    }
    void Move()
    {
        Ani.SetBool("Idle", false);
    }
    void LeaveMove()
    {
        Ani.SetBool("Idle", true);
    }
    void MoveStop()
    {
        Ani.SetBool("RunStop",true);
    }
    void LeaveMoveStop()
    {
        Ani.SetBool("RunStop", false);
    }
    void Flying()
    {
        Ani.SetBool("Flying", true);
    }
    void LeaveFly()
    {
        Ani.SetBool("Flying", false);
    }
    void Attack()
    {
        Ani.SetTrigger("Attack");
    }
    void Hitted()
    {
        Debug.Log("hit");
        Ani.SetTrigger("Hitted");
    }
}
