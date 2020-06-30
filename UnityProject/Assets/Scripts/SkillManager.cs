using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    private Dictionary<string, GameObject> Image;
    private bool TimeToSelectSkill;

    public bool SelectSkill
    {
        get { return TimeToSelectSkill; }    
    }


    private void Awake()
    {
        Image = new Dictionary<string, GameObject>();
        for (int i = 0; i < transform.childCount; ++i)
            Image.Add(transform.GetChild(i).name, transform.GetChild(i).gameObject);
        TimeToSelectSkill = false;

    }

    void Start()
    {
        Image["BaseUI"].SetActive(false);
        for (int i = 1; i <= 3; i++)
        {
            if (Image.ContainsKey("SkillImage"+i))
            {
                Image["SkillImage"+i].SetActive(false);
                Image["Selected"+i].SetActive(false);
                Image["Locked"+i].SetActive(false);
            }
        }

    }

    public void OnUI(bool[] data)
    {
        TimeToSelectSkill = true;
        Image["BaseUI"].SetActive(true);
        for (int i = 1; i <= 3; i++)
        {
            if (Image.ContainsKey("SkillImage" + i))
            {
                Image["SkillImage" + i].SetActive(true);
                Image["Locked" + i].SetActive(!data[i-1]);
            }
        }
    }

    public void UnLock(int index)
    {
        if (Image.ContainsKey("SkillImage" + index))
            Image["Locked" + index].SetActive(false);
    }

    public void Lock(int index)
    {
        if (Image.ContainsKey("SkillImage" + index))
            Image["Locked" + index].SetActive(true);
    }


    public void OffUI()
    {
        TimeToSelectSkill = false;
        Image["BaseUI"].SetActive(false);
        for (int i = 1; i <= 3; i++)
        {
            if (Image.ContainsKey("SkillImage" + i))
            {
                Image["SkillImage" + i].SetActive(false);
                Image["Selected" + i].SetActive(false);
                Image["Locked" + i].SetActive(false);
            }
        }
    }

    float x;
    float y;
    GameObject select = null;
    int selectIndex;
    void Update()
    {
        if (!TimeToSelectSkill)
            return;
        x = Input.GetAxis("Mouse X");
        y = Input.GetAxis("Mouse Y");

        float angle = Vector2.SignedAngle(Vector2.right,new Vector2(x,y));

        if (x == 0 && y == 0)
            return;

        if(15f < angle)
        {
            if (Image.ContainsKey("SkillImage2"))
            {
                Image["Selected2"].SetActive(true);
                if (select != null && select != Image["Selected2"])
                    select.SetActive(false);
                select = Image["Selected2"];
                selectIndex = 2;
            }
            else
            {
                if (select != null)
                    select.SetActive(false);
                select = null;
                selectIndex = 0;
            }
        }
        else if(-15f <= angle && angle <= 15f)
        {
            if (Image.ContainsKey("SkillImage1"))
            {
                Image["Selected1"].SetActive(true);
                if (select != null && select != Image["Selected1"])
                    select.SetActive(false);
                select = Image["Selected1"];
                selectIndex = 1;
            }
            else
            {
                if (select != null)
                    select.SetActive(false);
                select = null;
            }
        }
        else if (-15f > angle)
        {
            if (Image.ContainsKey("SkillImage3"))
            {
                Image["Selected3"].SetActive(true);
                if (select != null && select != Image["Selected3"])
                    select.SetActive(false);
                select = Image["Selected3"];
                selectIndex = 3;
            }
            else
            {
                if (select != null)
                    select.SetActive(false);
                select = null;
                selectIndex = 0;
            }

        }

    }
    
    public bool isShowUI()
    {
        return Image["BaseUI"].activeSelf == true;
    }

    public int getSelectedIndex()
    {
        return selectIndex;
    }
}
