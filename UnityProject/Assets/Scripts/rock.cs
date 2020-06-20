using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rock : MonoBehaviour
{
    ObjectPoller poller;
    // Start is called before the first frame update
    [SerializeField] private float hitDamage = 400;
    void Start()
    {
        poller = GameObject.Find("ObjectPoller").GetComponent<ObjectPoller>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag.Equals("Player"))
        {
            other.gameObject.GetComponentInChildren<PlayerStatus>().HealthPoint = -hitDamage;
        }

        if (!other.transform.tag.Equals("Monster"))
        {
            transform.gameObject.SetActive(false);
            Destroy(transform.GetComponent<Rigidbody>());
        }
    }

}
