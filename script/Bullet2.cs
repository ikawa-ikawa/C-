using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet2 : MonoBehaviour
{

    public GameObject ExplosionPrefab;
    public int AttackPower;
    public int ErrorRange;

    //何かにぶつかった時に呼ばれる
    void OnCollisionEnter(Collision other)
    {
        Instantiate(ExplosionPrefab, this.transform.position, Quaternion.identity);

        transform.localScale = Vector3.zero;//見えない大きさにする

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
        
    }

    void Update()
    {
        
    }
}
