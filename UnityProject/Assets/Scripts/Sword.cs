using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    PlayerStatus User;
    [SerializeField] float hitDamage = 100f;

    private void Awake()
    {
        User = GetComponentInParent<PlayerStatus>();
    }

    public void DamageTo(Collider other)
    {
        if(other.transform.tag.Equals("Monster") && User.isAttack && User.damageTo)
        {
            other.transform.GetComponentInParent<GolemStatus>().HealthPoint = -hitDamage;
        }
    }
}
