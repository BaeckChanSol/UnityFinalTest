using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.SceneManagement;

public class EventCreater : MonoBehaviour
{
    [SerializeField] EventHandler handler;
    // Start is called before the first frame update
    void start()
    {
        handler.Subscribe("slowGameSpeed", slowMotion);
        handler.Subscribe("normalGameSpeed", normalMotion);
        handler.Subscribe("restart", restart);
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
}
