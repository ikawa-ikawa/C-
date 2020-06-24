﻿using System.Collections;
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
        if (other.gameObject.name == "SearchArea")
        {

        }
        else
        {
            Instantiate(ExplosionPrefab, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
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
        
    }

    void Update()
    {
        //transform.position = transform.position + transform.forward * Speed * Time.deltaTime;
    }
}
