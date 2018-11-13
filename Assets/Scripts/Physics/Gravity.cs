using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Gravity : MonoBehaviour {

    public Planet Planet;
    public Position Position;
    public Rigidbody Rigidbody;
    public int Collisions;
    public EventHandler OnGrounded;
    public bool Grounded;

    private Ray _ray;
    private Ray _rayInverse;

    // Use this for initialization
    void Start () {
        _ray = new Ray();
        _rayInverse = new Ray();

        Position = GetComponent<Position>();
        Rigidbody = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (Planet != null)
        {
            Rigidbody.AddForce((Planet.transform.position - transform.position).normalized * 0.1f);

            UpdateRay();
            
            var hits = Physics.RaycastAll(_ray, 1, LayerMask.GetMask("Planet")).ToList();
            var hitsInverse = Physics.RaycastAll(_rayInverse, 1, LayerMask.GetMask("Planet")).ToList();

            if (!hits.Any() && hitsInverse.Any())
            {
                var hit = hitsInverse.OrderByDescending(h => h.distance).First();
                transform.position = hit.point;

                if (Position != null)
                {
                    Position.ResetPosition();
                }

                if(!Grounded && OnGrounded != null)
                {
                    OnGrounded.Invoke(this, null);
                }

                Grounded = true;
            }
            else
            {
                Grounded = false;
            }


            Collisions = hits.Count;
            

            

            /*
            if (Position != null && Position.PreviousPosition != Position.CurrentPosition)
            {
                var movement = -(Position.PreviousPosition - Position.CurrentPosition);

                if (movement != Vector3.zero)
                {
                    transform.rotation = Quaternion.LookRotation(movement, transform.up);
                }
            }
            */
        }
	}

    private void UpdateRay()
    {
        _ray.origin = Position.CurrentPosition;
        _ray.direction = (Planet.transform.position - Position.CurrentPosition).normalized;

        if (Settings.Instance.Debug)
        {
            Debug.DrawRay(_ray.origin, _ray.direction * Planet.Rayon, Color.green, 0.2f);
        }

        _rayInverse.origin = Position.CurrentPosition;
        _rayInverse.direction = -_ray.direction;

        if (Settings.Instance.Debug)
        {
            Debug.DrawRay(_rayInverse.origin, _rayInverse.direction * Planet.Rayon, Color.gray, 0.2f);
        }
    }
}
