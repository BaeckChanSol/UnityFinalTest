using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : StatusInterface
{
    PlayerControler pc;
    private Animator at_body;
    Coroutine restCol = null;
    ObjectPoller pollor;
    [SerializeField] private float StRecoverySpeed = 50.0f;
    [SerializeField] private float StRunUseSpeed = 75.0f;

    bool DamageTo = false;

    public bool isAlive
    {
        get
        {
            if (HP <= 0.0f)
                at_body.SetBool("isDie", true);
            return HP > 0.0f;
        }
    }
    public bool damageTo
    {
        get
        {
            if (DamageTo)
            {
                DamageTo = false;
                return true;
            }
            else
                return false;
        }
    }


    IEnumerator UseStForRun()
    {
        StaminaPoint = -Time.deltaTime * StRunUseSpeed;
        yield return null;
    }

    public float moveSpeed
    {
        get
        {
            return MS;
        }
    }

    public float runSpeed
    {
        get
        {
            return RS;
        }
    }

    public float PercentHP
    {
        get
        {
            return (float)(HP) / (float)(mHP);
        }
    }

    public float PercentSP
    {
        get
        {
            return (float)(SP) / (float)(mSP);
        }
    }
    
    public bool isGuard
    {
        get
        {
            return isGuarding;
        }
        set
        {
            isGuarding = value;
            at_body.SetBool("isGuard", value);
        }
    }

    public bool isWalk
    {
        get
        {
            return isWalking;
        }

        set
        {
            isWalking = value;
            at_body.SetBool("isWalk", value);
        }
    }
    public bool isRun
    {
        get
        {
            return isRunning;
        }

        set
        {
            isRunning = value;
            at_body.SetBool("isRun", value);
            if (value == true)
            {
                StartCoroutine(UseStForRun());
                at_body.SetBool("isRest", false);
            }
        }
    }
    public bool isAttack
    {
        get
        {
            if (AttackCount != 0)
                return true;

            return false;
        }
        set
        {
            if (value == true)
            {
                if (AllAttackCount < MaxAttackCount)
                {
                    if (AllAttackCount == 0)
                        DamageTo = true;
                    AttackMove = true;
                    at_body.SetInteger("AttackCount", ++AttackCount);
                    ++AllAttackCount;
                    at_body.SetBool("isRest", false);
                }
            }
        }
    }
    public bool isRolling
    {
        get
        {
            return doRolling;
        }
        set
        {
            doRolling = value;
            at_body.SetBool("isRoll", doRolling);
            if(value)
            {
                pollor.Spawn("MotionTrail", Vector3.zero);
                at_body.SetBool("isRest", false);
                resetAttackCount();
            }
        }
    }
    void FinishedRoll()
    {
        doRolling = false;
        at_body.SetBool("isRoll", doRolling);
        while(pollor.isSpawned("MotionTrail"))
            pollor.Delete("MotionTrail");
    }

    public bool doAttackMove
    {
        get
        {
            return AttackMove;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        pc = GameObject.Find("Player").GetComponent<PlayerControler>();
        at_body = GameObject.Find("Body").GetComponent<Animator>();
        pollor = GameObject.Find("ObjectPoller").GetComponent<ObjectPoller>();

    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(statminaRecovery());
    }

    void StartRest()
    {
        if(restCol != null)
            StopCoroutine(restCol);
        restCol = StartCoroutine(RestTimeCheckCol());
    }


    IEnumerator RestTimeCheckCol()
    {
        yield return new WaitForSeconds(RestTime);
        at_body.SetBool("isRest", true);
    }

    void DoAttackMove()
    {
        AttackMove = false;
    }


    void DecreaseAttackCount()
    {
        if (AttackCount != 0)
            AttackCount -= 1;
        at_body.SetInteger("AttackCount", AttackCount);
        DamageTo = true;
        if ((AttackCount) == 0)
        {
            DamageTo = false;
            AllAttackCount = 0;
            AttackMove = false;
        }
    }



    void resetAttackCount()
    {
        DamageTo = false;
        AttackCount = 0;
        AttackMove = false;
        AllAttackCount = 0;
    }


    public float StaminaPoint
    {
        get
        {
            return SP;
        }
        set
        {
            SP = value;
        }
    }

    public float HealthPoint
    {
        get
        {
            return HP;
        }
        set
        {
            if (isGuard)
            {
                HP = value * 0.2f;
                SP = -200;
            }
            else
                HP = value;
        }
    }

    IEnumerator statminaRecovery()
    {
        if(!isGuard)
            StaminaPoint = Time.deltaTime * StRecoverySpeed;
        yield return null;

    }

}

