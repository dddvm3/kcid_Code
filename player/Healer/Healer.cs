using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Healer : MonoBehaviour
{
    private Player_Master player_Master;
    private Player_DetectStatus player_DetectStatus;
    [Header("回一格血時間")]
    public float Healtime;
    public float InHealtime;
    private Collider HandObject_Col;
    private bool Onetime;
    public bool Hurt;

    void OnEnable()
    {
        SetInitialReferences();
    }
    void OnDisable() 
    {
        HandObject_Col = null;
    }
    void Update()
    {
        if (HpinMax())
        {
            // GameSystem.DelayMethod(0.5f,()=>{
            if(player_Master.Healer_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime%1 >0.95f)
            {
                player_DetectStatus.HealerThrowout();
            }
            // });
            
        }
        if(Hurt)
        {
            player_DetectStatus.HealerThrowout();
            Hurt = false;
        }
    }

    void SetInitialReferences()
    {
        player_Master = GetComponent<Player_Master>();
        player_DetectStatus = GetComponent<Player_DetectStatus>();
        HandObject_Col = player_Master.Healer_HandObject.GetComponent<Collider>();
    }

    public IEnumerator DelayMethod(float waitSec, Action action)
    {
        yield return new WaitForSeconds(waitSec);
        action();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (HandObject_Col != null)
        {
            if(HandObject_Col.tag == "Bullet")
            {
                Hurt = true;
            }   
        }
    }
    public bool HpinMax()
    {
        return Property_Class.m.NowHp == Property_Class.m.Max_Hp;
    }
}