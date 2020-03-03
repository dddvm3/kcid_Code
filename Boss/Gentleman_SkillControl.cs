using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gentleman_SkillControl : MonoBehaviour
{
    private Gentleman_Master gentleman_Master;
    private Gentleman_Ani gentleman_Ani;
    [Header("K-cid")]
    public Transform Player;
    [Header("什麼距離揮棍")]
    public float DisPlayer;
    [Header("揮棍ＣＤ")]
    public float SwingstickCD;
    [Header("瞬移ＣＤ")]
    public float TeleportCD;
    [Header("召喚ＣＤ")]
    public float SummonCD;
    
    private float DoSummonTime;
    private float DoSwingStickTime;
    private float DoTeleportTime;
    void OnEnable() 
    {
        SetInitialReferences();
    }
    void SetInitialReferences()
    {
        gentleman_Master = GetComponent<Gentleman_Master>();
        gentleman_Ani = GetComponent<Gentleman_Ani>();
    }

    void Update() 
    {
        if(!gentleman_Master.Can_Teleport)
        {
            if (CheckTeleportCD())
            {
                gentleman_Ani.PlayTeleport();
                DoTeleportTime = Time.time;
                DoSwingStickTime = 0;
            }
            else
            {
                if (CheckSwingstickCD() && CheckDistanceplayer())
                {
                    gentleman_Ani.PlaySwingStick();
                    DoSwingStickTime = Time.time;
                }
                else if (CheckSummonCD())
                {
                    gentleman_Ani.PlaySummon();
                    DoSummonTime = Time.time;
                }
            }
        }
        else
        {
            DoSwingStickTime = 0;
            DoTeleportTime = Time.time;
        }
    }
    bool CheckDistanceplayer()
    {
        return Vector3.Distance(Player.position, transform.position) < DisPlayer;
    }
    bool CheckTeleportCD()
    {
        return Time.time - DoTeleportTime > TeleportCD;
    }
    bool CheckSummonCD()
    {
        return Time.time - DoSummonTime > SummonCD;
    }
    bool CheckSwingstickCD()
    {
        return Time.time - DoSwingStickTime > SwingstickCD;
    }
    void ResetSwingCD()
    {
        DoSwingStickTime = Time.time;
    }
}