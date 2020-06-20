using UnityEngine;

public class StatusInterface : MonoBehaviour
{
    [SerializeField] float maxHealthPoint = 1000;
    [SerializeField] float HealthPoint = 1000;

    [SerializeField] float maxStaminaPoint = 1000;
    [SerializeField] float staminaPoint = 1000;

    [SerializeField] float MoveSpeed = 4.0f;
    [SerializeField] float RunSpeed = 6.0f;

    [SerializeField] protected float RestTime = 10.0f;

    [SerializeField] protected int MaxAttackCount = 3;
    protected int AttackCount = 0;
    protected int AllAttackCount = 0;

    protected bool AttackMove = false;
    protected bool doRolling = false;
    protected bool isRunning = false;
    protected bool isWalking = false;
    protected bool isGuarding = false;
    protected bool isAttacked = false;

    protected float RT
    {
        get
        {
            return RestTime;
        }
        set
        {
            RestTime = value;
        }
    }

    protected float mHP
    {
        set
        {
            maxHealthPoint = value;
        }
        get
        {
            return maxHealthPoint;
        }
    }

    protected float  mSP
    {
        set
        {
            maxStaminaPoint = value;
        }
        get
        {
            return maxStaminaPoint;
        }
    }

    protected float HP
    {
        get 
        { 
            return HealthPoint;
        }
        set
        {
            HealthPoint += value;
            if (HealthPoint > maxHealthPoint)
                HealthPoint = maxHealthPoint;
        }
    }

    protected float SP
    {
        get
        {
            return staminaPoint;
        }
        set
        {
            staminaPoint += value;
            if (staminaPoint > maxStaminaPoint)
                staminaPoint = maxStaminaPoint;
        }
    }

    protected float MS
    {
        get
        {
            return MoveSpeed;
        }
        set
        {
            MoveSpeed = value;
        }
    }
    protected float RS
    {
        get
        {
            return RunSpeed;
        }
        set
        {
            RunSpeed = value;
        }
    }

}