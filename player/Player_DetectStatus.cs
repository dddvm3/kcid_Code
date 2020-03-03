using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_DetectStatus : MonoBehaviour
{
    public static Player_DetectStatus Instance;

    [Header("什麼圖層用於狀態")]
    public LayerMask LayerToDetect;
    [Header("射線從哪射出")]
    public Transform rayTransformPivot;
    [Header("撿取")]
    public string ButtonPickUp;

    private Player_Master player_Master;
    public Transform StatusAvailableForPickUp;
    private RaycastHit hit;
    [Header("檢測跟撿取距離")]
    public float DetectRange;
    [Header("檢測角度")]
    public float DetectAngle;   
    private bool StatusInRange;
    [Header("每秒選轉角度")]
    public float rotatePerSecond;
    private bool haveTarget;
    private GameObject target;

    private float labelWidth = 200;
    private float labelHeight = 50;
    private int pickupCheckNum;
    private Collider[] colliders;

    public bool confirmownershipStatus;
    private Vector3 throwDirection;
    public float throwForce;

    void OnEnable()
    {
        SetInitialReferences();
    }
    void Awake()
    {
        Instance = this;
    }
	void Update ()
	{
        CastRayForDetectingStatus();
        // CheckForStatusPickupAttempt();
    }

    void SetInitialReferences()
    {
        player_Master = GetComponent<Player_Master>();
    }

    void CastRayForDetectingStatus()
    {
        if (!confirmownershipStatus)
        {
            if (haveTarget)
            {
                Vector3 targetpos = target.transform.position - rayTransformPivot.position;
                if (LookAround(Quaternion.FromToRotation(rayTransformPivot.forward, targetpos), Color.red))
                {
                    StatusAvailableForPickUp = hit.transform;
                    player_Master.Healer_HandObject = StatusAvailableForPickUp.GetComponent<StatusMaster>().HandObject;
                    player_Master.Healer_Animator = StatusAvailableForPickUp.GetComponent<Animator>();
                    StatusInRange = true;
                }
                else
                {
                    StatusInRange = false;
                    haveTarget = false;
                    target = null;
                }
            }
            else
            {
                if (LookAround(Quaternion.Euler(0, -DetectAngle / 2 + Mathf.Repeat(rotatePerSecond * Time.time, DetectAngle), 0), Color.green))
                {
                    haveTarget = true;
                    target = hit.transform.gameObject;
                }
            }
        }
    }

    public bool CanPick()
    {
        if(StatusAvailableForPickUp.GetComponent<StatusMaster>().Status_Num == 5
        && Time.time > 0
        && !player_Master.Had_Status
        && StatusInRange
        && StatusAvailableForPickUp.root.tag != GameManager_Reference._PlayerTag
        && !confirmownershipStatus)
        {
            return true;
        }
            return false;      
    }
    public void AirCancel()
    {
        // if(StatusAvailableForPickUp.GetComponent<StatusMaster>().Status_Num != 5)
        // {
            CarryOutThrowActions();
            StatusAvailableForPickUp.GetComponent<StatusMaster>().Durability = player_Master.Durability;
        // }
    }
    public void AirEvent()
    {
        StatusAvailableForPickUp.GetComponent<StatusMaster>().CallEventPickupAction(rayTransformPivot);
        player_Master.status_Num = 1;
        confirmownershipStatus = true;
        // Debug.Log("AirEvent");
    }
    public void HealEvent()
    {
        Debug.Log("Heal");
        StatusAvailableForPickUp.GetComponent<StatusMaster>().CallEventPickupAction(rayTransformPivot);
        player_Master.status_Num = 5;
        confirmownershipStatus = true;
    }
    public void HealCancel()
    {
        // Debug.Log("CancelHeal");
        player_Master.Healer_HandObject.SetActive(false);
        player_Master.Healer_HandObject = null;
        
        HealerThrowout();
    }
    public bool CanCancelHeal()
    {
        if(StatusAvailableForPickUp.GetComponent<StatusMaster>().Status_Num == 5)
        {
            if(confirmownershipStatus)
            {
                return true;
            }
        }
        return false;
    }
    // void CheckForStatusPickupAttempt()
    // {
        
        // if(
        //     Time.time > 0
        //     && !player_Master.Had_Status
        //     && StatusInRange
        //     && StatusAvailableForPickUp.root.tag != GameManager_Reference._PlayerTag
        //     && !confirmownershipStatus
        // )
        // {
        //     //Debug.Log("Pick UP");
        //     StatusAvailableForPickUp.GetComponent<StatusMaster>().CallEventPickupAction(rayTransformPivot);
        //     player_Master.status_Num = StatusAvailableForPickUp.GetComponent<StatusMaster>().Status_Num;
        //     confirmownershipStatus = true;

        //     if (StatusAvailableForPickUp.GetComponent<StatusMaster>().Status_Num != 5)
        //     {
        //         player_Master.Durability = StatusAvailableForPickUp.GetComponent<StatusMaster>().Durability;
        //     }
        //     // if (StatusAvailableForPickUp.GetComponent<StatusMaster>().Status_Num == 5)
        //     // {
        //     //     player_Master.InHealtime = Time.time;
        //     // }
        // }
        // else if(Input.GetButtonDown(Input_Collection.m.coll_list.Find(x => x.FeatureName == "Interact").ButtonName) && confirmownershipStatus)
        // {
        //     if(StatusAvailableForPickUp.GetComponent<StatusMaster>().Status_Num != 5)
        //     {
        //         CarryOutThrowActions();
        //         StatusAvailableForPickUp.GetComponent<StatusMaster>().Durability = player_Master.Durability;
        //     }
        //     else
        //     {
        //         player_Master.Healer_HandObject = null;
        //         HealerThrowout();
        //     }
        // }
    // }

    void CarryOutThrowActions()
    {
        confirmownershipStatus = false;
        player_Master.status_Num = 0;
        StatusAvailableForPickUp.transform.parent = null;
        throwDirection = StatusAvailableForPickUp.forward;
        StatusAvailableForPickUp.gameObject.SetActive(true);
        StatusAvailableForPickUp.transform.position += new Vector3(0,2,0);
        StatusAvailableForPickUp.GetComponent<StatusMaster>().CallEventObjectThrow();       
        HurlStatus();
    }

    public void HealerThrowout()
    {
        confirmownershipStatus = false;
        player_Master.status_Num = 0;
        StatusAvailableForPickUp.GetComponent<StatusMaster>().CallEventObjectThrow();
        // Player_ChangeStatus.Instance.HealClose();
    }

    void HurlStatus()
    {
        StatusAvailableForPickUp.GetComponent<Rigidbody>().AddForce(throwDirection * throwForce, ForceMode.Impulse);
    }

    public bool LookAround(Quaternion eulerAnger, Color DebugColor)
    {
        Debug.DrawRay(rayTransformPivot.position, eulerAnger * rayTransformPivot.forward.normalized * DetectRange, DebugColor);
        return Physics.Raycast(rayTransformPivot.position, eulerAnger * rayTransformPivot.forward, out hit, DetectRange, LayerToDetect);
    }
}