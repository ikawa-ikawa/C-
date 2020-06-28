using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockOnMarker : MonoBehaviour
{
    public int markernum;

    GameObject MotherShip;
    GameObject LockOnEnemy;

    List<Transform> enemys;

    int[] LockOnEnemysFlag;

    Image Img;

    LockOnSystem Sys;

    void Start()
    {
        Img = this.gameObject.GetComponent<Image>();

        Img.color = new Color(0f, 0f, 0f, 0f);//透明

        MotherShip = GameObject.FindWithTag("MotherShip");

        Sys = MotherShip.GetComponent<LockOnSystem>();
    }

    void Update()
    {
        //マーカーを非表示
        Img.color = new Color(0f, 0f, 0f, 0f);

        //画面に映っている敵のリストを取得
        enemys = Sys.getTarget();

        //ロックオンサークル内に居る敵のリストを取得
        LockOnEnemysFlag = Sys.LockOngetTargetsFlag();

        if (enemys.Count >= markernum + 1)
        {
            //enemys[markernum]の範囲外参照を避けるための処理
            if (enemys.Count >= markernum + 1)
            {
                Vector2 position = new Vector2( 0, 0 );

                if (enemys[markernum] != null)
                {
                    //画面にマーカーを表示する位置を計算
                    position = RectTransformUtility.WorldToScreenPoint(Camera.main, enemys[markernum].transform.position);
                }
                    

                //画像を計算した位置にポジションを変える
                this.transform.position = new Vector3(position.x, position.y, 0f);


                //ロックオンサークル内に居れば
                if( LockOnEnemysFlag[markernum] == 1 )
                {
                    //マーカーを表示
                    Img.color = new Color(0f, 1f, 1f, 1f);

                    //マーカーを回転
                    transform.localEulerAngles = new Vector3(0, 0, transform.localEulerAngles.z + 0.5f);

                    if(Sys.getFlagListCircle() == markernum)
                    {
                        //赤色にして表示
                        Img.color = new Color(1f, 0f, 0f, 1f);
                    }
                }
                else
                {
                    //マーカーを表示
                    Img.color = new Color(0f, 1f, 1f, 0.5f);

                    //マーカーを回転
                    transform.localEulerAngles = new Vector3(0, 0, transform.localEulerAngles.z + 0.2f);

                }                                        
            }                
        }
        
        transform.localEulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);

        // カメラ処理
        if (Input.GetKey(KeyCode.Mouse2))
        {
            // バックカメラがアクティブな時は非表示
            Img.color = new Color(0f, 0f, 0f, 0f);
        }
    }
}
