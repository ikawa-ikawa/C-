using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{

    public float ForwardStatus = 40f;     //加速力
    public float BrakeStatus = 0.05f;      //ブレーキ力
    public float MaxForwardPower = 50f;     //最高速度
    public float BoostPower = 50f;          //ブースト
    public float TiltPower = 30;         //最高傾け

    GameObject Local;
    SearchArea2 Sys;
    Transform Jet;
    Rigidbody rigidbody_a;

    float MaxForwardPowerTmp = 50f;     //最高速度退避
    float ForwardPower = 0.0f;        //前進力(計算用)
    float PassingCT = 0;
    float CTError = 0;

    int AvoidanceFlag = 0;      //回避行動を今行うかどうか
    int RightLeftFlag = 0;      //下記の右左のランダム関数を一度実行したか
    int RightLeftRand = 0;      //右と左どっちに回避するか

    void Start()
    {
        Local = transform.Find("SearchArea2").gameObject;
        Sys = Local.GetComponent<SearchArea2>();
        Jet = GameObject.Find("JET").GetComponent<Transform>();
        rigidbody_a = GetComponent<Rigidbody>();

        MaxForwardPowerTmp = MaxForwardPower;
    }

    void Update()
    {
        rigidbody_a.velocity = Vector3.zero;

        //位置設定
        transform.position += transform.forward * Time.deltaTime * ForwardPower;

        //前進
        if (1 == 1)
        {
            ForwardPower = ForwardPower + ForwardStatus * Time.deltaTime;


            if (ForwardPower >= MaxForwardPower)
            {
                ForwardPower = MaxForwardPower;
            }
        }

        //敵の方向情報を取得して差分を計算
        var Direction = Jet.transform.position - transform.position;

        //よくわからん
        Quaternion Rotation = Quaternion.LookRotation(Direction);

        // 補正実行
        Rotation = Quaternion.Euler(new Vector3(Rotation.eulerAngles.x, Rotation.eulerAngles.y, Rotation.eulerAngles.z));

        if( PassingCT >= CTError )
        {
            if( AvoidanceFlag == 0 )
            {
                //そっちへ向く
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Rotation, TiltPower * Time.deltaTime);
            }
        }


        // 内積
        float Dot = Vector3.Dot(Jet.transform.position - transform.position, this.transform.forward);

        //Rayの作成
        Ray Ray = new Ray(transform.position, transform.forward);

        //Rayが当たったオブジェクトの情報を入れる箱
        RaycastHit Hit;

        if (Physics.Linecast(transform.position, Jet.transform.position, out Hit))
        {
            //Rayが当たったオブジェクトのtagがPlayerだったら
            if (Hit.collider.CompareTag("Player") || Hit.collider.CompareTag("Shield"))
            {
                //衝突回避行動
                if (Dot >= 0 && Vector3.Distance( transform.position , Hit.transform.position ) <= 350f )
                {
                    AvoidanceFlag = 1;

                    if( RightLeftFlag == 0 )
                    {
                        RightLeftFlag = 1;
                        RightLeftRand = Random.Range(0, 2);
                    }
                }
                else
                {
                    AvoidanceFlag = 0;
                }

                if(Dot < 0)
                {
                    if (PassingCT >= 8f && Vector3.Distance(transform.position, Hit.transform.position) <= 350f)
                    {
                        MaxForwardPower = BoostPower;
                    }
                }
            }
            else
            {
                AvoidanceFlag = 0;
            }
        }
        else
        {
            AvoidanceFlag = 0;
        }


        //内積を取ってマイナスなら敵を通り越したと判断
        if (Dot < 0)
        {
            if(PassingCT == 0)
            {
                CTError = Random.Range(0.1f, 5);
            }

            PassingCT = PassingCT + 1 * Time.deltaTime;
            RightLeftFlag = 0;
        }
        if( Dot >= 0 )
        {
            MaxForwardPower = MaxForwardPowerTmp;

            if (AvoidanceFlag == 0)
            {
                //そっちへ向く
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Rotation, TiltPower * Time.deltaTime);
                PassingCT = 0;
            }
            else
            {
                if( RightLeftRand == 0 )
                {
                    transform.Rotate(new Vector3(0, 0.2f, -0.5f));
                }
                else
                {
                    transform.Rotate(new Vector3(0, -0.2f, 0.5f));
                }
            }
        }
    }
}
