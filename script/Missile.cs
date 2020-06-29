using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public GameObject MissilePrefab;
    public float ShotSpeed;
    public float ShotInterval;

    private float LocalShotInterval;


    void Start()
    {
        LocalShotInterval = ShotInterval;
    }

    void Update()
    {

        //連射速度
        LocalShotInterval = LocalShotInterval + 1 * Time.deltaTime;

        if(LocalShotInterval >= ShotInterval)
        {
            LocalShotInterval = ShotInterval;
        }


        if (Input.GetKey(KeyCode.Mouse1))
        {

            if (ShotInterval == LocalShotInterval )
            {

                GameObject Missile = (GameObject)Instantiate(MissilePrefab, transform.position, Quaternion.Euler(transform.parent.eulerAngles.x, transform.parent.eulerAngles.y, 0));
                Rigidbody MissileRb = Missile.GetComponent<Rigidbody>();

                //射撃されてから2秒後に銃弾のオブジェクトを破壊する
                Destroy(Missile, 10.0f);

                LocalShotInterval = 0;
            }

        }
    }

    public float getInterval()
    {
        return LocalShotInterval / ShotInterval;
    }
}
