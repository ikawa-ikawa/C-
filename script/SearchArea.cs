using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchArea : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if( !(other.tag == "Bullet") && !(other.tag == "Missile") && !(other.tag == "EnemyBullet") && !(other.tag == "Shield") && !(other.tag == "Object"))
        {
            if( other.transform.parent.tag == "Player" )
            {
                //プレイヤーの方向差分
                var PlayerDirection = other.transform.position - transform.parent.position;

                //よくわからん
                Quaternion Rotation = Quaternion.LookRotation(PlayerDirection);

                //相手の方を向く
                transform.parent.rotation = Quaternion.Slerp(transform.parent.rotation, Rotation, 0.5f * Time.deltaTime);
            }
            else if (other.transform.parent.transform.parent.tag == "Player")
            {
                //プレイヤーの方向差分
                var PlayerDirection = other.transform.position - transform.parent.position;

                //よくわからん
                Quaternion Rotation = Quaternion.LookRotation(PlayerDirection);

                //相手の方を向く
                transform.parent.rotation = Quaternion.Slerp(transform.parent.rotation, Rotation, 0.5f * Time.deltaTime);

            }  
        }
        
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
