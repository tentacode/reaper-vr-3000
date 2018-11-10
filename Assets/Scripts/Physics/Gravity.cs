using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Position))]
public class Gravity : MonoBehaviour {

    public Planet Planet;
    public Position Position;

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

            var movement = -(Position.PreviousPosition - Position.CurrentPosition);

            if (movement != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(movement, transform.up);
            }

            UpdateRay();

            var hits = Physics.RaycastAll(_ray, 1).ToList();
            hits.RemoveAll(h => h.collider.gameObject.tag != "Planet");

            if (hits.Any())
            {
                var hit = hits.OrderBy(h => h.distance).First();
                transform.position = hit.point;
            }
        }
	}

    private void UpdateRay()
    {
        _ray.origin = transform.position + transform.up * 0.1f;
        _ray.direction = (Planet.transform.position - transform.position).normalized * Planet.Rayon;

        Debug.DrawRay(_ray.origin, _ray.direction * Planet.Rayon, Color.green, 0.2f);
    }
}
