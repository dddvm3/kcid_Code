using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gentleman_LookRotation: MonoBehaviour
{
    private Gentleman_Master gentleman_Master;
    [Header("玩家")]
    public Transform Player;
    
    void OnEnable() 
    {
        SetInitialReferences();
    }
    void OnDisable() 
    {
        
    }
    void SetInitialReferences()
    {
        gentleman_Master = GetComponent<Gentleman_Master>();
    }
    void Update() 
    {
        if(!gentleman_Master.SaluteShoot)
        {
            Vector3 pos = new Vector3(Player.position.x,transform.position.y, Player.position.z);
            transform.LookAt(pos);
        }
    }
}
