using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Gravity : MonoBehaviour {

    public Planet Planet;
    public Position Position;
    public int Collisions;
    public EventHandler OnGrounded;
    public bool Grounded;

    private Ray _ray;

    // Use this for initialization
    void Start () {
        _ray = new Ray();

        Position = GetComponent<Position>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Planet != null)
        {
            transform.up = -(Planet.transform.position - transform.position).normalized;

            UpdateRay();

            var hits = Physics.RaycastAll(_ray, 1, LayerMask.GetMask("Planet")).ToList();

            if (hits.Any())
            {
                var hit = hits.OrderBy(h => h.distance).First();
                transform.position = hit.point;

                if (Position != null)
                {
                    Position.UpdatePosition();
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

            if (Position != null && Position.PreviousPosition != Position.CurrentPosition)
            {
                var movement = -(Position.PreviousPosition - Position.CurrentPosition);

                if (movement != Vector3.zero)
                {
                    transform.rotation = Quaternion.LookRotation(movement, transform.up);
                }
            }
        }
	}

    private void UpdateRay()
    {
        _ray.origin = Position.CurrentPosition + transform.up * 0.1f;
        _ray.direction = (Planet.transform.position - Position.CurrentPosition).normalized;

        if (Settings.Instance.Debug)
        {
            Debug.DrawRay(_ray.origin, _ray.direction * Planet.Rayon, Color.green, 0.2f);
        }
    }
}
