using UnityEngine;
using System.Collections;
using System;
using UnityEngine.AI;
using DG.Tweening;
using Cinemachine;
public class ThirdPersonController : MonoBehaviour
{
    public Animator anim;
    private Material m_mat;
    public Material mat
    {
        get{return m_mat;}
        set{
            m_mat = value;
            for(int i = 0;i<SkinmeshAry.Length;i++)
            {
                SkinmeshAry[i].material = value;
            }
        }
    }
    public SkinnedMeshRenderer[] SkinmeshAry;
    public Normal_AniMaster normal_AniMaster;
    public static ThirdPersonController m;
    [Header("起跳前延遲時間")]
    public float jumpdelay;
    [Header("衝刺慣性")]
    public float ImpactTime;
    [Header("衝刺持續時間")]
    public float DashStayTime;
    [Header("走路速度")]
    public float walkSpeed;
    public Material FlashMat, OriMat;
    [HideInInspector]
    public float WalkInitSpeed;
    [Header("跑步速度")]
    public float runSpeed;
    [Header("空中微操移動速度")]
    public float inAirControlAcceleration;
    [Header("跳躍高度")]
    public float jumpHeight;
    [Header("二段跳躍高度")]
    public float SecondJumpHeight;
    [Header("重力")]
    public float gravity;
    [Header("停止後速度平滑時間")]
    public float speedSmoothing;
    [Header("旋轉角色速度")]
    public float rotateSpeed;
    
    [Header("Dash的CD時間,時間內不能移動")]
    public float MoveLockTime;
    
    [Header("Dash力道")]
    public float Force;
    [Header("Max空中速度")]
    public float MaxAirSpeed;
    private bool HardStraigh;
    public bool Sloping;
    public bool MoveLock;
    public float moveSpeed = 0.0f;
    private static CollisionFlags collisionFlags;
    private bool movingBack = false;
    private bool isMoving = false;
    private float lastJumpButtonTime = -10.0f;
    private float lastJumpTime = -1.0f;
    private Vector3 inAirVelocity = Vector3.zero;
    private Vector3 impact;
    private Vector3 targetDirection;
    private bool paused;
    public bool IsSlide;
    // private bool can_jump;
    private bool grounded;
    [HideInInspector]
    public bool Invincibleing;
    [Header("受傷後無敵時間")]
    public float Invincible_HurtTime;
    [Header("Dash無敵時間")]
    public float Invincible_DashTime;
    // private float NowInvincible_Time;
    public int JumpCount;
    //targetSpeed 現在速度
    private float targetSpeed;
    private Vector3 hit_normal_keep;
    private Vector3 hit_point_keep;
    public Transform hit_transform_keep;
    public float slopeLimit;
    // public float InteractDis;
    public bool WalkMode;
    private float v, h;
    private float Mass = 1;
    private float jumpRepeatTime = 0.05f;
    private float jumpTimeout = 0.15f;
    public Vector3 moveDirection = Vector3.zero;
    private float verticalSpeed = 0.0f;
    private Rigidbody rb;
    private CharacterController controller;
    public bool CantAction;
    [Header("滑動速度")]
    public float slideFriction = 0.3f;
    [Header("滑落最大值")]
    public float MaxSlideSpeed;
    private bool Stop = true;
    private RaycastHit NowHeight;
    private bool NotInGroundedLock;
    public Vector3 SliderMove;
    
    // public AudioSource RunAudiosource;
    public AudioSource CatchAudio;
    public AudioSource ThrowAudio;
    public AudioSource DashAudio; 
    public AudioSource AirDashAudio;
    public AudioSource AttackAudio;
    public AudioSource BehitAudio;
    public AudioSource JumpAudio;
    public AudioSource LandAudio;
    public AudioSource RightRun;
    public AudioSource LeftRun;
    // public cinem;
    void Awake()
    {
        m = this;
        anim = transform.GetChild(1).GetComponent<Animator>();
        WalkInitSpeed = walkSpeed;
        rb = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
        moveDirection = transform.TransformDirection(Vector3.forward);
        hit_transform_keep = new GameObject().transform;
        hit_transform_keep.SetParent(transform);
        hit_transform_keep.transform.localRotation = Quaternion.identity;
    }
    void OnPauseGame()
    {
        paused = true;
    }

