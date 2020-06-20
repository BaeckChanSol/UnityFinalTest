using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;

using TMPro;
using UnityEngine;


public class PlayerControler : MonoBehaviour
{
    // Start is called before the first frame update
    

    [SerializeField] private float SensitiveX = 5.0f;
    [SerializeField] private float SensitiveY = 0.5f;
    [SerializeField] private float maxUpRotate = 30.0f;
    [SerializeField] private float maxDownRotate = 20.0f;
    [SerializeField] private float camUpdownCorrection = 13f;
    [SerializeField] private float attackMoveCorrection = 0.5f;
    [SerializeField] private float rollingMoveCorrection = 3f;


    private Coroutine DeleteMotionCol;
   

    private float h = 0.0f;
    private float v = 0.0f;
    private float x = 0.0f;
    private float y = 0.0f;
    private float rotate = 0.0f;

    private Transform tr;
    private Transform tr_body;
    private Transform tr_Cam;
    private float moveCorrection;
    private float Accel = 1.0f;
    private Vector3 LookForward;
    private Vector3 LookRight;
    private Vector3 CharacterGoto;
    private Vector3 RayGoto;
    private PlayerStatus pStatus;
    private ObjectPoller poller;
    private Rigidbody rigid;
 
    void Start()
    {



        tr = GetComponent<Transform>();

        tr_body = GameObject.Find("Body").GetComponent<Transform>();
        pStatus = GameObject.Find("Body").GetComponent<PlayerStatus>();
        tr_Cam = GameObject.Find("Main Camera").GetComponent<Transform>();
        LookRight = Vector3.right;
        LookForward = Vector3.forward;
        CharacterGoto = Vector3.forward;
        poller = GameObject.Find("ObjectPoller").GetComponent<ObjectPoller>();
        rigid = GetComponent<Rigidbody>();
    }

    bool CanMove = true;
    [SerializeField] float rayDistance = 0.5f;

    void RayCheck()
    {
        Debug.DrawRay(transform.position + new Vector3(0, 0.9f, 0), RayGoto.normalized * rayDistance, Color.red);
        if (Physics.Raycast(transform.position + new Vector3(0,0.9f,0), RayGoto.normalized, rayDistance))
        {
            CanMove = false;
        }
        else
        {
            CanMove = true;
        }
    }

    void Guard()
    {

        if (Input.GetMouseButton(1) && pStatus.isRolling == false && pStatus.isAttack == false && pStatus.StaminaPoint >= 200)
        {
            if (!pStatus.isGuard)
            {
                pStatus.isGuard = true;
                tr_body.rotation = Quaternion.LookRotation(1 * LookForward);
                poller.Spawn("Shield", GetComponent<Transform>().position + Vector3.up * 0.8f - LookForward * 0.1f);
                CharacterGoto = Vector3.forward;
                RayGoto = LookForward;
            }
        }
        else
        {
            pStatus.isGuard = false;
            poller.Delete("Shield");
        }
  

    }
    void Roll()
    {
        if (Input.GetKeyDown(KeyCode.Space)&& pStatus.StaminaPoint >= 200 && (h != 0 || v != 0) && pStatus.isGuard == false && pStatus.isRolling == false)
        {
            pStatus.isRolling = true;
            pStatus.StaminaPoint = -200;
        }
        if (pStatus.isRolling == true)
        {
            if(CanMove)
                tr.Translate(CharacterGoto.normalized * pStatus.moveSpeed * rollingMoveCorrection * Accel * Time.deltaTime);
        }
    }
    void Attack()
    {
        if (Input.GetMouseButtonDown(0) && pStatus.isGuard == false && pStatus.isRolling == false)
        {
            pStatus.isAttack = true;
        }
        if (pStatus.doAttackMove == true)
        {
            if(CanMove)
                tr.Translate(CharacterGoto.normalized * pStatus.moveSpeed * attackMoveCorrection * Time.deltaTime);
        }

    }
    void Move()
    {
        if ((h != 0 || v != 0) && pStatus.isGuard == false)
        {
            pStatus.isWalk = true;
            tr_body.rotation = Quaternion.LookRotation(v * LookForward + h * LookRight);
            CharacterGoto = (v * Vector3.forward + h * Vector3.right);
            RayGoto = (v * LookForward + h * LookRight);
            if (CanMove && pStatus.isAttack == false && pStatus.isRolling == false)
                tr.Translate(CharacterGoto.normalized * pStatus.moveSpeed * Accel * Time.deltaTime);
        }
        else
        {
            pStatus.isWalk = false;
        }
    }
    void CamMove()
    {
        if (x != 0 && pStatus.isGuard == false)
        {
            transform.Rotate(Vector3.up * SensitiveX * x);
            LookForward = Quaternion.Euler(new Vector3(0, SensitiveX * x, 0)) * LookForward;
            LookRight = Quaternion.Euler(new Vector3(0, SensitiveX * x, 0)) * LookRight;
            RayGoto = Quaternion.Euler(new Vector3(0, SensitiveX * x, 0)) * RayGoto;

        }
        if (y != 0)
        {

            if (rotate + SensitiveY * y >= maxUpRotate)
            {
                tr_Cam.Rotate(Vector3.left * (maxUpRotate - rotate));
                rotate = maxUpRotate;
                tr_Cam.Translate(-Vector3.up * (maxUpRotate - rotate) / camUpdownCorrection);
            }
            else if (rotate + SensitiveY * y <= -maxDownRotate)
            {
                tr_Cam.Rotate(Vector3.left * (-maxDownRotate - rotate));
                rotate = -maxDownRotate;
                tr_Cam.Translate(-Vector3.up * (-maxDownRotate - rotate) / camUpdownCorrection);
            }
            else
            {
                tr_Cam.Rotate(Vector3.left * SensitiveY * y);
                rotate += SensitiveY * y;
                tr_Cam.Translate(-Vector3.up * SensitiveY / camUpdownCorrection * y);
            }
        }
    }
    bool ShiftDrained = false;
    [SerializeField] float StaminaHurdles = 200f;
    void Shift()
    {
        if (ShiftDrained && pStatus.StaminaPoint >= StaminaHurdles)
            ShiftDrained = false;

        if (Input.GetKey(KeyCode.LeftShift) && pStatus.isAttack == false && pStatus.isGuard == false && pStatus.StaminaPoint > 0 && !ShiftDrained)
        {
            pStatus.isRun = true;
            Accel = (pStatus.runSpeed) / (pStatus.moveSpeed);
        }
        else if (pStatus.isRun)
        {
            pStatus.isRun = false;
            Accel = 1.0f;
            if(pStatus.StaminaPoint <= 0)
                ShiftDrained = true;
        }
    }

    void skill()
    {
        if(Input.GetKeyDown(KeyCode.Q) && !poller.isSpawned("RotatorPS1"))
        {
            poller.Spawn("RotatorPS1", transform.position);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
  
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        x = Input.GetAxis("Mouse X");
        y = Input.GetAxis("Mouse Y");

        if (pStatus.isAlive)
        {
            RayCheck();
            Guard();
            Roll();
            skill();
            Attack();
            Move();
            CamMove();
            Shift();
        }
    }


}


