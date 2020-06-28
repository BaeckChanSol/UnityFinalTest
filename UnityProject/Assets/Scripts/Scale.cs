using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Scale : MonoBehaviour
{
    public Vector3 maxScale;
    public Vector3 minScale;
    private Vector3 myScale;
    bool trigger = true;
    float weight = 0;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (weight > 1.0f)
            trigger = false;
        else if (weight < 0.0f)
            trigger = true;

        if (trigger)
            weight += Time.deltaTime/3;
        else
            weight -= Time.deltaTime/3;

        myScale = Vector3.Lerp(minScale, maxScale, weight);
        transform.localScale = myScale;
    }
}
