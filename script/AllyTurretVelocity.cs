using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyTurretVelocity : MonoBehaviour
{
    Rigidbody rigidbody_a;

    void Start()
    {
        rigidbody_a = GetComponent<Rigidbody>();
    }

    void Update()
    {
        rigidbody_a.velocity = Vector3.zero;
    }
}
