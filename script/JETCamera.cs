using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JETCamera : MonoBehaviour
{
    GameObject JETCUBE;
    GameObject JET;
    /*
    private Vector3 prevPlayerPos;
    private Vector3 posVector;
    public float scale = 3.0f;
    public float cameraSpeed = 100.0f;
    */

    void Start()
    {
        JETCUBE = GameObject.Find("JETCUBE");
        JET = GameObject.Find("JET");
    }

    void LateUpdate()
    {



        if ( JET.GetComponent<JET>().getRollingFlagA() == 1 )
        {
            transform.eulerAngles = new Vector3( 10, 0, 0 );

            //transform.localPosition = new Vector3( transform.localPosition.x, transform.localPosition.y, transform.localPosition.z );
        }
        else
        {
            transform.rotation = JETCUBE.transform.rotation;
            transform.position = JETCUBE.transform.position;
        }

        //transform.eulerAngles = new Vector3(JET.transform.localEulerAngles.x + 10, JET.transform.eulerAngles.y, JET.transform.eulerAngles.z );
        /*
        Vector3 currentPlayerPos = JET.transform.position;

        Vector3 backVector = JET.transform.forward * -1;

        posVector = (backVector == Vector3.zero) ? posVector : backVector;

        Vector3 targetPos = currentPlayerPos + scale * posVector;

        targetPos.y = targetPos.y + 0.5f;

        this.transform.position = Vector3.Lerp(
            this.transform.position,
            targetPos,
            cameraSpeed * Time.deltaTime
        );

        this.transform.LookAt(JET.transform.position);
        prevPlayerPos = JET.transform.position;
        */
    }
}
