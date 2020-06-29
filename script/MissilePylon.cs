using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissilePylon : MonoBehaviour
{
    SearchArea2 Sys;
    GameObject Local;

    void Start()
    {
        //1度親に行かないと同階層のものが取れない
        Local = transform.Find("F/A/B/C/D/E/canon").transform.Find("SearchArea2").gameObject;
        Sys = Local.GetComponent<SearchArea2>();
    }


    void Update()
    {
        if (Sys.getFlag() == 1)
        {

            //プレイヤーの方向差分
            var Direction = Sys.getPosition() - transform.position;

            //y軸固定
            Direction.y = 0;

            //よくわからん
            Quaternion Rotation = Quaternion.LookRotation(Direction);



            // 補正実行
            Rotation = Quaternion.Euler(new Vector3(Rotation.eulerAngles.x, Rotation.eulerAngles.y - 270, Rotation.eulerAngles.z));


            //そっちへ向く
            transform.rotation = Quaternion.Slerp(transform.rotation, Rotation, 5f * Time.deltaTime);


        }
    }
}
