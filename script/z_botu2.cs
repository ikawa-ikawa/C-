using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class imagerotate : MonoBehaviour
{
    float angle_x;
    float angle_y;
    float angle_z;

    float nowangle;
    float angle;
    float goalangle;

    // Start is called before the first frame update
    void Start()
    {
        angle_x = 0f;
        angle_y = 0f;
        angle_z = 0f;

        nowangle = 0f;
        angle = 0f;
        goalangle = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody JETrb = GameObject.Find("JET").GetComponent<Rigidbody>();

        angle_x = JETrb.transform.localEulerAngles.x - 270f;
        angle_y = JETrb.transform.localEulerAngles.y;
        angle_z = JETrb.transform.localEulerAngles.z;

        Debug.Log("( " + angle_x + ", " + angle_y + ", " + angle_z  + " )");
        Debug.Log(JETrb.transform.right.x);

        angle = JETrb.transform.right.y;

        if (goalangle - 1f <= nowangle && nowangle <= goalangle + 1f)
        {
            nowangle = goalangle;
        }

        transform.localEulerAngles = new Vector3(0, 0, nowangle);


        if (angle_y <= 180 && angle_z >= 180 || angle_y > 180 && angle_z > 180)
        {

            if(angle_x > 0)
            {
                goalangle = angle * 90;
                //transform.localEulerAngles = new Vector3(0, 0, angle * 90);
            }
            else
            {
                goalangle = ( 1 - angle ) * 90 - 90;
                //transform.localEulerAngles = new Vector3(0, 0, ( 1 - angle ) * 90 - 90);
                goalangle = goalangle - 180;
            }

            if( nowangle < goalangle )
            {
                nowangle = nowangle + 1f;
            }
            else
            {
                nowangle = nowangle - 1f;
            }
            
        }
        else
        {

            if (angle_x > 0)
            {
                goalangle = angle * 90;
                //transform.localEulerAngles = new Vector3(0, 0, angle * 90);
            }
            else
            {
                goalangle = ( 1 -  angle) * 90 + 90;
                //transform.localEulerAngles = new Vector3(0, 0, 90 + (1 - angle) * 90);
            }

            if (nowangle < goalangle)
            {
                nowangle = nowangle + 1f;
            }
            else
            {
                nowangle = nowangle - 1f;
            }

        }

        

        


    }
}
