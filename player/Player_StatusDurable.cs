using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_StatusDurable : MonoBehaviour
{
    private Player_Master player_Master;

    private int judgement_Status;
    private int Status_Num;
    public bool startreduce;

    [Tooltip("飛行器耐久度減少值")]
    public float Aircraft_SubtractDurability;
    [Tooltip("拳擊手久度減少值")]
    public float Boxer_SubtractDurability;

    void OnEnable()
	{
        SetInitialReferences();
	}

	void OnDisable()
	{

	}

	void Start ()
	{
	
	}
	
	void Update ()
	{       
        ChangeStatusNum();
		if(judgement_Status !=0)
        {
            if (startreduce)
            {
                Whichstatusforjudge(Status_Num);
            }
        }
	}
	
	void SetInitialReferences()
	{
        player_Master = GetComponent<Player_Master>();
        judgement_Status = 0;
	}

    void ChangeStatusNum()
    {
        if(judgement_Status != player_Master.status_Num)
        {
            Status_Num = player_Master.status_Num;
        }
    }

    void Whichstatusforjudge(int Status_Num)
    {
        switch(Status_Num)
        {
            case 0:
                break;
            case 1:
                player_Master.Durability -= Aircraft_SubtractDurability;
                break;
            case 2:
                break;
        }
    }
}