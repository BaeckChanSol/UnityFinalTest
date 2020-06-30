using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemStatus : StatusInterface
{
    [SerializeField] GameObject Target;
    [SerializeField] float MaxSpeedDistance = 50.0f;

    bool IsAttack;
    [SerializeField] float[] SkillCoolTime;
    [SerializeField] float[] skillChargeTime;
    [SerializeField] bool[] IsSkillCoolTime;
    GameObject rock;
    GameObject elect;
    ObjectPoller poller;
    EventHandler eh;

    public bool isWin
    {
        get
        {
            if (!target.transform.GetComponentInChildren<PlayerStatus>().isAlive)
                at_body.SetBool("isWin", true);
            return !target.transform.GetComponentInChildren<PlayerStatus>().isAlive;
        }
    }

    public bool isAlive
    {
        get
        {
            if (HP <= 0.0f)
                at_body.SetBool("isDie", true);
            return HP > 0.0f;
        }
    }

    public int GetSkillCount
    {
        get
        {
            return SkillCoolTime.Length;
        }
    }

    public bool CheckCoolTime(int num)
    {
        return IsSkillCoolTime[num];
    }

    public void UseSkill(int num)
    {
        skillChargeTime[num] = 0.0f;
        IsSkillCoolTime[num] = true;
        IsAttack = true;
        at_body.SetInteger("Attack", num + 1);
    }

    void SkillUsed()
    {
        skillChargeTime[at_body.GetInteger("Attack") - 1] = 0.0f;
        IsSkillCoolTime[at_body.GetInteger("Attack") - 1] = true;
        at_body.SetInteger("Attack", 0);
    }

    public bool rest
    {
        set 
        {
            if (value)
            {
                if(target.GetComponent<PlayerControler>().CheckAdaptSlowmotion)
                    eh.Emit("slowGameSpeed");
                if(!at_body.GetBool("isRest"))
                    eh.Emit("skillReset");
            }
            else
            {
                eh.Emit("UserSkillLock");
                eh.Emit("normalGameSpeed");
            }
            at_body.SetBool("isRest", value);
        }  
    }

    public bool isChance
    {
        get
        {
            return at_body.GetBool("isRest");
        }
    }

    void resetSkill()
    {
        for (int i = 0; i < SkillCoolTime.Length; ++i)
        {
            skillChargeTime[i] = 0.0f;
            IsSkillCoolTime[i] = true;
        }
    }
    void SkillCharge()
    {
        for(int i =0; i< SkillCoolTime.Length; ++i)
        {
            if(IsSkillCoolTime[i])
            {
                skillChargeTime[i] += Time.deltaTime;
                if(skillChargeTime[i] >= SkillCoolTime[i])
                {
                    skillChargeTime[i] = 0.0f;
                    IsSkillCoolTime[i] = false;
                }
            }
        }
    }


    void makeElect()
    {
        elect = poller.Spawn("ElectParticle", GameObject.Find("Hand_L").transform.position);
        elect.transform.parent = GameObject.Find("Hand_L").transform;
    }

    void deleteElect()
    {
        elect.GetComponent<Elect>().attackFlagReset();
        elect.SetActive(false);
    }


    private Animator at_body;

    public float speedDistance
    {
        get
        {
            return MaxSpeedDistance;
        }
    }

    public bool isMove
    {
        get
        {
            return isWalking;
        }

        set
        {
            isWalking = value;
            at_body.SetBool("isMove", value);
        }
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


    public GameObject target
    {
        get
        {
            return Target;
        }
        set
        {
            Target = value;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        eh = GameObject.Find("StageEventHandler").GetComponent<EventHandler>();
        at_body = GetComponent<Animator>();
        resetSkill();
        IsAttack = false;
        poller = GameObject.Find("ObjectPoller").GetComponent<ObjectPoller>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        SkillCharge();
    }

    void MakeStone()
    {
        rock = poller.Spawn("Rock", GameObject.Find("Hand_R").transform.position);
        rock.transform.parent = GameObject.Find("Hand_R").transform;
    }

    void ThrowStone()
    {
        rock.transform.parent = null;
        rock.AddComponent<rock>();
        rock.AddComponent<Rigidbody>();
        rock.GetComponent<Rigidbody>().AddForce((Target.transform.position - transform.position) * Random.Range(30.0f, 40.0f));
    }

    void AttackEnd()
    {
        IsAttack = false;
    }

    public bool isAttack
    {
        get
        {
            return IsAttack;
        }
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
            if(value <= 0)
            {
                at_body.SetFloat("Damage", -value);
            }
            HP = value;
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

    public bool isFullSP
    {
        get
        {
            return SP == mSP;
        }
    }

    public bool IsAttacked
    {
        get
        {
            return isAttacked;
        }
    }


    void Damaged()
    {
        isAttacked = true;
    }

    void ResetDamage()
    {
        isAttacked = false;
        at_body.SetFloat("Damage", 0);
    }

}




