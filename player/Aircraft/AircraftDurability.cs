using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AircraftDurability : MonoBehaviour
{
    private Player_Master player_Master;
    private LockeShootMaster lockeShootMaster;

    [Tooltip("耐久度減少值")]
    public float SubtractDurability;

    void OnEnable()
	{
        SetInitialReferences();
        lockeShootMaster.EventPlayerInput += SubtratDurability;
	}

	void OnDisable()
	{
        lockeShootMaster.EventPlayerInput -= SubtratDurability;
	}	
	
	void SetInitialReferences()
	{
        lockeShootMaster = GetComponent<LockeShootMaster>();
        player_Master = GetComponent<Player_Master>();
	}

    void SubtratDurability()
    {
        player_Master.Durability -= SubtractDurability;
    }
}