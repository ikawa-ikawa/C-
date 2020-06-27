using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMuzzle : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float shotSpeed;
    public float AttackSpeed;
    public float AngleError;
    private float shotInterval;
    public int ForwardNum;     //正面軸どれやねんz:0 x:1 y:2

    SearchArea2 Sys;
    Transform Jet;

    void Start()
    {
        Sys = transform.parent.Find("SearchArea2").GetComponent<SearchArea2>();
        Jet = GameObject.Find("JET").GetComponent<Transform>();
    }

    void Update()
    {
        //連射速度
        shotInterval = shotInterval + Time.deltaTime * 1;

        if (shotInterval >= AttackSpeed )
        {
            shotInterval = 0;

            float ErrorX = Random.Range(-AngleError, AngleError);
            float ErrorY = Random.Range(-AngleError, AngleError);

            // サーチエリアにプレイヤーが居る場合のみ発射
            if( Sys.getFlag() == 1)
            {

                RaycastHit hit;

                if( Physics.Linecast(transform.position, Jet.position, out hit ))
                {
                    if ( hit.transform.root.CompareTag("Player") || hit.transform.root.CompareTag("Shield"))
                    {
                        if (ForwardNum == 0)
                        {
                            GameObject bullet = (GameObject)Instantiate(bulletPrefab, transform.position, Quaternion.Euler(transform.parent.eulerAngles.x + ErrorX, transform.parent.eulerAngles.y + ErrorY, 0));
                            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
                            Transform bulletTr = bullet.GetComponent<Transform>();
                            bulletRb.AddForce(bullet.transform.forward * shotSpeed);

                            //射撃されてから3秒後に銃弾のオブジェクトを破壊する
                            Destroy(bullet, 2.0f);
                        }
                        if (ForwardNum == 1)
                        {
                            GameObject bullet = (GameObject)Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0, transform.parent.eulerAngles.y + ErrorX, transform.parent.eulerAngles.z + ErrorY));
                            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
                            Transform bulletTr = bullet.GetComponent<Transform>();
                            bulletRb.AddForce(bullet.transform.right * shotSpeed);

                            //射撃されてから3秒後に銃弾のオブジェクトを破壊する
                            Destroy(bullet, 2.0f);
                        }
                    }
                }


            }

        }
    }
}
