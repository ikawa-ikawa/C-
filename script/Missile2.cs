using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile2 : MonoBehaviour
{


    // 着弾時間
    public float TrackingTime;
    public float ErrorRange;

    public float Speed;
    public float TurningPower;

    public GameObject ExplosionPrefab;
    public int AttackPower;

    float Period = 0;

    int PassingFlag = 0;

    GameObject Enemy;

    Vector3 TargetPosition;

    int NonTarget;

    int ExplosionFlag = 0;
    Vector3 ExplosionPosition;


    //何かにぶつかった時に呼ばれる
    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Missile") || other.gameObject.CompareTag("Shield"))
        {

        }
        else
        {
            Instantiate(ExplosionPrefab, this.transform.position, Quaternion.identity);

            transform.localScale = Vector3.zero;//見えない大きさにする

            ExplosionFlag = 1;

            ExplosionPosition = transform.position;

        }

    }

    public int getAttackPower()
    {
        return AttackPower;
    }

    public int getErrorRange()
    {
        //乱数はintになります．
        return (int)Random.Range(-ErrorRange, ErrorRange + 1);
    }

    void Start()
    {

        Period = 0;

        //とりあえず"Enemy"タグのついたものにアクセス
        if (GameObject.FindWithTag("Enemy") != null)
        {
            Enemy = GameObject.FindWithTag("Enemy");

            /*ロックオンシステム*/
            LockOnSystem Sys = Enemy.GetComponent<LockOnSystem>();

            if (Sys.getConcentrationsCount() != 0)
            {
                TargetPosition = Sys.getConcentration();
                NonTarget = 0;
            }
            else
            {
                NonTarget = 1;
            }

        }
        else
        {
            NonTarget = 1;
        }
    }



    void Update()
    {

        if (NonTarget == 0)
        {
            //自分の位置とターゲットの位置の差分を取得
            Vector3 relativePos = TargetPosition - transform.position;



            //よくわからん
            Quaternion rotation = Quaternion.LookRotation(relativePos);

            //相手の方をTurningPowerにそって向く
            if (PassingFlag == 0)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, TurningPower);

                //時間経過事に旋回力を上げる
                TurningPower = TurningPower + Time.deltaTime * 0.01f;
            }

            //時間計測
            Period = Period + Time.deltaTime;

            //追尾時間を超えたらどっかいく
            if (Period >= TrackingTime)
            {
                TurningPower = TurningPower - Time.deltaTime * 0.05f;
            }

            float Dot = Vector3.Dot( relativePos, this.transform.forward);

            //内積を取ってマイナスなら敵を通り越したと判断
            if(Dot < 0 || PassingFlag == 1)
            {
                PassingFlag = 1;
                TurningPower = TurningPower - Time.deltaTime * 0.01f;
            }

        }

        //爆発したあとどっか飛んでいくのを防ぐ
        if(ExplosionFlag == 0)
        {
            //移動処理
            transform.position = transform.position + transform.forward * Speed;
        }
        else
        {
            TurningPower = 0;

            NonTarget = 1;

            Speed = 0;

            transform.position = ExplosionPosition;
        }


    }
}
