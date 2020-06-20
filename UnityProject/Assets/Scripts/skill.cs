using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skill : MonoBehaviour
{
    ObjectPoller poller;
    // Start is called before the first frame update
    [SerializeField] private float hitDamage = 100;
    [SerializeField] private float dotDamage = 5;
    void Start()
    {
        poller = GameObject.Find("ObjectPoller").GetComponent<ObjectPoller>();
    }
    float time = 0.0f;
    bool timeCheck;
    private void OnEnable()
    {
        timeCheck = true;
        time = 0.0f;
    }

    private void Update()
    {
        if (timeCheck)
        {
            time += Time.deltaTime;
            if (time > 7.5f)
            {
                timeCheck = false;
                time = 0.0f;
                transform.gameObject.SetActive(false);
            }
        }
    }


    private void OnTriggerStay(Collider other)
    {

        if (other.CompareTag("Monster"))
        {
            if (time >= 6.5f)
            {
                other.transform.GetComponentInParent<GolemStatus>().HealthPoint = -hitDamage;
                transform.gameObject.SetActive(false);
            }
            else
            {
                other.transform.GetComponentInParent<GolemStatus>().HealthPoint = -dotDamage * Time.deltaTime;
            }
        }
    }

}
