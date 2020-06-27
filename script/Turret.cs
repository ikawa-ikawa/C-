using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public int ForwardNum;     //正面軸どれやねんz:0 x:1 y:2
    public float MaxAngle;

    SearchArea2 Sys;
    Transform JetTr;


    void Start()
    {
        //1度親に行かないと同階層のものが取れない
        Sys = GameObject.Find("SearchArea2").GetComponent<SearchArea2>();

        JetTr = GameObject.Find("JET").GetComponent<Transform>();
    }


    void Update()
    {
        if (Sys.getFlag() == 1)
        {

            //プレイヤーの方向差分
            var Direction = JetTr.transform.position - transform.position;



            //よくわからん
            Quaternion Rotation = Quaternion.LookRotation(Direction);


            float Angle = 360 - Rotation.eulerAngles.x;

            if( Angle >= 180 )
            {
                Angle = Angle - 360;
            }

            if( Angle < 0 )
            {
                if( -Angle > MaxAngle )
                {
                    Angle = -MaxAngle;
                }
            }
            else
            {
                if( Angle > MaxAngle )
                {
                    Angle = MaxAngle;
                }
            }


            if (ForwardNum == 1)
            {
                transform.localRotation = Quaternion.Euler(0, 0, Angle);
            }

        }
    }
}
