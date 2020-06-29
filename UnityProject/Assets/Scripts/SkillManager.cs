using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    private Dictionary<string, GameObject> Image;


    private void Awake()
    {
        Image = new Dictionary<string, GameObject>();
        for (int i = 0; i < transform.childCount; ++i)
            Image.Add(transform.GetChild(i).name, transform.GetChild(i).gameObject);
    }

    void Start()
    {
        Image["Selected"].SetActive(false);
        Image["BaseUI"].SetActive(false);
        if(Image.ContainsKey("SkillImage1"))
            Image["SkillImage1"].SetActive(false);
        if (Image.ContainsKey("SkillImage1"))
            Image["SkillImage2"].SetActive(false);
        if (Image.ContainsKey("SkillImage1"))
            Image["SkillImage3"].SetActive(false);
        Image["Selected"].SetActive(false);
    }



    private void OnEnable()
    {
        
    }

    private void nDisable()
    {
        Image["Selected"].SetActive(false);
        Image["BaseUI"].SetActive(false);
        if (Image.ContainsKey("SkillImage1"))
            Image["SkillImage1"].SetActive(false);
        if (Image.ContainsKey("SkillImage1"))
            Image["SkillImage2"].SetActive(false);
        if (Image.ContainsKey("SkillImage1"))
            Image["SkillImage3"].SetActive(false);
        Image["Selected"].SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