    void OnResumeGame()
    {
        paused = false;
    }
    public bool CanAction()
    {
        return !anim.GetCurrentAnimatorStateInfo(0).IsName("Hitted") && !anim.GetCurrentAnimatorStateInfo(0).IsName("Hitted_recover") && !anim.GetCurrentAnimatorStateInfo(0).IsName("Dead")&&AttackCanBreak();
    }
    public bool CantCollisionEnemy()
    {
        return anim.GetCurrentAnimatorStateInfo(0).IsName("DashOnFloor")||anim.GetCurrentAnimatorStateInfo(0).IsName("DashInTheAir");

    }
    public bool AttackCanBreak()
    {
        if(anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            return (anim.GetCurrentAnimatorStateInfo(0).normalizedTime%1)>0.55f;
        }
        return true;
    }
    
    public bool CantMove()
    {
        if(anim.GetCurrentAnimatorStateInfo(0).IsName("Stop"))
        {
            return (anim.GetCurrentAnimatorStateInfo(0).normalizedTime%1)<0.6f;
        }
        return false;
    }
    public void CantActionEvent(bool CantActionBool)
    {
       
        CantAction = CantActionBool;
    }
    void UpdateSmoothedMovementDirection()
    {
        grounded = IsGrounded();

        Vector3 forward = CameraController.m_cam.transform.TransformDirection(Vector3.forward);
        forward.y = 0;
        forward = forward.normalized;

        Vector3 right = new Vector3(forward.z, 0, -forward.x);
        if(!CantAction)
        {
            if(!MoveLock&&CanAction()&&!CantMove())
            {
                v = Input.GetAxisRaw("Vertical");
                h = Input.GetAxisRaw("Horizontal");
                
            }
        }
        else
        {
            
            v = 0;
            h = 0;
        }
        // Are we moving backwards or looking backwards
        if (v < -0.2f)
        {
            normal_AniMaster.CallEventMove();
            movingBack = true;
        }
        else
        {
            //normal_AniMaster.CallEventIdle();
            normal_AniMaster.CallEventleaveMove();
            movingBack = false;
            
        }
        // if(v==0&&h==0)
        // {
        //     normal_AniMaster.CallEventleaveMove();
        // }
        bool wasMoving = isMoving;
        isMoving = Mathf.Abs(h) > 0.1f || Mathf.Abs(v) > 0.1f;
        if(isMoving)
        {
            
            if(!WalkMode)
            {
                // if(!RunAudiosource.isPlaying)
                // RunAudiosource.Play();
            }
            normal_AniMaster.CallEventleaveMoveStop();
            Stop = false;
            if(IsGrounded())
            //PlayerAnim.m.ChangeState(1);
            normal_AniMaster.CallEventMove();
        }
        else
        {
            if(!Stop)
            {
                Stop = true;
                normal_AniMaster.CallEventMoveStop();
                
                // RunAudiosource.Stop();
            }
            //  StartCoroutine(DelayMethod(0.3f, () =>
            //  { 
            //     //PlayerAnim.m.ChangeState(0);
            //     normal_AniMaster.CallEventleaveMove();
            // }));
            
        }
        targetDirection = h * right + v * forward;

        if (grounded)
        {
            if (targetDirection != Vector3.zero)
            {
                if (moveSpeed < walkSpeed * 0.9f && grounded)
                {
                    moveDirection = targetDirection.normalized;
                }
                else
                {
                    moveDirection = Vector3.RotateTowards(moveDirection, targetDirection, rotateSpeed * Mathf.Deg2Rad * Time.deltaTime, 1000);

                    moveDirection = moveDirection.normalized;
                }
            }

            float curSmooth = speedSmoothing * Time.deltaTime;
            targetSpeed = Mathf.Min(targetDirection.magnitude, 1.0f);
            if (Input.GetButtonDown(Input_Collection.m.coll_list.Find(x =>x.FeatureName == "Walk").ButtonName))
            {
                if(!CatchController.Instance.Catching)
                WalkMode = !WalkMode;
                
            }
            if(WalkMode)
            {
                //PlayerAnim.m.Walk = true;
                normal_AniMaster.CallEventWalk();
                targetSpeed *= walkSpeed;
            }
            else
            {
                //PlayerAnim.m.Walk = false;
                normal_AniMaster.CallEventRun();
                targetSpeed *= runSpeed;
            }
            moveSpeed = Mathf.Lerp(moveSpeed, targetSpeed, curSmooth);
        }
        else
        {
            if (isMoving)
            {
                inAirVelocity += targetDirection.normalized * Time.deltaTime * inAirControlAcceleration;

                if(inAirVelocity.z>MaxAirSpeed) inAirVelocity = new Vector3(inAirVelocity.x,inAirVelocity.y,MaxAirSpeed);
                if(inAirVelocity.x>MaxAirSpeed) inAirVelocity = new Vector3(MaxAirSpeed,inAirVelocity.y,inAirVelocity.z);
            }
        }
    }

