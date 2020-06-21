
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JET : MonoBehaviour
{
    float ForwardStatus = 0.1f;     //加速力
    float BrakeStatus = 0.05f;      //ブレーキ力
    float TiltStatus = 1f;          //傾け力
    float RotationStatus = 2500;    //機首の回転力(高い方が弱い)
    float RiseStatus = 500;         //機首の上げ下げ（高い方が弱い

    float Inv_Rise = -1;
    float Inv_Rotation = -1;
    float Inv_Tilt = 1;

    float ForwardPower;
    float MaxForwardPower;
    float TiltPower;
    float MaxTiltPower;

    int BstopFlag;
    int BstopCT;



    float MousPointer_x;
    float MousPointer_y;
    int Screen_w;
    int Screen_h;

    public int MaxHp;
    int HP;

    RectTransform GunBoreSight;

    //何かにぶつかった時に呼ばれる
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name); 

        TiltPower = 0;
        ForwardPower = 0;
    }

    public float GetForwardPower()
    {
        return ForwardPower;
    }

    void Start()
    {


        ForwardPower = 0.0f;        //前進力(計算用)
        MaxForwardPower = 50f;     //最高速度

        TiltPower = 0;              //傾け(計算用)
        MaxTiltPower = 100;         //最高傾け

        BstopFlag = 0;
        BstopCT = 100;

        HP = MaxHp;

        GunBoreSight = GameObject.Find("GunBoreSight").GetComponent<RectTransform>();
    }

    void Update()
    {
  
        //ステータス取得
        Rigidbody rigidbody = GetComponent<Rigidbody>();

        //位置設定
        transform.position += transform.forward * Time.deltaTime * ForwardPower;

        //前進
        if (Input.GetKey(KeyCode.W) && BstopFlag == 0)
        {
            ForwardPower = ForwardPower + ForwardStatus;


            if (ForwardPower >= MaxForwardPower)
            {
                ForwardPower = MaxForwardPower;
            }
        }
        else
        {
            //前進をやめた時の空気抵抗
            ForwardPower = ForwardPower - BrakeStatus;

            if (ForwardPower <= 0)
            {
                ForwardPower = 0;
            }
        }

        //ブレーキ
        if (Input.GetKey(KeyCode.S) && BstopFlag == 0)
        {
            ForwardPower = ForwardPower - BrakeStatus;

            if (ForwardPower <= 0)
            {
                ForwardPower = 0;
            }
        }

        //機体の傾け
        if (Input.GetKey(KeyCode.A) && BstopFlag == 0)
        {
            TiltPower = TiltPower + TiltStatus;

            if (TiltPower > MaxTiltPower)
            {
                TiltPower = MaxTiltPower;
            }
        }
        if (Input.GetKey(KeyCode.D) && BstopFlag == 0)
        {
            TiltPower = TiltPower + (TiltStatus * -1);

            if (TiltPower < (MaxTiltPower * -1))
            {
                TiltPower = (MaxTiltPower * -1);
            }
        }
        if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && BstopFlag == 0)
        {
            if (TiltPower > 0)
            {
                TiltPower = TiltPower - (TiltStatus * 1.5f);
            }
            if (TiltPower < 0)
            {
                TiltPower = TiltPower + (TiltStatus * 1.5f);
            }
        }

        //傾け実行
        //ここ変えて
        transform.Rotate(0, 0, TiltPower * Time.deltaTime * Inv_Tilt);

        //Bストップ処理
        if (Input.GetKey(KeyCode.B) && BstopCT <= 0)
        {
            BstopCT = 100;

            if (BstopFlag == 0)
            {
                BstopFlag = 1;
                ForwardPower = 0.0f;        //前進力(計算用)
                TiltPower = 0;              //傾け(計算用)
                rigidbody.velocity = Vector3.zero;
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

        if (BstopFlag == 0)
        {
            //機首の上げ下げ
            transform.Rotate(MousPointer_y / RiseStatus * Inv_Rise, 0, 0);

            //機体回転
            transform.Rotate(0, 0, MousPointer_x / RotationStatus * Inv_Rotation);

            //回転と同時に機体を傾ける
            transform.Rotate(0, ((MousPointer_x * -1) / RotationStatus) / 2 * Inv_Rotation, 0);
        }


        // Rayを飛ばす（第1引数がRayの発射座標、第2引数がRayの向き）
        //Vector3 Raystart = new Vector3(transform.position.x, transform.position.y, transform.position.z + 50);
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

    public int getBstop()
    {
        return BstopFlag;
    }
}

