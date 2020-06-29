using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public int HealPower;
    public float HealInterval;

    float LocalInterval;
    Transform Jet;
    JET Sys;

    //何かにぶつかった時に呼ばれる
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "EnemyBullet")
        {
            //弾の攻撃力とか取るためのやつ
            EnemyBullet Bullet = other.gameObject.GetComponent<EnemyBullet>();

            //乱数を得る
            int Damage = Bullet.getAttackPower() + Bullet.getErrorRange();

            //シールド値を減らす
            Sys.setHP( Sys.getHP() - Damage );
        }
        if (other.gameObject.tag == "EnemyMissile")
        {
            //弾の攻撃力とか取るためのやつ
            EnemyMissile Missile = other.gameObject.GetComponent<EnemyMissile>();

            //乱数を得る
            int Damage = Missile.getAttackPower() + Missile.getErrorRange();

            //シールド値を減らす
            Sys.setHP(Sys.getHP() - Damage);
        }
    }

    void Start()
    {
        Jet = GameObject.Find("JET").GetComponent<Transform>();
        Sys = GameObject.FindWithTag("Player").GetComponent<JET>();
    }

    void Update()
    {
        LocalInterval = LocalInterval + 1 * Time.deltaTime;

        if( LocalInterval >= HealInterval )
        {
            Sys.setHP(Sys.getHP() + HealPower);
            LocalInterval = 0;
        }

        Vector3 LocalNum = Jet.transform.position;

        transform.position = LocalNum;
        transform.rotation = Jet.transform.rotation;
    }


}
