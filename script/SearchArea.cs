using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchArea : MonoBehaviour
{

    int Flag = 0;
    //int DiffFlag = 0;

    Transform EnemyPos;
    Vector3 Position;
    Vector3 LocalPosition;
    Vector3 DiffPosition;
    Vector3 CorrectionPosition;

    private void OnTriggerStay(Collider other)
    {
        if ( other.transform.CompareTag("Enemy") /*&& Flag == 0*/ )
        {
            if ( Flag == 0 )
            {
                EnemyPos = other.GetComponent<Transform>();
                Position = EnemyPos.position;
            }

            Flag = 1;

            LocalPosition = EnemyPos.position;
        }

        float LocalPos1 = (LocalPosition - transform.position).magnitude;
        float LocalPos2 = (Position - transform.position).magnitude;

        if (LocalPos1 < 0)
        {
            LocalPos1 = LocalPos1 * -1;
        }

        if (LocalPos2 < 0)
        {
            LocalPos2 = LocalPos2 * -1;
        }

        if (LocalPos1 < LocalPos2)
        {
            Position = LocalPosition;
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
        DiffPosition = new Vector3(0, 0, 0);
        Position = new Vector3(0, 0, 0);
    }

    void Update()
    {
        CorrectionPosition = Position + (Position - DiffPosition) * (Position - transform.position).magnitude * 0.15f;

        DiffPosition = Position;
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
        return CorrectionPosition;
    }
}
