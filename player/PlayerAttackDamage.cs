using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackDamage : MonoBehaviour
{
    private int judgement_StatusNum;
    public int now_StatusNum;
    [Header("一般模式攻擊傷害")]
    public int NormalMode_Damage;
    [Header("拳擊模式攻擊傷害")]
    public int PunchMode_Damage;
    [Header("飛行器模式攻擊傷害")]
    public int AircraftMode_Damage;

    public int Now_Damage;

    void Update()
    {
        if (judgement_StatusNum != now_StatusNum)
        {
            judgement_StatusNum = now_StatusNum;
            DamageChange();
        }
    }

    void DamageChange()
    {
        switch(now_StatusNum)
        {
            case 0:
                Now_Damage = NormalMode_Damage;
                break;
            case 1:
                Now_Damage = PunchMode_Damage;
                break;
            case 2:
                Now_Damage = AircraftMode_Damage;
                break;
        }
    }
}