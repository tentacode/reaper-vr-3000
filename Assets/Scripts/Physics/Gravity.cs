using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Position))]
public class Gravity : MonoBehaviour {

    public Planet Planet;
    public Position Position;

    private Ray _ray;
    private Vector3? _previousPosition;

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
            transform.rotation = Quaternion.LookRotation(-(Position.PreviousPosition - Position.CurrentPosition), transform.up);

            UpdateRay();

            var hits = Physics.RaycastAll(_ray, 1).ToList();
            hits.RemoveAll(h => h.collider.gameObject.tag != "Planet");

            if (hits.Any())
            {
                var hit = hits.OrderBy(h => h.distance).First();
                transform.position = hit.point;
            }

            _previousPosition = transform.position;
        }
	}

    private void UpdateRay()
    {
        _ray.origin = transform.position + transform.up * 0.1f;
        _ray.direction = (Planet.transform.position - transform.position).normalized * Planet.Rayon;

        Debug.DrawRay(_ray.origin, _ray.direction * Planet.Rayon, Color.green, 0.2f);
    }
}
