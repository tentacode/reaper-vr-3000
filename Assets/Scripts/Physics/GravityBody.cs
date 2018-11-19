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
    private bool _isGrounded;

    // Use this for initialization
    void Start () {
        Rigidbody = GetComponent<Rigidbody>();

        Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        Rigidbody.useGravity = false;

        _ray = new Ray();
        _resultRaycast = new RaycastHit();
    }
	
	// Update is called once per frame
	public void UpdatePhysics ()
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

                _isGrounded = true;
            }
            else
            {
                _isGrounded = false;
                Rigidbody.isKinematic = false;
            }
        }
    }

    public void UpdateGravity()
    {
        if(!_isGrounded)
        {
            ApplyGravity();
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
