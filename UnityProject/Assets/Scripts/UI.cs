using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UI : MonoBehaviour
{
    // Start is called before the first frame update
    PlayerStatus ps;
    Slider playerHP_UI;
    Slider playerSP_UI;

    GolemStatus gs;
    Slider MonsterHP_UI;
    Slider MonsterSP_UI;


    void Start()
    {
        ps = GameObject.Find("Body").GetComponent<PlayerStatus>();
        playerHP_UI = GameObject.Find("Player_HP_UI").GetComponent<Slider>();
        playerSP_UI = GameObject.Find("Player_ST_UI").GetComponent<Slider>();

        gs = GameObject.Find("PBR_Golem").GetComponent<GolemStatus>();
        MonsterHP_UI = GameObject.Find("Boss_HP_UI").GetComponent<Slider>();
        MonsterSP_UI = GameObject.Find("Boss_ST_UI").GetComponent<Slider>();

    }


    private void Update() // 정립되면 코루틴으로 변경
    {
        playerHP_UI.value = ps.PercentHP;
        playerSP_UI.value = ps.PercentSP;

        MonsterHP_UI.value = gs.PercentHP;
        MonsterSP_UI.value = gs.PercentSP;

    }
    // Update is called once per frame




}
