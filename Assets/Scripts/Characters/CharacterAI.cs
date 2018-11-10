using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Character))]
public class CharacterAI : MonoBehaviour {

    public float Speed;
    public Planet Planet;
    public Vector3? Target;

    private Ray _ray;

    // Use this for initialization
    void Start () {
        _ray = new Ray();

    }
	
	// Update is called once per frame
	void Update () {
        if(Planet != null && (Target == null || (transform.position - Target).Value.magnitude < 0.1f))
        {
            NewTarget();
        }

        UpdateRay();

        var hits = Physics.RaycastAll(_ray, 1).ToList();
        hits.RemoveAll(h => h.collider.gameObject.tag != "Obstacle" && h.collider.gameObject.tag != "Planet");

        if (hits.Any())
        {
            transform.Rotate(0, 90, 0);

            UpdateRay();

            NewTarget();
        }

        //transform.Translate(_ray.direction * Time.deltaTime * 0.1f, Space.World);

        if (Target != null)
        {
            Move();
        }
    }

    private void UpdateRay()
    {
        _ray.origin = transform.position + transform.up * 0.1f;
        _ray.direction = transform.forward;

        Debug.DrawRay(_ray.origin, _ray.direction * 0.2f, Color.yellow, Time.deltaTime * 2);
    }

    private void Move()
    {
        Vector3 newPos = transform.position;
        Vector3 travelDir = (Target.Value - transform.position).normalized;

        //move forward by our speed
        newPos += travelDir * (Time.deltaTime * Speed);

        //turning the position into a direction from the centre
        newPos.Normalize();
        newPos *= Planet.Rayon;

        //transform.forward = newPos - transform.position;

        Vector3 gravityUp = (transform.position - Planet.transform.position).normalized;
        Vector3 localUp = transform.up;

        transform.position = newPos;
    }

    private void NewTarget()
    {
        Target = RandomSpherePoint(
            Planet.transform.position.x,
            Planet.transform.position.y,
            Planet.transform.position.z,
            Planet.Rayon
            );

    }

    private Vector3 RandomSpherePoint(float x0, float y0, float z0, float radius)
    {
        var u = Random.Range(0f, 1f);
        var v = Random.Range(0f, 1f);
        var theta = 2 * Mathf.PI * u;
        var phi = Mathf.Acos(2 * v - 1);
        var x = x0 + (radius * Mathf.Sin(phi) * Mathf.Cos(theta));
        var y = y0 + (radius * Mathf.Sin(phi) * Mathf.Sin(theta));
        var z = z0 + (radius * Mathf.Cos(phi));
        return new Vector3(x, y, z);
    }
}
