using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        GetComponentInParent<Sword>().DamageTo(other);
    }
}
