using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AircraftStatus : MonoBehaviour
{
    private Player_Master player_Master;
    private Transform cameraTransform;
    private CharacterController characterController;

    [Tooltip("浮空高度")]
    public float floating_Height;
    [Tooltip("最高浮空高度")]
    public float max_floating_Height;
    [Tooltip("上升速度")]
    public float rise_Speed;
    [Tooltip("下降速度")]
    public float decline_Spped;
    [Tooltip("回復浮空高度速度")]
    public float recovery_floatingheight_Speed;
    [Tooltip("停滯時間")]
    private float stagnant_Time;

    private float DeparturesPos; //起飛的點
    private float gobackHeight;
    private RaycastHit floorHit;
    private bool tooHigh;
    private bool needDown;
    private bool inTop;
    private float arriveHighest_Time;
    private bool NotFly;

    void OnEnable()
    {
        SetInitialReferences();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
        if (Physics.Raycast(characterController.transform.position, -Vector3.up,out floorHit, 500.0f))
        {
            gobackHeight = floorHit.transform.position.y + floating_Height;
            Debug.Log(gobackHeight);

            if (characterController.transform.position.y - DeparturesPos >= max_floating_Height)
            {
                tooHigh = true;

                arrivetooTop();
            }
            else
            {
                inTop = false;
            }
        }      

        if (transform.position.y <= gobackHeight
            && NotFly)
        {
            tooHigh = false;
            needDown = false;
        }
        else
        {
            gobackfloatHeight();
        }

        if(Input.GetButtonDown(Input_Collection.m.coll_list.Find(x => x.FeatureName == "Jump").ButtonName))
        {
            DeparturesPos = floorHit.transform.position.y;
        }

        if(Input.GetButton(Input_Collection.m.coll_list.Find(x => x.FeatureName == "Jump").ButtonName)
            && Time.timeScale > 0
            && !tooHigh
            && !needDown)
        {           
            characterController.transform.position = new Vector3(characterController.transform.position.x, characterController.transform.position.y + rise_Speed * Time.deltaTime, characterController.transform.position.z);
            NotFly = true;
        }

        if(Input.GetButtonUp(Input_Collection.m.coll_list.Find(x => x.FeatureName == "Jump").ButtonName))
        {
            needDown = true;          
        }

        ApplyBackPos();
    }

    void SetInitialReferences()
    {
        generalflyPos();
        characterController = GetComponent<CharacterController>();
        player_Master = GetComponent<Player_Master>();
        stagnant_Time = 0.3f;
    }

    void gobackfloatHeight()
    {
        if(transform.position.y > gobackHeight)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - rise_Speed * Time.deltaTime, transform.position.z);
        }
    }

    void ApplyBackPos()
    {
        if(Time.time - arriveHighest_Time >= stagnant_Time
           && needDown || tooHigh )
        {
            gobackfloatHeight();
        }
    }

    void arrivetooTop()
    {
        if (!inTop)
        {
            arriveHighest_Time = Time.time;
            inTop = true;
        }
    }  

    void generalflyPos()
    {
        transform.position = new Vector3( transform.position.x, transform.position.y + floating_Height, transform.position.z);
    }
}