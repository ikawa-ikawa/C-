using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public int ForwardNum;     //正面軸どれやねんz:0 x:1 y:2
    public float MaxAngle;

    SearchArea2 Sys;
    GameObject Local;


    void Start()
    {
        //1度親に行かないと同階層のものが取れない
        Local = transform.Find("SearchArea2").gameObject;
        Sys = Local.GetComponent<SearchArea2>();
    }


    void Update()
    {
        if (Sys.getFlag() == 1)
        {
            //Sys = Local.GetComponent<SearchArea2>();

            //プレイヤーの方向差分
            var Direction = Sys.getPosition() - transform.position;



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
