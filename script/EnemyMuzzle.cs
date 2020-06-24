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

    void Start()
    {
        
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


            //GameObject bullet = (GameObject)Instantiate(bulletPrefab, transform.position, Quaternion.Euler(transform.parent.eulerAngles.x, transform.parent.eulerAngles.y, 0));
            GameObject bullet = (GameObject)Instantiate(bulletPrefab, transform.position, Quaternion.Euler(transform.parent.eulerAngles.x + ErrorX, transform.parent.eulerAngles.y + ErrorY, 0));
            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
            Transform bulletTr = bullet.GetComponent<Transform>();



            bulletRb.AddForce(bullet.transform.forward * shotSpeed);


            //射撃されてから3秒後に銃弾のオブジェクトを破壊する
            Destroy(bullet, 3.0f);
        }
    }
}
