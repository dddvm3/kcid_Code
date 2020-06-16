using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Displacement_Master : MonoBehaviour
{
    public delegate void MoveingHandler();
    public event MoveingHandler Normal_Status_Moving;
    public event MoveingHandler Aircraft_Status_Moving;
    public event MoveingHandler Boxer_Status_Moving;

    public delegate void JumpHandler();
    public event JumpHandler Normal_Status_Jump;
    public event JumpHandler Aircraft_Status_Jump;
    public event JumpHandler Boxer_Status_Jump;

    public delegate void DashHandler();
    public event DashHandler Normal_Status_Dash;
    public event DashHandler Aircraft_Status_Dash;
    public event DashHandler Boxer_Status_Dash;

    public delegate void BeatenHandler();
    public event BeatenHandler Beaten;

    void CallNormal_Status_Moving()
    {
        if(Normal_Status_Moving != null)
        {
            Normal_Status_Moving();
        }
    }

    void CallAircraft_Status_Moving()
    {
        if (Aircraft_Status_Moving != null)
        {
            Aircraft_Status_Moving();
        }
    }

    void CallBoxer_Status_Moving()
    {
        if (Boxer_Status_Moving != null)
        {
            Boxer_Status_Moving();
        }
    }

    void CallNormal_Status_Jump()
    {
        if(Normal_Status_Jump != null)
        {
            Normal_Status_Jump();
        }
    }

    void CallAircraft_Status_Jump()
    {
        if (Aircraft_Status_Jump != null)
        {
            Aircraft_Status_Jump();
        }
    }

    void CallBoxer_Status_Jump()
    {
        if (Boxer_Status_Jump != null)
        {
            Boxer_Status_Jump();
        }
    }

    void CallNormal_Status_Dash()
    {
        if(Normal_Status_Dash != null)
        {
            Normal_Status_Dash();
        }
    }

    void CallAircraft_Status_Dash()
    {
        if (Aircraft_Status_Dash != null)
        {
            Aircraft_Status_Dash();
        }
    }

    void CallBoxer_Status_Dash()
    {
        if (Boxer_Status_Dash != null)
        {
            Boxer_Status_Dash();
        }
    }

    void CallBeaten()
    {
        if(Beaten != null)
        {
            Beaten();
        }
    }
}