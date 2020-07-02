using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultPrint : MonoBehaviour
{
    GameObject GameOver;
    GameObject GameWin;
    EventHandler eventHandler;
    private void Awake()
    {
        GameOver = transform.GetChild(0).gameObject;
        GameWin = transform.GetChild(1).gameObject;
        eventHandler = GameObject.Find("StageEventHandler").GetComponent<EventHandler>();
    }



    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            eventHandler.Emit("restart");
        }
    }

    public void setLose()
    {
        GameWin.SetActive(false);
    }

    public void setWin()
    {
        GameOver.SetActive(false);
    }
}
