using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pylon : MonoBehaviour
{
    SearchArea2 Sys;
    GameObject Local;

    public int ForwardNum;     //正面軸どれやねんz:0 x:1 y:2 -z:3 -x:4 -y:5

    void Start()
    {
        //1度親に行かないと同階層のものが取れない
        Local = transform.Find("Turret").transform.Find("SearchArea2").gameObject;
        Sys = Local.GetComponent<SearchArea2>();

    }

    void Update()
    {
        if(Sys.getFlag() == 1)
        {
            //Sys = Local.GetComponent<SearchArea2>();

            //プレイヤーの方向差分
            var Direction = Sys.getPosition() - transform.position;

            //y軸固定
            Direction.y = 0;

            //よくわからん
            Quaternion Rotation = Quaternion.LookRotation(Direction);

            // forwardの向きがおかしい場合補正をかける
            float LocalNum = Rotation.eulerAngles.y;

            if (ForwardNum == 1)
            {
                LocalNum = LocalNum - 90;
            }
            if (ForwardNum == 5)
            {
                LocalNum = LocalNum + 90;
            }

            // 補正実行
            Rotation = Quaternion.Euler(new Vector3(Rotation.eulerAngles.x, LocalNum, Rotation.eulerAngles.z));


            //そっちへ向く
            transform.rotation = Quaternion.Slerp(transform.rotation, Rotation, 5f * Time.deltaTime);


        }
    }
}
