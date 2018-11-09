using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Gravity : MonoBehaviour {

    public Planet Planet;

    private Ray _ray;

    // Use this for initialization
    void Start () {
        _ray = new Ray();
    }
	
	// Update is called once per frame
	void Update () {
        if (Planet != null)
        {
            transform.up = Planet.transform.position - transform.position;

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
        _ray.origin = transform.position;
        _ray.direction = Planet.transform.position - transform.position;
    }
}
