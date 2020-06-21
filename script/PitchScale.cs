using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PitchScale : MonoBehaviour
{


    Rigidbody Jet;
    JET JetScript;

    // Start is called before the first frame update
    void Start()
    {
        //ジェットからコンポーネント取得
        Jet = GameObject.FindWithTag("Player").GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        // テキストの表示を入れ替える
        float Pitch = Jet.transform.localEulerAngles.x;

        if (180 <= Pitch && Pitch <= 360)
        {
            Pitch = (360 - Pitch);
        }
        else
        {
            Pitch = Pitch * -1;
        }

        transform.localEulerAngles = new Vector3(0, 0, -Pitch);
    }
}
