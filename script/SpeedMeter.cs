using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedMeter : MonoBehaviour
{
    Rigidbody Jet;
    JET JetScript;

    Text SpeedText;

    void Start()
    {
        //ジェットからコンポーネント取得
        Jet = GameObject.Find("JET").GetComponent<Rigidbody>();
        JetScript = GameObject.Find("JET").GetComponent<JET>();

        // テキストコンポーネント取得
        SpeedText = GetComponent<Text>();
    }

    void Update()
    {
        // テキストの表示を入れ替える
        float Speed = JetScript.GetForwardPower() * 5f;
        SpeedText.text = "" + Speed.ToString("f2");
    }
}
