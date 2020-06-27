using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float shotSpeed;
    public int shotCount = 300;
    public float AttackSpeed;
    private float shotInterval;



    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            //連射速度
            shotInterval = shotInterval + Time.deltaTime * 1 ;

            if (shotInterval >= AttackSpeed && shotCount > 0)
            {
                shotCount = shotCount - 1;

                shotInterval = 0;

                GameObject bullet = (GameObject)Instantiate(bulletPrefab, transform.position, Quaternion.Euler(transform.parent.eulerAngles.x, transform.parent.eulerAngles.y, 0));
                Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
                bulletRb.AddForce(transform.forward * shotSpeed);

                //射撃されてから2秒後に銃弾のオブジェクトを破壊する
                Destroy(bullet, 2.0f);
            }

        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            shotCount = 300;
        }
    }
}
