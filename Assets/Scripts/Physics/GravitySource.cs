﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GravitySource : MonoBehaviour {

    public float Gravity = -9.81f;

    public void Attract(Rigidbody body)
    {
        var gravityUp = (body.transform.position - transform.position).normalized;

        body.AddForce(gravityUp * Gravity);
    }

    public void UpdateOrientation(Transform body)
    {
        var gravityUp = (body.position - transform.position).normalized;

        var targetRotation = Quaternion.FromToRotation(body.up, gravityUp) * body.rotation;
        body.rotation = Quaternion.Slerp(body.rotation, targetRotation, 50 * Time.deltaTime);
        //body.transform.rotation.Set(targetRotation.x, targetRotation.y, targetRotation.z, targetRotation.w);
    }
}
