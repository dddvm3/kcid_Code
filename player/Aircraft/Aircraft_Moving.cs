using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aircraft_Moving : MonoBehaviour
{
    private Player_Master player_Master;
    private Transform cameraTransform;
    private CharacterController characterController;

    [Header("移動速度")]
    public float move_Speed;    
    [Header("下降位移速度")]
    public float Down_move_Speed;
    [Header("旋轉角色速度")]
    public float rotateSpeed;
    [Header("停止後速度平滑時間")]
    public float speedSmoothing;

    public bool InDown;
    private float Now_moveSpeed;
    private Vector3 targetDirection;
    private Vector3 moveDirection = Vector3.zero;
    private float targetSpeed;
    private bool isMoving;
    private CollisionFlags collisionFlags;

    private void OnEnable()
    {
        SetInitialReferences();
    }

    void Start()
    {

    }

    void Update()
    {
        SmoothedMovementDirection();
        Vector3 movement = moveDirection * move_Speed;
        movement *= Time.deltaTime;
        collisionFlags = characterController.Move(movement);
    }

    void SetInitialReferences()
    {
        player_Master = GetComponent<Player_Master>();
        characterController = GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;
        moveDirection = transform.TransformDirection(Vector3.forward);
    }    

    void SmoothedMovementDirection()
    {
        Vector3 forward = cameraTransform.TransformDirection(Vector3.forward);
        forward.y = 0;
        forward = forward.normalized;

        // Right vector relative to the camera
        // Always orthogonal to the forward vector
        Vector3 right = new Vector3(forward.z, 0, -forward.x);

        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");

        targetDirection = h* right + v* forward;

        bool wasMoving = isMoving;
        isMoving = Mathf.Abs(h) > 0.1f || Mathf.Abs(v) > 0.1f;

        if (targetDirection != Vector3.zero)
        {
            if (Now_moveSpeed < move_Speed * 0.9f)
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
        Now_moveSpeed = Mathf.Lerp(Now_moveSpeed, targetSpeed, curSmooth);

        if (!InDown)
        {          
            targetSpeed *= move_Speed;
        }
        else
        {
            targetSpeed *= Down_move_Speed;
        }
    }
}