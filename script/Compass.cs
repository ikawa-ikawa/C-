using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Compass : MonoBehaviour
{

    Rigidbody Jet;
    JET JetScript;

    Text CompassText;

    void Start()
    {
        //ジェットからコンポーネント取得
        Jet = GameObject.FindWithTag("Player").GetComponent<Rigidbody>();

        // テキストコンポーネント取得
        CompassText = GetComponent<Text>();
    }

    void Update()
    {
        // テキストの表示を入れ替える
        float Compass = Jet.transform.localEulerAngles.y;


        CompassText.text = "" + Compass.ToString("f2");
    }
}
