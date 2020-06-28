using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchArea2 : MonoBehaviour
{
    int Flag;


    Vector3 LocalPosition;
    Vector3 Position;

    Transform EnemyPos;

    float LocalPos1;
    float LocalPos2;

    private void OnTriggerStay(Collider other)
    {
        if( other.CompareTag("Player") || other.CompareTag("Ally") || other.CompareTag("Shield") )
        {
            if (Flag == 0)
            {
                EnemyPos = other.GetComponent<Transform>();
                Position = EnemyPos.position;
            }

            Flag = 1;
            LocalPosition = EnemyPos.position;

            LocalPos1 = (transform.position - LocalPosition ).magnitude;
            LocalPos2 = (transform.position- Position).magnitude;

            
            if (LocalPos1 < LocalPos2)
            {
                Position = LocalPosition;
            }

            if (other.CompareTag("Player") || other.CompareTag("Shield"))
            {
                Flag = 1;
                Position = other.transform.root.position;
            }
        }


    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Ally") || other.CompareTag("Shield"))
        {
            Flag = 0;
        }
    }

    public int getFlag()
    {
        return Flag;
    }


    void Start()
    {
        Flag = 0;
    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        Flag = 0;
    }

    public Vector3 getPosition()
    {
        return Position;
    }
}
