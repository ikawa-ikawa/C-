using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyTurret : MonoBehaviour
{
    public float MaxAngle;
    public int ForwardNum;     //正面軸どれやねんz:0 x:1 y:2 -z:3 -x:4 -y:5

    SearchArea Sys;

    void Start()
    {
        //1度親に行かないと同階層のものが取れない
        Sys = transform.parent.transform.parent.Find("SearchArea").GetComponent<SearchArea>();
    }

    void Update()
    {
        if (Sys.getFlag() == 1)
        {
            if (Sys.getPosition() != null)
            {

                //敵の方向情報を取得して差分を計算
                var Direction = Sys.getPosition2()/*.position*/ - transform.position;

                //よくわからん
                Quaternion Rotation = Quaternion.LookRotation(Direction);

                //最大俯角，仰角の処理
                float Angle = 360 - Rotation.eulerAngles.x;

                if (Angle >= 180)
                {
                    Angle = Angle - 360;
                }

                if (Angle < 0)
                {
                    if (-Angle > MaxAngle)
                    {
                        Angle = -MaxAngle;
                    }
                }
                else
                {
                    if (Angle > MaxAngle)
                    {
                        Angle = MaxAngle;
                    }
                }


                if (ForwardNum == 1)
                {
                    transform.localRotation = Quaternion.Euler(0, 0, Angle);
                }
                if (ForwardNum == 5)
                {
                    transform.localRotation = Quaternion.Euler(-Angle + 0.1f, 0, 0);
                }
            }
        }
    }
}
