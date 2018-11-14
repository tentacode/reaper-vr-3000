using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GravityBody : MonoBehaviour {

    public Collider ColliderSource;
    public GravitySource GravitySource;
    public Rigidbody Rigidbody;

    private Ray _ray;
    private RaycastHit _resultRaycast;

    // Use this for initialization
    void Start () {
        Rigidbody = GetComponent<Rigidbody>();

        Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        Rigidbody.useGravity = false;

        _ray = new Ray();
        _resultRaycast = new RaycastHit();
    }
	
	// Update is called once per frame
	void Update ()
    {
        UpdateRay();

        if (ColliderSource != null)
        {
            if (ColliderSource.Raycast(_ray, out _resultRaycast, 0.1f))
            {
                transform.position = _resultRaycast.point;

                Rigidbody.isKinematic = true;
                Rigidbody.velocity = Vector3.zero;

                ApplyGravityOrientation();
            }
            else
            {
                Rigidbody.isKinematic = false;
                ApplyGravity();
            }
        }
        else
        {
            var hits = Physics.RaycastAll(_ray, 0.1f, LayerMask.GetMask("Planet")).ToList();

            if (hits.Any())
            {
                var hit = hits.OrderBy(h => h.distance).First();
                transform.position = hit.point;

                Rigidbody.isKinematic = true;
                Rigidbody.velocity = Vector3.zero;

                ApplyGravityOrientation();
            }
            else
            {
                Rigidbody.isKinematic = false;
                ApplyGravity();
            }
        }
    }

    private void ApplyGravity()
    {
        if (GravitySource != null)
        {
            GravitySource.Attract(Rigidbody);
            GravitySource.UpdateOrientation(transform);
        }
    }

    private void ApplyGravityOrientation()
    {
        if (GravitySource != null)
        {
            GravitySource.UpdateOrientation(transform);
        }
    }

    private void UpdateRay()
    {
        _ray.origin = transform.position + transform.up * 0.05f;
        _ray.direction = -transform.up;

        if (Settings.Instance.Debug)
        {
            Debug.DrawRay(_ray.origin, _ray.direction * 0.1f, Color.green, 0.2f);
        }
    }
}
