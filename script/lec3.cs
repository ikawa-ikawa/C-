using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lec3 : MonoBehaviour
{
    float angle_x;
    float angle_y;
    float angle_z;

    Rigidbody JETrb;

    void Start()
    {
        angle_x = 0f;
        angle_y = 0f;
        angle_z = 0f;

        JETrb = GameObject.Find("JET").GetComponent<Rigidbody>();
    }

    void Update()
    {

        angle_x = JETrb.transform.localEulerAngles.x;
        angle_y = JETrb.transform.localEulerAngles.y;
        angle_z = JETrb.transform.localEulerAngles.z;

        transform.localEulerAngles = new Vector3(0, angle_y, 0);

    }
}
