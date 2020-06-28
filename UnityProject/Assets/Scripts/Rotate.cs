using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{

    public GameObject Center;
    public Vector3 maxRotateSpeed;

    [Range(0, 20)]
    public float RotateCenterSpeed;

    // Update is called once per frame
    private void FixedUpdate()
    {
        transform.Rotate(maxRotateSpeed.x, maxRotateSpeed.y, maxRotateSpeed.z);
        transform.RotateAround(Center.transform.position, Vector3.up, RotateCenterSpeed);
    }
}
