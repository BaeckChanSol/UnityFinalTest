using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using Unity.Mathematics;

public class healthVolume : MonoBehaviour
{
    [SerializeField] private Volume vol;
    [SerializeField] private Vignette vig;
    private PlayerStatus ps; 
    // Start is called before the first frame update
    void Start()
    {
        ps = GameObject.Find("Body").GetComponent<PlayerStatus>();
        vol.profile.TryGet(out vig);
    }

    float pumpT = 0.0f;
    float pmTrans = 1.0f;
    // Update is called once per frame
    void Update()
    {
      
        if(!ps.isAlive)
        {
            vig.color.value = Color.black;
            vig.smoothness.value = 1.0f;
            vig.intensity.value = 0.5f;
        }
        else if (ps.HealthDanger)
        {
            vig.color.value = Color.red;
            vig.smoothness.value = Mathf.Lerp(0.2f, 0.5f, pumpT);
            vig.intensity.value = 0.3f;
            pumpT += Time.deltaTime * 0.8f * pmTrans;
            if (pumpT > 1.0f || pumpT < 0.0f)
                pmTrans *= -1;
        }
        else
        {
            vig.color.value = Color.black;
            vig.smoothness.value = 0.2f;
            vig.intensity.value = 0.3f;
        }
    }
}
