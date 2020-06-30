using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
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
    private SkillManager skillManager;
    private GolemStatus gs;
    private EventHandler eh;


    Dictionary<string, bool> keyDictionary;
    void Start()
    {
        keyDictionary = new Dictionary<string, bool>
        {
            { "Space", false},
            { "LeftShift", false },
            { "Q", false},
            { "LMouse", false},
            { "RMouse", false},
        };

        tr = GetComponent<Transform>();
        eh = GameObject.Find("StageEventHandler").GetComponent<EventHandler>();
        gs = GameObject.Find("PBR_Golem").GetComponent<GolemStatus>();
        tr_body = GameObject.Find("Body").GetComponent<Transform>();
        pStatus = GameObject.Find("Body").GetComponent<PlayerStatus>();
        tr_Cam = GameObject.Find("Main Camera").GetComponent<Transform>();
        LookRight = Vector3.right;
        LookForward = Vector3.forward;
        CharacterGoto = Vector3.forward;
        poller = GameObject.Find("ObjectPoller").GetComponent<ObjectPoller>();
        skillManager = GameObject.Find("SkillUI").GetComponent<SkillManager>();
    }


    private void Update()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        x = Input.GetAxis("Mouse X");
        y = Input.GetAxis("Mouse Y");

        if (Input.GetMouseButton(1))
            keyDictionary["RMouse"] = true;
        if (Input.GetKeyDown(KeyCode.Space))
            keyDictionary["Space"] = true;
        if(Input.GetMouseButtonDown(0))
            keyDictionary["LMouse"] = true;
        if (Input.GetKey(KeyCode.LeftShift))
            keyDictionary["LeftShift"] = true;
        if(Input.GetKeyDown(KeyCode.Q))
            keyDictionary["Q"] = true;
    }


    bool CanMove = true;
    [SerializeField] float rayDistance = 0.5f;

    void RayCheck()
    {
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
        if (skillManager.isShowUI())
        {
            keyDictionary["RMouse"] = false;
            return;
        }

        if (keyDictionary["RMouse"] && pStatus.isRolling == false && pStatus.isAttack == false && pStatus.StaminaPoint >= 200)
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
        keyDictionary["RMouse"] = false;

    }
    void Roll()
    {
        if (skillManager.isShowUI())
        {
            keyDictionary["Space"] = false;
            return;
        }

        if (keyDictionary["Space"] && pStatus.StaminaPoint >= 200 && (h != 0 || v != 0) && pStatus.isGuard == false && pStatus.isRolling == false)
        {
            pStatus.isRolling = true;
            pStatus.StaminaPoint = -200;
        }
        keyDictionary["Space"] = false;
        if (pStatus.isRolling == true)
        {
            if(CanMove)
                tr.Translate(CharacterGoto.normalized * pStatus.moveSpeed * rollingMoveCorrection * Accel * Time.deltaTime);
        }

    }
    void Attack()
    {
        if (skillManager.isShowUI())
        {
            if (keyDictionary["LMouse"] && pStatus.isGuard == false && pStatus.isRolling == false)
            {
                switch(skillManager.getSelectedIndex())
                {
                    case 1:
                        if(pStatus.skilluse[1])
                        {
                            poller.Spawn("RotatorPS1", transform.position);
                            eh.Emit("UserSkillLock");
                        }
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                }
                skillManager.OffUI();
                eh.Emit("normalGameSpeed");
            }
        }
        else
        {
            if (keyDictionary["LMouse"] && pStatus.isGuard == false && pStatus.isRolling == false)
            {
                pStatus.isAttack = true;
            }
            if (pStatus.doAttackMove == true)
            {
                if (CanMove)
                    tr.Translate(CharacterGoto.normalized * pStatus.moveSpeed * attackMoveCorrection * Time.deltaTime);
            }
        }
        keyDictionary["LMouse"] = false;

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
        if (skillManager.isShowUI())
            return;

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

        if (keyDictionary["LeftShift"] && pStatus.isAttack == false && pStatus.isGuard == false && pStatus.StaminaPoint > 0 && !ShiftDrained)
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
        keyDictionary["LeftShift"] = false;
    }
    
    public bool CheckAdaptSlowmotion
    {
        get { return skillManager.isShowUI(); }
    }

    void skill()
    {
        if (pStatus.isAttack || pStatus.isGuard || pStatus.isRolling)
        {
            keyDictionary["Q"] = false;
            return;
        }

        if (keyDictionary["Q"])
        {
            if (skillManager.isShowUI())
            {
                eh.Emit("normalGameSpeed");
                skillManager.OffUI();
            }
            else
            {
                if (gs.isChance)
                    eh.Emit("slowGameSpeed");
                skillManager.OnUI(pStatus.skilluse);
            }
        }
        keyDictionary["Q"] = false;


    }

    // Update is called once per frame
    void FixedUpdate()
    {
 

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


