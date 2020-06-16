using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockeShootMaster : MonoBehaviour
{
    public delegate void GeneralLockeShootHandler();
    public event GeneralLockeShootHandler EventPlayerInput;

    public delegate void LockeShootPowerHandler(Vector3 hitPos, Transform hitTransform);
    public event LockeShootPowerHandler NormalLockeShootHitDefault;
    public event LockeShootPowerHandler NormalLockeShootHitEnemy;
    public event LockeShootPowerHandler PowerfulLockeShootHitDefault;
    public event LockeShootPowerHandler PowerfulLockeShootHitEnemy;

    public bool IsLockeShootLoaded;

    public void CallEventPlayerInput()
    {
        if(EventPlayerInput != null)
        {
            EventPlayerInput();
        }
    }

    public void CallNormalLockeShootHitDefault(Vector3 hPos, Transform hTarnsform)
    {
        if(NormalLockeShootHitDefault != null)
        {
            NormalLockeShootHitDefault(hPos, hTarnsform);
        }
    }

    public void CallNormalLockeShootHitEnemy(Vector3 hPos, Transform hTarnsform)
    {
        if (NormalLockeShootHitEnemy != null)
        {
            NormalLockeShootHitEnemy(hPos, hTarnsform);
        }
    }

    public void CallPowerfulLockeShootHitDefault(Vector3 hPos, Transform hTarnsform)
    {
        if (PowerfulLockeShootHitDefault != null)
        {
            PowerfulLockeShootHitDefault(hPos, hTarnsform);
        }
    }

    public void CallPowerfulLockeShootHitEnemy(Vector3 hPos, Transform hTarnsform)
    {
        if (PowerfulLockeShootHitEnemy != null)
        {
            PowerfulLockeShootHitEnemy(hPos, hTarnsform);
        }
    }
}