    public void JumpEvent(float delay,float jumpHeight)
    {
        SlideController.Instance.SliderMove = Vector3.zero;
        
        float temp_verticalSpeed = CalculateJumpVerticalSpeed(jumpHeight);
        StartCoroutine(DelayMethod(delay, () =>
        { 
            verticalSpeed = temp_verticalSpeed;
        }));
    }
    void ApplyJumping()
    {
        if (lastJumpTime + jumpRepeatTime  > Time.time)
            return;

        if (IsGrounded())
        {
            if (Time.time < lastJumpButtonTime + jumpTimeout)
            {
                //PlayerAnim.m.ChangeState(2);
                JumpEvent(jumpdelay,jumpHeight);
            }
        }
    }
    void ApplyGravity()
    {
        if (IsGrounded())
        {
            // verticalSpeed = 0;
        }
        else
        {
            verticalSpeed -= gravity * Time.deltaTime;
        }
    }
    public IEnumerator DelayMethod(float waitSec, Action action)
    {
        yield return new WaitForSeconds(waitSec);
        action();
    }
    public IEnumerator DelayMethod(int waitframe, Action action)
    {
        for(int i = 0;i<waitframe;i++)
        {
         yield return null;
        }
        action();
    }
    
    float CalculateJumpVerticalSpeed(float targetJumpHeight)
    {
        return Mathf.Sqrt(2 * targetJumpHeight * gravity);
    }

