using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.SceneManagement;

public class EventCreater : MonoBehaviour
{
    [SerializeField] EventHandler handler;
    PlayerStatus ps;
    // Start is called before the first frame update
    private void Start()
    {
        ps = GameObject.Find("Body").GetComponent<PlayerStatus>();
        handler.Subscribe("slowGameSpeed", slowMotion);
        handler.Subscribe("normalGameSpeed", normalMotion);
        handler.Subscribe("restart", restart);
        handler.Subscribe("skillReset", UserSkillReset);
        handler.Subscribe("removeCursor", removeCursor);
        handler.Subscribe("UserSkillLock", UserSkillLock);
        handler.Emit("removeCursor");
    }

    void removeCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    void slowMotion()
    {
        Time.timeScale = 0.3f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
    }
    void normalMotion()
    {
        Time.timeScale = 1.0f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
    }
    void restart()
    {
        SceneManager.LoadScene("SampleScene");
    }

    void UserSkillReset()
    {
        for (int i = 1; i <= 3; ++i)
            ps.skillReset(i);
    }

    void UserSkillLock()
    {
        for (int i = 1; i <= 3; ++i)
            ps.skillCoolTime(i);
    }
}
