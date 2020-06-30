using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissileMuzzle : MonoBehaviour
{
    public GameObject MissilePrefab;
    public float ShotInterval;

    private float LocalShotInterval;

    GameObject Local;
    SearchArea2 Sys;

    void Start()
    {
        LocalShotInterval = ShotInterval;

        Local = transform.parent.Find("SearchArea2").gameObject;
        Sys = Local.GetComponent<SearchArea2>();
    }

    void Update()
    {
        //連射速度
        LocalShotInterval = LocalShotInterval + 1 * Time.deltaTime;

        if (LocalShotInterval >= ShotInterval)
        {
            LocalShotInterval = ShotInterval;
        }

        // サーチエリアにプレイヤーが居る場合のみ発射
        if (Sys.getPosition() != null)
        {
            RaycastHit hit;

            if (Physics.Linecast(transform.position, Sys.getPosition(), out hit))
            {

                if (hit.transform.CompareTag("Player") || hit.transform.CompareTag("Shield") || hit.transform.CompareTag("Ally") )
                {
                    if (ShotInterval == LocalShotInterval)
                    {
                        //自分の位置とターゲットの位置の差分を取得
                        Vector3 relativePos = Sys.getPosition() - transform.position;

                        //よくわからん
                        Quaternion rotation = Quaternion.LookRotation(relativePos);

                        GameObject Missile = (GameObject)Instantiate(MissilePrefab, transform.position, rotation/*Quaternion.Euler(Sys.getPosition())*/);
                        Rigidbody MissileRb = Missile.GetComponent<Rigidbody>();

                        //射撃されてから2秒後に銃弾のオブジェクトを破壊する
                        Destroy(Missile, 10.0f);

                        LocalShotInterval = 0;
                    }
                }
            }
        }
    }
}
