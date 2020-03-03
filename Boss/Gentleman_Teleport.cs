using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gentleman_Teleport : MonoBehaviour
{
    private Gentleman_Master gentleman_Master;
    private Gentleman_Ani ani;
    [Header("移動端點")]
    public Transform[] Teleport_Point;
    [Header("Boss模型")]
    public GameObject[] mybody;
    private int teleportCount;
    private int Copy_teleportCount;
    void OnEnable()
    {
        SetInitialReferences();
        gentleman_Master.EventBeforeTeleport += SetGentlemanInvisible;
        gentleman_Master.EventTeleport += Teleport;
        gentleman_Master.EventAfterTeleport += AddteleportCount;
        gentleman_Master.EventAfterTeleport += SetGentlemanShowUp;
    }
    
    void OnDisable()
    {
        gentleman_Master.EventBeforeTeleport -= SetGentlemanInvisible;
        gentleman_Master.EventTeleport -= Teleport;
        gentleman_Master.EventAfterTeleport -= AddteleportCount;
        gentleman_Master.EventAfterTeleport -= SetGentlemanShowUp;
    }
    void SetInitialReferences()
    {
        gentleman_Master = GetComponent<Gentleman_Master>();
        AddteleportCount();
    }
    void Update() 
    {
        // if(!gentleman_Master.Can_Teleport)
        // {
        //     if (CheckTeleportCD())
        //     {
        //         ani.PlayTeleport();
        //         NextTeleportTime = Time.time + Teleport_CD;
        //     }
        // }
        // else
        // {
        //     NextTeleportTime = Time.time + Teleport_CD;
        // }
    }
    void SetGentlemanInvisible()
    {
        for (int i = 0; i < mybody.Length; i++)
        {
            mybody[i].SetActive(false);
        }
    }

    void  SetGentlemanShowUp()
    {
        for (int i = 0; i < mybody.Length; i++)
        {
            mybody[i].SetActive(true);
        }
    }
    void Teleport()
    {
        if(Copy_teleportCount == teleportCount)
        {   
            AddteleportCount();
        }
        else
        {
            transform.position = Teleport_Point[teleportCount].position;
            Copy_teleportCount = teleportCount;
        }
    }
    void AddteleportCount()
    {
        teleportCount = Random.Range(0,Teleport_Point.Length - 1);
    }
}
