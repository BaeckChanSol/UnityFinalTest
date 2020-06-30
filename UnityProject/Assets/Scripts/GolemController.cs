using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class GolemController : MonoBehaviour
{
    // Start is called before the first frame update


    Vector3 Look;
    GolemStatus Gs;
    float speedLerp;
    float speedMove;
    private void Start()
    {
        Gs = transform.GetComponent<GolemStatus>();
        TargetSet(GameObject.Find("Player"));
    }



    // Update is called once per frame
    void FixedUpdate()
    {
        if (Gs.isAlive && !Gs.isWin)
        {
            RoateToTarget();
            Move();
            SkillUse();
            Rest();
        }
    }
 
    void Rest()
    {
        if (restTime)
        {
            Gs.rest = true;
            Gs.StaminaPoint = 100 * Time.deltaTime;
            if (Gs.isFullSP)
            {
                Gs.rest = false;
                restTime = false;
            }
        }

    
    }

    bool restTime = false;

    void SkillUse()
    {
        if (Gs.StaminaPoint < 200)
        {
            restTime = true;
        }

        if (Gs.isAttack || Gs.IsAttacked || restTime)
            return;


        for(int i = 0; i < Gs.GetSkillCount; ++i)
        {
            if (i == 1 && Vector3.Distance(transform.position, Gs.target.transform.position) > 7.5f)
                continue;

            if (i == 0 && (Vector3.Distance(transform.position, Gs.target.transform.position) > 40.0f || Vector3.Distance(transform.position, Gs.target.transform.position) <= 7.5f))
                continue;

            if(!Gs.CheckCoolTime(i))
            {
                Gs.StaminaPoint = -200;
                Gs.UseSkill(i);
                break;
            }
        }
    }


    void TargetSet(GameObject player)
    {
        Gs.target = player;
    }

    void Move()
    {
        if (Gs.isAttack || Gs.IsAttacked || restTime)
            return;

        if (Gs.target != null && Vector3.Distance(transform.position, Gs.target.transform.position) > 5.0f)
        {
            speedLerp = Vector3.Distance(transform.position, Vector3.Lerp(transform.position, Gs.target.transform.position, 0.5f)) / Gs.speedDistance;
            if (speedLerp > 1.0f)
                speedLerp = 1.0f;
            else if (speedLerp < 0.0f)
                speedLerp = 0.0f;
            speedMove = Gs.moveSpeed + (Gs.runSpeed - Gs.moveSpeed) * speedLerp;
            Gs.isMove = true;
            transform.Translate(new Vector3(0.0f, 0.0f, 1.0f) * Time.deltaTime * speedMove);
        }
        else
            Gs.isMove = false;
    }

    void RoateToTarget()
    {
        if (restTime)
            return;

        Look = Vector3.Slerp(transform.forward, (Gs.target.transform.position - transform.position).normalized, Time.deltaTime * 3f);
        transform.forward = (Gs.target.transform.position - transform.position).normalized;
        //transform.rotation = Quaternion.LookRotation(transform.forward, Vector3.up);
        transform.rotation = Quaternion.LookRotation(Look, Vector3.up);

    }





}