using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyMarker : MonoBehaviour
{
    public int Markernum;
    public float DetectionRange;
    public float Radius;

    float GoalX = 0;
    float GoalY = 0;

    List<Vector3> enemys = new List<Vector3>();
    List<Vector3> FlagList = new List<Vector3>();

    GameObject MotherShip;
    GameObject[] ExistenceEnemys;

    Rigidbody JETrb;
    LockOnSystem Sys;

    Image Img;


    void Start()
    {
        Img = gameObject.GetComponent<Image>();

        //透明
        Img.color = new Color(0f, 0f, 0f, 0f);

        JETrb = GameObject.FindWithTag("Player").GetComponent<Rigidbody>();

        MotherShip = GameObject.FindWithTag("MotherShip");
        
        /*ロックオンシステム*/
        Sys = MotherShip.GetComponent<LockOnSystem>(); 
    }

    void Update()
    {
        //存在する敵のリスト
        ExistenceEnemys = Sys.getExistenceEnemys();

        //とりあえず"Enemy"タグのついたものにアクセス
        if (ExistenceEnemys != null)
        {
            int i = 0;

            FlagList.Clear();

            //敵のオブジェクトリストに対してアクセス
            while (i < ExistenceEnemys.Length)
            {
                if (ExistenceEnemys[i] != null)
                {
                    //当該の敵が画面内に居るのであれば
                    if (ExistenceEnemys[i].GetComponent<EnemyManagement>().getIsRendered())
                    {

                    }
                    else
                    {
                        float Distance = Vector3.Distance(ExistenceEnemys[i].GetComponent<Transform>().transform.position, JETrb.transform.position);

                        if (Distance < DetectionRange)
                        {
                            FlagList.Add(ExistenceEnemys[i].GetComponent<Transform>().position);
                        }
                    }
                }

                i = i + 1;
            }

            i = 0;

            //非表示
            Img.color = new Color(0f, 0f, 0f, 0f);

  
            //enemys[markernum]の範囲外参照を避けるための処理
            if ( FlagList.Count >= Markernum + 1 )
            {
                //画面にマーカーを表示する位置を計算
                Vector2 Position = RectTransformUtility.WorldToScreenPoint(Camera.main, FlagList[Markernum]);

                float X = Position.x;
                float Y = Position.y;

                float Dot = Vector3.Dot((GameObject.Find("MainCamera").GetComponent<Transform>().transform.position - FlagList[Markernum]).normalized, GameObject.Find("MainCamera").GetComponent<Transform>().transform.forward);

                GoalX = Position.x - (Screen.width / 2f);
                GoalY = Position.y - (Screen.height / 2f);
                                 
                float Katamuki = ( GoalY / GoalX );

                float x = Mathf.Sqrt( ( Radius * Radius ) / ( Katamuki * Katamuki + 1 ) );

                float y = Mathf.Sqrt( ( Radius * Radius ) - ( x * x ) );

                if ( Dot >= 0 )
                {
                    GoalX = GoalX * -1;
                    GoalY = GoalY * -1;
                }

                if ( GoalX < 0 )
                {
                    GoalX = ( Screen.width / 2f ) - x;
                }
                else
                {
                    GoalX = ( Screen.width / 2f ) + x;
                }

                if( GoalY < 0 )
                {
                    GoalY = ( Screen.height / 2f ) - y;
                }
                else
                {
                    GoalY = ( Screen.height / 2f ) + y;
                }

                this.transform.position = new Vector3( GoalX, GoalY, 0f );

                //表示する
                Img.color = new Color( 1f, 0f, 0f, 1f );

                // カメラ処理
                if (Input.GetKey(KeyCode.Mouse2))
                {
                    Img.color = new Color(0f, 0f, 0f, 0f);
                }
            }
        }       
    }
}
