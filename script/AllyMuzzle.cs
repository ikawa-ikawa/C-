using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyMuzzle : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float shotSpeed;
    public float AttackSpeed;
    private float shotInterval;


    SearchArea Sys;

    void Start()
    {
        //サーチエリアを取得
        Sys = transform.parent.transform.parent.transform.parent.Find("SearchArea").GetComponent<SearchArea>();

    }

    void Update()
    {

        if (Sys.getPosition() != null)
        {

            //Ray ray = new Ray(transform.position, Sys.getPosition()/*.position*/ - transform.position);

            RaycastHit hit;

            Physics.Linecast(transform.position, Sys.getPosition(), out hit);

            // Rayがなんかにヒットしたら
            if ( hit.transform == null || hit.transform.CompareTag("Enemy"))
            {

                if (Sys.getFlag() == 1)
                {
                    //連射速度
                    shotInterval = shotInterval + Time.deltaTime * 1;

                    if (shotInterval >= AttackSpeed)
                    {
                        shotInterval = 0;

                        GameObject bullet = (GameObject)Instantiate(bulletPrefab, transform.position, Quaternion.Euler(transform.parent.eulerAngles.x, transform.parent.eulerAngles.y, 0));
                        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
                        bulletRb.AddForce(transform.forward * shotSpeed);

                        //射撃されてから2秒後に銃弾のオブジェクトを破壊する
                        Destroy(bullet, 2.0f);
                    }
                }
                
            }
        }
    }
}
