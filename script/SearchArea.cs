using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchArea : MonoBehaviour
{

    int Flag = 0;

    Transform EnemyPos;
    Vector3 Position;

    private void OnTriggerStay(Collider other)
    {

        if ( other.transform.root.CompareTag("Enemy") && Flag == 0 )
        {
            Flag = 1;

            EnemyPos = other.GetComponent<Transform>();
            Position = EnemyPos.position;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.transform.root.CompareTag("Enemy"))
        {
            Flag = 0;
        }
    }

    void Start()
    {

    }

    void FixedUpdate()
    {
        Flag = 0;
    }

    public int getFlag()
    {
        return Flag;
    }

    public Vector3 getPosition()
    {
        return Position;
    }
}
