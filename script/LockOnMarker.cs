using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockOnMarker : MonoBehaviour
{

    GameObject enemy;
    List<Vector3> enemys;


    GameObject LockOnEnemy;
    int[] LockOnEnemysFlag;


    public int markernum;

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponent<Image>().color = new Color(0f, 0f, 0f, 0f);//透明
    }

    // Update is called once per frame
    void Update()
    {

        //マーカーを非表示
        this.gameObject.GetComponent<Image>().color = new Color(0f, 0f, 0f, 0f);

        //とりあえず"Enemy"タグのついたものにアクセス
        if (GameObject.FindWithTag("Enemy") != null)
        {
            enemy = GameObject.FindWithTag("Enemy");

            /*ロックオンシステム*/
            LockOnSystem l = enemy.GetComponent<LockOnSystem>();


            //画面に映っている敵のリストを取得
            enemys = l.getTarget();

            //ロックオンサークル内に居る敵のリストを取得
            LockOnEnemysFlag = l.LockOngetTargetsFlag();


            if (enemys.Count != 0)
            {
                //enemys[markernum]の範囲外参照を避けるための処理
                if (enemys.Count >= markernum + 1)
                {

                    //画面にマーカーを表示する位置を計算
                    Vector2 position = RectTransformUtility.WorldToScreenPoint(Camera.main, enemys[markernum]);

                    //画像を計算した位置にポジションを変える
                    this.transform.position = new Vector3(position.x, position.y, 0f);


                    //ロックオンサークル内に居れば
                    if( LockOnEnemysFlag[markernum] == 1 )
                    {
                        //マーカーを表示
                        this.gameObject.GetComponent<Image>().color = new Color(0f, 1f, 1f, 1f);


                        //マーカーを回転
                        transform.localEulerAngles = new Vector3(0, 0, transform.localEulerAngles.z + 0.5f);

                        if(l.getFlagListCircle() == markernum)
                        {
                            //赤色にして表示
                            this.gameObject.GetComponent<Image>().color = new Color(1f, 0f, 0f, 1f);
                        }

                    }
                    else
                    {
                        //マーカーを表示
                        this.gameObject.GetComponent<Image>().color = new Color(0f, 1f, 1f, 0.5f);

                        //マーカーを回転
                        transform.localEulerAngles = new Vector3(0, 0, transform.localEulerAngles.z + 0.2f);

                    }
                                        
                }
                


            }
        }

        transform.localEulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);

    }
}
