using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_ChangeStatus : MonoBehaviour
{
    public static Player_ChangeStatus Instance;
    private Player_Master player_Master;
    private PlayerAttackDamage playerattackDamage;
    [SerializeField]
    private int judgement_StatusNum;


	void OnEnable()
	{
        SetInitialReferences();
        player_Master.NormalStatus += ChangeModetoNormal;
        player_Master.AircraftStatus += ChangeModetoAircraft;
        player_Master.BoxerStatus += ChangeModetoBoxer;
	}

    void OnDisable()
    {
        player_Master.NormalStatus -= ChangeModetoNormal;
        player_Master.AircraftStatus -= ChangeModetoAircraft;
        player_Master.BoxerStatus -= ChangeModetoBoxer;
    }
    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        ChangeStatus();
    }

    void SetInitialReferences()
	{
        playerattackDamage = GetComponent<PlayerAttackDamage>();
        player_Master = GetComponent<Player_Master>();
        judgement_StatusNum = 0;
	}

    void ChangeStatus()
    {
        if (judgement_StatusNum != player_Master.status_Num)
        {
            StatusChange();
            judgement_StatusNum = player_Master.status_Num;
            playerattackDamage.now_StatusNum = player_Master.status_Num;
        }
    }

    void ChangeModetoNormal()
    {
        // if (player_Master.status_Num == 5)
        // {
        // if(player_Master.Healer_HandObject)
        // {
        // }
        // if(player_Master.Healer_Animator)
        // {
        // }
    // }
        player_Master.NormalMode.SetActive(true);
        player_Master.AircraftMode.SetActive(false);
        player_Master.BoxerMode.SetActive(false);
        
        HealClose();
    }
    public void HealClose()
    {
        Debug.Log("close");
        if(player_Master.Healer_HandObject)
        {
            player_Master.Healer_HandObject.SetActive(false);
        }
        if(player_Master.Healer_Animator)
        {
            player_Master.Healer_Animator.Rebind();
            player_Master.Healer_Animator.enabled = false;
        }
    }

    void ChangeModetoAircraft()
    {
        player_Master.NormalMode.SetActive(false);
        player_Master.AircraftMode.SetActive(true);
    }

    void ChangeModetoBoxer()
    {
        player_Master.NormalMode.SetActive(false);
        player_Master.BoxerMode.SetActive(true);
    }

    void ChangeModetoHealer()
    {
        player_Master.NormalMode.SetActive(false);
        player_Master.Healer_HandObject.SetActive(true);
        player_Master.Healer_Animator.enabled = true;
    }

    void ChangeModeInTimeLine()
    {
        player_Master.NormalMode.SetActive(false);
        player_Master.AircraftMode.SetActive(false);
        player_Master.BoxerMode.SetActive(false);
        // player_Master.Healer_HandObject.SetActive(false);
        // player_Master.Healer_Animator.Rebind();
        // player_Master.Healer_Animator.enabled = false;
    }

    void StatusChange()
    {
        switch (player_Master.status_Num)
        {
            case 0:
                player_Master.myStatus = Status_Num.Normal;
                player_Master.Had_Status = false;

                player_Master.CallNormalStatus();

                for (int i = 0; i < player_Master.Normal_status.Length; i++)
                {
                    player_Master.Normal_status[i].enabled = true;
                }
                for (int i = 0; i < player_Master.Aircraft_status.Length; i++)
                {
                    player_Master.Aircraft_status[i].enabled = false;
                }

                for (int i = 0; i < player_Master.Boxer_status.Length; i++)
                {
                    player_Master.Boxer_status[i].enabled = false;
                }

                for (int i = 0; i < player_Master.Bombthrower_status.Length; i++)
                {
                    player_Master.Bombthrower_status[i].enabled = false;
                }

                for (int i = 0; i < player_Master.LightningMode_status.Length; i++)
                {
                    player_Master.LightningMode_status[i].enabled = false;
                }

                for (int i = 0; i < player_Master.Healer.Length; i++)
                {
                    player_Master.Healer[i].enabled = false;
                }
                break;
            case 1:
                player_Master.myStatus = Status_Num.Aircraft;
                player_Master.Had_Status = true;

                player_Master.CallAircraftStatus();

                for (int i = 0; i < player_Master.Aircraft_status.Length; i++)
                {
                    player_Master.Aircraft_status[i].enabled = true;
                }

                for (int i = 0; i < player_Master.Normal_status.Length; i++)
                {
                    player_Master.Normal_status[i].enabled = false;
                }

                for (int i = 0; i < player_Master.Boxer_status.Length; i++)
                {
                    player_Master.Boxer_status[i].enabled = false;
                }

                for (int i = 0; i < player_Master.Bombthrower_status.Length; i++)
                {
                    player_Master.Bombthrower_status[i].enabled = false;
                }

                for (int i = 0; i < player_Master.LightningMode_status.Length; i++)
                {
                    player_Master.LightningMode_status[i].enabled = false;
                }

                for (int i = 0; i < player_Master.Healer.Length; i++)
                {
                    player_Master.Healer[i].enabled = false;
                }
                break;
            case 2:
                player_Master.myStatus = Status_Num.Boxer;
                player_Master.Had_Status = true;

                player_Master.CallBoxerStatus();

                for (int i = 0; i < player_Master.Normal_status.Length; i++)
                {
                    player_Master.Normal_status[i].enabled = false;
                }

                for (int i = 0; i < player_Master.Aircraft_status.Length; i++)
                {
                    player_Master.Aircraft_status[i].enabled = false;
                }

                for (int i = 0; i < player_Master.Boxer_status.Length; i++)
                {
                    player_Master.Boxer_status[i].enabled = true;
                }

                for (int i = 0; i < player_Master.Bombthrower_status.Length; i++)
                {
                    player_Master.Bombthrower_status[i].enabled = false;
                }

                for (int i = 0; i < player_Master.LightningMode_status.Length; i++)
                {
                    player_Master.LightningMode_status[i].enabled = false;
                }

                for (int i = 0; i < player_Master.Healer.Length; i++)
                {
                    player_Master.Healer[i].enabled = false;
                }
                break;
            case 3:
                player_Master.myStatus = Status_Num.Bombthrower;
                player_Master.Had_Status = true;

                for (int i = 0; i < player_Master.Normal_status.Length; i++)
                {
                    player_Master.Normal_status[i].enabled = false;
                }

                for (int i = 0; i < player_Master.Aircraft_status.Length; i++)
                {
                    player_Master.Aircraft_status[i].enabled = false;
                }

                for (int i = 0; i < player_Master.Boxer_status.Length; i++)
                {
                    player_Master.Boxer_status[i].enabled = false;
                }

                for (int i = 0; i < player_Master.Bombthrower_status.Length; i++)
                {
                    player_Master.Bombthrower_status[i].enabled = true;
                }

                for (int i = 0; i < player_Master.LightningMode_status.Length; i++)
                {
                    player_Master.LightningMode_status[i].enabled = false;
                }

                for (int i = 0; i < player_Master.Healer.Length; i++)
                {
                    player_Master.Healer[i].enabled = false;
                }
                break;

            case 4:
                player_Master.myStatus = Status_Num.Bombthrower;
                player_Master.Had_Status = true;

                for (int i = 0; i < player_Master.Normal_status.Length; i++)
                {
                    player_Master.Normal_status[i].enabled = false;
                }

                for (int i = 0; i < player_Master.Aircraft_status.Length; i++)
                {
                    player_Master.Aircraft_status[i].enabled = false;
                }

                for (int i = 0; i < player_Master.Boxer_status.Length; i++)
                {
                    player_Master.Boxer_status[i].enabled = false;
                }

                for (int i = 0; i < player_Master.Bombthrower_status.Length; i++)
                {
                    player_Master.Bombthrower_status[i].enabled = false;
                }

                for (int i = 0; i < player_Master.LightningMode_status.Length; i++)
                {
                    player_Master.LightningMode_status[i].enabled = true;
                }

                for (int i = 0; i < player_Master.Healer.Length; i++)
                {
                    player_Master.Healer[i].enabled = false;
                }
                break;

            case 5:
                player_Master.myStatus = Status_Num.Healer;
                player_Master.Had_Status = true;
                ChangeModetoHealer();

                for (int i = 0; i < player_Master.Normal_status.Length; i++)
                {
                    player_Master.Normal_status[i].enabled = false;
                }

                for (int i = 0; i < player_Master.Aircraft_status.Length; i++)
                {
                    player_Master.Aircraft_status[i].enabled = false;
                }

                for (int i = 0; i < player_Master.Boxer_status.Length; i++)
                {
                    player_Master.Boxer_status[i].enabled = false;
                }

                for (int i = 0; i < player_Master.Bombthrower_status.Length; i++)
                {
                    player_Master.Bombthrower_status[i].enabled = false;
                }

                for (int i = 0; i < player_Master.LightningMode_status.Length; i++)
                {
                    player_Master.LightningMode_status[i].enabled = false;
                }

                for (int i = 0; i < player_Master.Healer.Length; i++)
                {
                    player_Master.Healer[i].enabled = true;
                }
                break;
            case 6:
                player_Master.myStatus = Status_Num.TimeLine;
                player_Master.Had_Status = true;
                // ChangeModeInTimeLine();
                Debug.Log("TimelineTest");
                for (int i = 0; i < player_Master.Normal_status.Length; i++)
                {
                    player_Master.Normal_status[i].enabled = false;
                }

                for (int i = 0; i < player_Master.Aircraft_status.Length; i++)
                {
                    player_Master.Aircraft_status[i].enabled = false;
                }

                for (int i = 0; i < player_Master.Boxer_status.Length; i++)
                {
                    player_Master.Boxer_status[i].enabled = false;
                }

                for (int i = 0; i < player_Master.Bombthrower_status.Length; i++)
                {
                    player_Master.Bombthrower_status[i].enabled = false;
                }

                for (int i = 0; i < player_Master.LightningMode_status.Length; i++)
                {
                    player_Master.LightningMode_status[i].enabled = false;
                }

                for (int i = 0; i < player_Master.Healer.Length; i++)
                {
                    player_Master.Healer[i].enabled = false;
                }
                break;
        }
    }
}