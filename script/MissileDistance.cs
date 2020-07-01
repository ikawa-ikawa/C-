using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissileDistance : MonoBehaviour
{
    Transform Jet;
    JET JetScript;

    Text MissileText;

    GameObject[] Missile;

    float Distance;
    float Min;

    int Flag;

    void Start()
    {
        //ジェットからコンポーネント取得
        Jet = GameObject.Find("JET").GetComponent<Transform>();
        JetScript = GameObject.Find("JET").GetComponent<JET>();

        // テキストコンポーネント取得
        MissileText = GetComponent<Text>();

        Distance = 0;
        Min = 1000000000;
    }

    void FixedUpdate()
    {
        //敵のミサイル処理
        Missile = GameObject.FindGameObjectsWithTag("EnemyMissile");

        //非表示
        MissileText.color = new Color(0f, 0f, 0f, 0f);

        Flag = 0;
        Min = 1000000000;

        if (Missile != null)
        {
            for (int i = 0; i < Missile.Length; i = i + 1)
            {
                EnemyMissile MissileSys = Missile[i].GetComponent<EnemyMissile>();

                if (MissileSys.getPassingFlag() == 0)
                {
                    //表示
                    MissileText.color = new Color(1f, 0.5f, 0f, 1f);
                    Distance = (Missile[i].GetComponent<Transform>().position - Jet.transform.position).magnitude;
                    Flag = 1;

                    if(Distance < Min)
                    {
                        Min = Distance;
                    }
                }
                else
                {
                    Flag = 0;
                }
            }

        }

        // テキストの表示を入れ替える
        MissileText.text = "" + Min.ToString("f0");
    }

    public int getMissileFlag()
    {
        return Flag;
    }
}

    