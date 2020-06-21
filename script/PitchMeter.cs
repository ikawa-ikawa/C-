using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PitchMeter : MonoBehaviour
{

    Rigidbody Jet;
    JET JetScript;

    Text PitchText;

    void Start()
    {
        //ジェットからコンポーネント取得
        Jet = GameObject.FindWithTag("Player").GetComponent<Rigidbody>();

        // テキストコンポーネント取得
        PitchText = GetComponent<Text>();
    }

    void Update()
    {
        // テキストの表示を入れ替える
        float Pitch = Jet.transform.localEulerAngles.x;

        if( 180 <= Pitch && Pitch <= 360 )
        {
            Pitch = (360 - Pitch);
        }
        else
        {
            Pitch = Pitch * -1;
        }


        PitchText.text = "" + Pitch.ToString("f2");
    }
}
