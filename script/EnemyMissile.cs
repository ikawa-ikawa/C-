using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissile : MonoBehaviour
{
    // 着弾時間
    public float TrackingTime;
    public float ErrorRange;
    public float Speed;
    public float TurningPower;
    public int AttackPower;
    public GameObject ExplosionPrefab;

    float Period = 0;
    float Dot;
    int PassingFlag = 0;
    int ExplosionFlag = 0;

    GameObject Player;

    Vector3 TargetPosition;
    //Vector3 ExplosionPosition;

    LockOnSystem Sys;
    Transform Trans;
    JET Jet;


    //何かにぶつかった時に呼ばれる
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("EnemyMissile") || other.gameObject.CompareTag("Enemy") )
        {
 
        }
        else
        {

            if (Jet.getRollingFlagA() == 0 && Jet.getRollingFlagD() == 0)
            {
                Instantiate(ExplosionPrefab, this.transform.position, Quaternion.identity);

                Destroy(this.gameObject);
            }

        }

    }

    public int getAttackPower()
    {
        return AttackPower;
    }

    public int getErrorRange()
    {
        //乱数はintな．
        return (int)Random.Range(-ErrorRange, ErrorRange + 1);
    }

    void Start()
    {
        Period = 0;

        Player = GameObject.FindWithTag("Shield");

        Trans = Player.GetComponent<Transform>();

        Jet = GameObject.Find("JET").GetComponent<JET>();

        Dot = 0;
    }

    public int getPassingFlag()
    {
        return PassingFlag;
    }

    void Update()
    {

        if (Trans != null)
        {

            TargetPosition = Trans.transform.position;

            //自分の位置とターゲットの位置の差分を取得
            Vector3 relativePos = TargetPosition - transform.position;


            //よくわからん
            Quaternion rotation = Quaternion.LookRotation(relativePos);

            //相手の方をTurningPowerにそって向く
            if (PassingFlag == 0)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, TurningPower);

                //時間経過事に旋回力を上げる
                TurningPower = TurningPower + Time.deltaTime * 0.002f;

                Dot = Vector3.Dot(relativePos, this.transform.forward);
            }

            //時間計測
            Period = Period + Time.deltaTime;

            //追尾時間を超えたらどっかいく
            if (Period >= TrackingTime)
            {
                TurningPower = TurningPower - Time.deltaTime * 0.05f;
            }

            //内積を取ってマイナスなら敵を通り越したと判断
            if (Dot < 0 || PassingFlag == 1)
            {
                PassingFlag = 1;
                TurningPower = TurningPower - Time.deltaTime * 0.002f;
            }
        }

        

        //爆発したあとどっか飛んでいくのを防ぐ
        if (ExplosionFlag == 0)
        {
            //移動処理
            transform.position = transform.position + transform.forward * Speed * Time.deltaTime;
        }
        /*
        else
        {
            TurningPower = 0;

            Speed = 0;

            transform.position = ExplosionPosition;
        }*/


    }
}
