using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elect : MonoBehaviour
{
    [SerializeField] private float hitDamage = 200;
    bool AttackFlag = false;
    // Start is called before the first frame update

    public void attackFlagReset()
    {
        AttackFlag = false;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.transform.tag.Equals("Player") && !AttackFlag)
        {
            AttackFlag = true;
            other.gameObject.GetComponentInChildren<PlayerStatus>().HealthPoint = -hitDamage;
        }
    }
}
