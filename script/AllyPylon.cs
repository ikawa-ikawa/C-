using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyPylon : MonoBehaviour
{
    SearchArea Sys;
    Transform EneTr;

    public int ForwardNum;     //正面軸どれやねんz:0 x:1 y:2 -z:3 -x:4 -y:5
    void Start()
    {
        //サーチエリアは同階層
        Sys = transform.parent.Find("SearchArea").GetComponent<SearchArea>();

    }

    void Update()
    {
        if (Sys.getFlag() == 1)
        {
            if (Sys.getPosition() != null)
            {

                //敵の方向情報を取得して差分を計算
                var Direction = Sys.getPosition()/*.position*/ - transform.position;

                //y軸固定
                Direction.y = 0;


                //よくわからん
                Quaternion Rotation = Quaternion.LookRotation(Direction);

                // forwardの向きがおかしい場合補正をかける
                float LocalNum = Rotation.eulerAngles.y;

                if (ForwardNum == 1)
                {
                    LocalNum = LocalNum - 90;

                    // 補正実行
                    Rotation = Quaternion.Euler(new Vector3(Rotation.eulerAngles.x, LocalNum, Rotation.eulerAngles.z));

                    //そっちへ向く
                    transform.rotation = Quaternion.Slerp(transform.rotation, Rotation, 2f * Time.deltaTime);
                }
                if (ForwardNum == 5)
                {
                    // 補正実行
                    Rotation = Quaternion.Euler(new Vector3(-90, LocalNum, Rotation.eulerAngles.z));

                    //そっちへ向く
                    transform.rotation = Quaternion.Slerp(transform.rotation, Rotation, 2f * Time.deltaTime);
                }
            }
        }
    }
}