    public void AddImpact(Vector3 dir, float force)
    {
        moveDirection = dir;
        transform.rotation = Quaternion.LookRotation(dir);
        dir.Normalize();
        if (dir.y < 0) dir.y = -dir.y; // reflect down force on the ground
        impact += dir.normalized * force / Mass;

        HardStraigh = true;
        StartCoroutine(DelayMethod(DashStayTime, () =>
        {
            HardStraigh = false;
        }
        ));
    }
    void DashEvent()
    {
        if(!CantAction)
        if(SlideController.Instance.IsSlide) 
        if(!NotInGroundedLock)
        if(!CatchController.Instance.Catching)
        if(!MoveLock&&CanAction())
        if (Input.GetButtonDown(Input_Collection.m.coll_list.Find(x => x.FeatureName == "Dash").ButtonName))
        {
            inAirVelocity = Vector3.zero;
            MoveLock = true;
            Stop = true;
            StartCoroutine(DelayMethod(MoveLockTime, () =>
			{
				MoveLock = false;
			}
			));
            // PlayerBeHurt.Instance.Invincible(Invincible_DashTime);
            if(IsGrounded())
            {
                //PlayerAnim.m.ChangeState(3,true);DashOnFloor;
                StartCoroutine(DelayMethod(0.2f,()=>{
                    if(!DashAudio.isPlaying)
                    DashAudio.Play();
                }));
                normal_AniMaster.CallEventDash();
            }
            else
            {
                normal_AniMaster.CallAirEventDash();
                if(!AirDashAudio.isPlaying)
                AirDashAudio.Play();
                NotInGroundedLock = true;
                //PlayerAnim.m.ChangeState(12,true);
            }
            // StartCoroutine(DelayMethod(0.45f, () =>
			// {
			// 	PlayerAnim.m.Lock = false;
			// }
			// ));
            if(targetDirection!=Vector3.zero)
            {
                AddImpact(targetDirection, Force);
            }
            else
            {
                AddImpact(controller.transform.forward, Force);
            }
        }
    }
    public bool CanDoubleJump()
    {
        if(anim.GetCurrentAnimatorStateInfo(0).IsName("jump"))
        {
            if((anim.GetCurrentAnimatorStateInfo(0).normalizedTime%1)<0.3f)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        else
        {
            return true;
        }
    }
    void JumpEvent()
    {
        if(!CantAction)
        if(SlideController.Instance.IsSlide) 
        if(CanAction())
        if(!CatchController.Instance.Catching)
        if (Input.GetButtonDown(Input_Collection.m.coll_list.Find(x => x.FeatureName == "Jump").ButtonName))
        {
            if(grounded){
                lastJumpButtonTime = Time.time;
                JumpCount=1;
                // JumpAudio.Play();
                normal_AniMaster.CallEventJump();
            }
            else{
                if(CanDoubleJump())
                if(JumpCount==1)
                {
                    normal_AniMaster.CallEventResetJump();
                    normal_AniMaster.CallEventDoubleJump();
                    
                    JumpEvent(0,SecondJumpHeight);
                    JumpCount=0;
                }
            }
        }
    }
    // public void InvincibleingEnter()
    // {
    //     if(Invincibleing)
    //     {
    //         // mat.color = Color.red;
    //         mat = FlashMat;
    //         // 
    //         mat.DOKill();
    //         mat.DOFade(0.3f,0.25f).OnComplete(()=>mat.DOFade(1,0.25f).SetLoops(-1,LoopType.Yoyo));
    //     }
    //     else
    //     {
    //         mat = OriMat;
    //     }
    // }
    void Update()
    {
        if (paused)
            return;

        if(Input.GetKeyDown(KeyCode.U))
        {
             
        }
        if (IsGrounded())
        {
            NowLanding();
            inAirVelocity = Vector3.zero;
        }
        if(CantCollisionEnemy())
        {
            gameObject.layer = 18;
        }
        else
        {
            gameObject.layer = 9;
        }

        // if(IsGrounded())
        // {
        //     IsSlide = (Vector3.Angle (Vector3.up, hit_normal_keep) <= slopeLimit);
        //     if (!IsSlide&&Sloping) {
        //         SliderMove.x += (1f - hit_normal_keep.y) * hit_normal_keep.x * slideFriction;
        //         SliderMove.z += (1f - hit_normal_keep.y) * hit_normal_keep.z * slideFriction;
        //         SliderMove.x = Mathf.Min(SliderMove.x,MaxSlideSpeed);
        //         SliderMove.z = Mathf.Min(SliderMove.z,MaxSlideSpeed);
        //     }
        //     else
        //     {
        //         SliderMove = Vector3.zero;
        //     }
        // }

        Physics.Raycast(transform.position, -transform.up,out NowHeight,15);
        // if(NowInvincible_Time>0)
        // {
        //     NowInvincible_Time -= Time.deltaTime;
        // }
        // else if (NowInvincible_Time<0)
        // {
        //     NowInvincible_Time = 0;
        // }
        if (IsGrounded()&&NotInGroundedLock)
        {
            NotInGroundedLock = false;
            // verticalSpeed = 0;
        }
       
        AttackEvent();
        
        if (impact.magnitude > 0.2F) controller.Move(impact * Time.deltaTime);


        if (HardStraigh)
        {
            verticalSpeed = 0;
            return;
        }
        else
        {
            impact = Vector3.Lerp(impact, Vector3.zero, ImpactTime * Time.deltaTime);
        }

        DashEvent();
        JumpEvent();
        UpdateSmoothedMovementDirection();
        ApplyGravity();
        ApplyJumping();
        
        if(CantAction||!CanAction())
        {
            rb.velocity = Vector3.zero;
            targetSpeed = 0;
            moveSpeed = 0;
            
        }
        Vector3 movement = moveDirection * moveSpeed + new Vector3(0, verticalSpeed, 0) + inAirVelocity ;//+ SliderMove;
        movement *= Time.deltaTime;

        collisionFlags = controller.Move(movement);
        if(!IsGrounded())
        {
            normal_AniMaster.CallEventFalling();
        }
        if (IsGrounded())
        {
            transform.rotation = Quaternion.LookRotation(moveDirection);
        }
        else
        {
            /* This causes choppy behaviour when colliding with SIDES
             * Vector3 xzMove = velocity;
            xzMove.y = 0;
            if (xzMove.sqrMagnitude > 0.001f)
            {
                transform.rotation = Quaternion.LookRotation(xzMove);
            }*/
        }

        // We are in jump mode but just became grounded
       
        // lastPos = transform.position;
    }

    void NowLanding()
    {
        // if(transform.position.y - NowHeight.point.y <= 1.25)
        // {
            normal_AniMaster.CallEventLanding();
        // }
    }
	public static void AttackEvent()
    {
        // Debug.Log("atkevent");
        if (Input.GetButtonUp(Input_Collection.m.coll_list.Find(x => x.FeatureName == "Attack").ButtonName))
        {
            if(InteractList.Instance.Interacting&&CatchController.Instance.Catching)
            {
                if(InteractList.Instance.bomb_controller!=null&&!InteractList.Instance.bomb_controller.IsntBumb)
                {
                    InteractList.Instance.bomb_controller.InteractCol.enabled = false;
                    InteractList.Instance.InteractGoList.Remove(InteractList.Instance.bomb_controller.InteractCol.gameObject);
                }
                InteractList.Instance.UI_State = InteractList.UIState.None;
                // InteractList.Instance.UIThrow_Hint.SetActive(false);
                InteractList.Instance.Interacting = false;
                ThirdPersonController.m.anim.SetLayerWeight(1,0);
                CatchController.Instance.Throw();
                if(!ThirdPersonController.m.ThrowAudio.isPlaying)
                {
                    ThirdPersonController.m.ThrowAudio.Play();
                }
                // anim.ChangeState(14,true,1);
                // Vector3 forward = CameraController.m_cam.transform.TransformDirection(Vector3.forward)*5;
                // CatchController.Instance.Throw(forward);
            }
            else
            {
                    // Debug.Log("atkevent2");
                if(ThirdPersonController.m.AttackCanBreak())
                {
                    // Debug.Log("atkevent3");
                    ThirdPersonController.m.ForceStop();
                    if(Player_Master.Instnace.myStatus == Status_Num.Normal)
                    {
                        Normal_AniMaster.Instnace.CallEventAttack();
                        Normal_AniMaster.Instnace.CallEventResetJump();
                    }
                    if(Player_Master.Instnace.myStatus == Status_Num.Aircraft)
                    {
                        Aircraft_AniMaster.Instance.CallEventAttack();
                        // Normal_AniMaster.Instnace.CallEventResetJump();
                    }
                }
            }
        }
    }
    // public void BeHurt(Vector3 dis,float force)
    // {
    //     if(!Invincibleing)
    //     {
    //         AddImpact(dis, force);
    //         Invincible(Invincible_HurtTime,true);
    //         Property_Class.m.NowHp -= 1;
    //         if(Property_Class.m.NowHp>0)
    //         {
    //             ForceStop(0.3f);
    //             if(Player_Master.Instnace.myStatus == Status_Num.Aircraft)
    //             {
    //                 Aircraft_AniMaster.Instance.CallEventBeHitted();
    //             }
    //             else if(Player_Master.Instnace.myStatus == Status_Num.Normal)
    //             {
    //                 normal_AniMaster.CallEventHitted();
    //             }
    //         }
    //     }
    // }
    public void ForceStop(float stoptime = 0.1f)
    {
        CantAction = true;
        StartCoroutine(DelayMethod(stoptime,()=>
        {
            if(CantAction)
            {
                CantAction = false;
            }
        }));
    }
    // public void Invincible(float InvTime,bool effect = false)
    // {
    //     if(NowInvincible_Time<InvTime)
    //     {
    //         NowInvincible_Time = InvTime;
    //     }
    //     else
    //     {
    //         return;
    //     }
        
    //     Invincibleing = true;
    //     if(effect)
    //     {
    //         InvincibleingEnter();
    //     }
    //     StartCoroutine(DelayMethod(NowInvincible_Time,()=>
    //     {
    //         Invincibleing = false;
    //         if(effect)
    //         {
    //             InvincibleingEnter();
    //         }
    //     }));
    // }
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // if(hit.gameObject.layer==17)
        // {
        //     Sloping = true;
        // }
        // else
        // {
        //     Sloping = false;
        // }
        // // IsOnGround =true;
        Debug.DrawRay(hit.point, hit.normal,Color.red,1.25f);
        // can_jump = true;
        hit_normal_keep = hit.normal;
        hit_point_keep = hit.point;
        hit_transform_keep.position = hit.point;
    }

    public static bool IsGrounded()
    {
        return (collisionFlags & CollisionFlags.CollidedBelow) !=0;
    }
}
