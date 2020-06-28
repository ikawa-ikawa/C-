
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JET : MonoBehaviour
{
    public float ForwardStatus = 0.1f;     //加速力
    public float BrakeStatus = 0.05f;      //ブレーキ力
    public float TiltStatus = 1f;          //傾け力
    public float RotationStatus = 2500;    //機首の回転力(高い方が弱い)
    public float RiseStatus = 500;         //機首の上げ下げ（高い方が弱い
    public int MaxHp;
    public float MaxForwardPower = 50f;     //最高速度
    public float MaxTiltPower = 100;         //最高傾け

    float ForwardPower = 0.0f;        //前進力(計算用)
    float TiltPower = 0;              //傾け(計算用)

    float Inv_Rise = -1;
    float Inv_Rotation = -1;
    float Inv_Tilt = 1;

    int BstopFlag;
    int BstopCT;



    float MousPointer_x;
    float MousPointer_y;
    int Screen_w;
    int Screen_h;

    int HP;

    int RollingFlagA = 0;
    int RollingFlagD = 0;
    int AFlag = 0;
    int DFlag = 0;
    float ACnt = 0;
    float DCnt = 0;

    //普通に機体傾けるときにもローリングフラグがたっちまうぜ
    float AACnt = 0;
    float DDCnt = 0;

    //ローリング１回に何秒かかるか
    float RollingSpeed = 0.8f;
    float RollingCnt = 0;

    RectTransform GunBoreSight;

    // カメラ
    private GameObject MainCam;
    [SerializeField] GameObject SubCam = null;


    Rigidbody rigidbody_a;

    //何かにぶつかった時に呼ばれる
    void OnCollisionEnter(Collision collision)
    {
        TiltPower = 0;
        ForwardPower = 0;
    }

    public float GetForwardPower()
    {
        return ForwardPower;
    }

    void Start()
    {
        BstopFlag = 0;
        BstopCT = 100;

        HP = MaxHp;

        GunBoreSight = GameObject.Find("GunBoreSight").GetComponent<RectTransform>();

        rigidbody_a = GetComponent<Rigidbody>();

        // カメラ処理
        MainCam = GameObject.Find("MainCamera");

        SubCam.SetActive(true);
        
    }

    void Update()
    {
        // カメラ処理
        if (Input.GetKey(KeyCode.Mouse2) /*&& RollingFlagA == 0 && RollingFlagD == 0*/)
        {
        
            //MainCam.SetActive(false);
            SubCam.SetActive(true);
            
        }
        else
        {
            //MainCam.SetActive(true);
            SubCam.SetActive(false);
        }

        // HP処理
        if (HP > MaxHp)
        {
            HP = MaxHp;
        }
  

        //位置設定
        transform.position += transform.forward * Time.deltaTime * ForwardPower;

        //ローリング処理(A)
        if (Input.GetKeyUp(KeyCode.A) && BstopFlag == 0 && 0 < AACnt && AACnt < 0.2f)
        {
            AFlag = 1;
            AACnt = 0;
        }
        if( AFlag == 1 )
        {
            ACnt = ACnt + Time.deltaTime;
        }
        if(ACnt > 0.3f)
        {
            ACnt = 0;
            AFlag = 0;
        }
        if ( Input.GetKey(KeyCode.A) && BstopFlag == 0 && AFlag == 1 && ACnt <= 0.3f && RollingFlagD == 0)
        {
            ACnt = 0;
            AFlag = 0;
            RollingFlagA = 1;
        }
        //ローリング処理(D)
        if (Input.GetKeyUp(KeyCode.D) && BstopFlag == 0 && 0 < DDCnt && DDCnt < 0.2f)
        {
            DFlag = 1;
            DDCnt = 0;
        }
        if (DFlag == 1)
        {
            DCnt = DCnt + Time.deltaTime;
        }
        if (DCnt > 0.3f)
        {
            DCnt = 0;
            DFlag = 0;
        }
        if (Input.GetKey(KeyCode.D) && BstopFlag == 0 && DFlag == 1 && DCnt <= 0.3f && RollingFlagA == 0)
        {
            DCnt = 0;
            DFlag = 0;
            RollingFlagD = 1;
        }


        //実際のローリング処理
        if ( RollingFlagA == 1 || RollingFlagD == 1 )
        {
            TiltPower = 0;

            if ( RollingFlagA == 1 )
            {
                float LocalNum = (360 / RollingSpeed) * Time.deltaTime;

                transform.Rotate(new Vector3(0, 0, LocalNum));

                MainCam.transform.RotateAround(transform.localPosition, transform.forward, -LocalNum);

            }
            if( RollingFlagD == 1 )
            {
                float LocalNum = (360 / RollingSpeed) * Time.deltaTime;

                transform.Rotate(new Vector3(0, 0, (360 / RollingSpeed) * Time.deltaTime * -1));

                MainCam.transform.RotateAround(transform.localPosition, transform.forward, LocalNum);
            }

            RollingCnt = RollingCnt + Time.deltaTime;
        }

        // ローリング終了処理
        if (RollingCnt >= RollingSpeed)
        {
            RollingFlagA = 0;
            RollingFlagD = 0;
            RollingCnt = 0;

            MainCam.transform.localPosition = new Vector3(0, 10, -20);
            MainCam.transform.localEulerAngles = new Vector3(10, 0, 0);
        }




        //前進
        if (Input.GetKey(KeyCode.W) && BstopFlag == 0)
        {
            ForwardPower = ForwardPower + ForwardStatus * Time.deltaTime;


            if (ForwardPower >= MaxForwardPower)
            {
                ForwardPower = MaxForwardPower;
            }
        }
        else
        {
            //前進をやめた時の空気抵抗
            ForwardPower = ForwardPower - BrakeStatus * Time.deltaTime;

            if (ForwardPower <= 0)
            {
                ForwardPower = 0;
            }
        }

        //ブレーキ
        if (Input.GetKey(KeyCode.S) && BstopFlag == 0)
        {
            ForwardPower = ForwardPower - BrakeStatus * Time.deltaTime;

            if (ForwardPower <= 0)
            {
                ForwardPower = 0;
            }
        }

        //機体の傾け
        if (Input.GetKey(KeyCode.A) && BstopFlag == 0 && RollingFlagA == 0 && RollingFlagD == 0)
        {
            TiltPower = TiltPower + TiltStatus * Time.deltaTime;

            AACnt = AACnt + 1 * Time.deltaTime;

            if (TiltPower > MaxTiltPower)
            {
                TiltPower = MaxTiltPower;
            }
        }
        if (Input.GetKey(KeyCode.D) && BstopFlag == 0 && RollingFlagA == 0 && RollingFlagD == 0)
        {
            TiltPower = TiltPower - TiltStatus  * Time.deltaTime;

            DDCnt = DDCnt + 1 * Time.deltaTime;

            if (TiltPower < (MaxTiltPower * -1))
            {
                TiltPower = (MaxTiltPower * -1);

            }
        }
        if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && BstopFlag == 0)
        {
        
            if (TiltPower > 0)
            {
                TiltPower = TiltPower - (TiltStatus * 1.5f) * Time.deltaTime;
            }
            if (TiltPower < 0)
            {
                TiltPower = TiltPower + (TiltStatus * 1.5f) * Time.deltaTime;
            }

            AACnt = 0;
            DDCnt = 0;
        }



        //傾け実行
        //ここ変えて
        if (RollingFlagA == 0 && RollingFlagD == 0)
        {
            transform.Rotate(0, 0, TiltPower * Time.deltaTime * Inv_Tilt);
        }
        

        //Bストップ処理
        if (Input.GetKey(KeyCode.B) && BstopCT <= 0 && RollingFlagA == 0 && RollingFlagD == 0)
        {
            BstopCT = 100;

            if (BstopFlag == 0)
            {
                BstopFlag = 1;
                ForwardPower = 0.0f;        //前進力(計算用)
                TiltPower = 0;              //傾け(計算用)
                rigidbody_a.velocity = Vector3.zero;
            }
            else if (BstopFlag == 1)
            {
                BstopFlag = 0;
            }
        }
        if (BstopCT <= 100)
        {
            BstopCT = BstopCT - 1;
            if (BstopCT < 0)
            {
                BstopCT = 0;
            }
        }

        //マウス座標を取得
        MousPointer_x = Input.mousePosition.x;
        MousPointer_y = Input.mousePosition.y;

        //画面サイズの取得
        Screen_w = Screen.width;
        Screen_h = Screen.height;

        //画面の中央座標を(0, 0)とする
        MousPointer_x = MousPointer_x - (Screen_w / 2);
        MousPointer_y = MousPointer_y - (Screen_h / 2);


        //移動処理
        if ( BstopFlag == 0 && RollingFlagA == 0 && RollingFlagD == 0)
        {
            //機首の上げ下げ
            transform.Rotate(MousPointer_y / RiseStatus * Inv_Rise, 0, 0);

            //機体回転
            transform.Rotate(0, 0, MousPointer_x / RotationStatus * Inv_Rotation);

            //回転と同時に機体を傾ける
            transform.Rotate(0, ((MousPointer_x * -1) / RotationStatus) / 2 * Inv_Rotation, 0);
        }


        // Rayを飛ばす（第1引数がRayの発射座標、第2引数がRayの向き）
        Ray ray = new Ray(transform.position, transform.forward);

        // outパラメータ用に、Rayのヒット情報を取得するための変数を用意
        RaycastHit hit;


        
        if (Physics.Raycast(ray, out hit, 600.0f))
        {
            

            string hitTag = hit.collider.tag;

            //タグの名前がBulletなら
            if( hitTag.Equals("Bullet") )
            {
                //Rayを飛ばし直し
                Physics.Raycast(ray, out hit, 600.0f);

                hitTag = hit.collider.tag;
            }

            if (hitTag.Equals("Enemy"))
            {
                //レイが敵にヒットしたらガンボアサイトを縮小
                var sizeDelta = GunBoreSight.sizeDelta;
                GunBoreSight.sizeDelta = new Vector2(90, 90);
            }
            else
            {
                //レイが特に何もヒットしなけりゃガンボアサイトを通常サイズにセット
                var sizeDelta = GunBoreSight.sizeDelta;
                GunBoreSight.sizeDelta = new Vector2(120, 120);
            }

        }
        else
        {
            //レイが特に何もヒットしなけりゃガンボアサイトを通常サイズにセット
            var sizeDelta = GunBoreSight.sizeDelta;
            GunBoreSight.sizeDelta = new Vector2(120, 120);
        }
      
    }

    public int getHP()
    {
        return HP;
    }

    public int getMaxHP()
    {
        return MaxHp;
    }

    public void setHP(int x)
    {
        HP = x;
    }

    public int getBstop()
    {
        return BstopFlag;
    }

    public int getRollingFlagA()
    {
        return RollingFlagA;
    }

    public int getRollingFlagD()
    {
        return RollingFlagD;
    }
}

