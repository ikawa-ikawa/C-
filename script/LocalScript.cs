using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalScript : MonoBehaviour
{
    public Rigidbody rbody;
    private float speed;
    private float radius;
    private float yPosition;

    // Start is called before the first frame update
    void Start()
    {
        speed = 0.05f;
        radius = 600.0f;
        yPosition = 80f;
    }

    // Update is called once per frame
    void Update()
    {
        rbody.MovePosition( new Vector3( radius * Mathf.Sin(Time.time * speed), yPosition, radius * Mathf.Cos(Time.time * speed) ) );
    }
}
