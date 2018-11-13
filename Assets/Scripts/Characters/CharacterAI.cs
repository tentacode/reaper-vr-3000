using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Character))]
public class CharacterAI : MonoBehaviour {

    public float Speed;
    public Planet Planet;
    public Vector3 Target;

    private Ray _ray;

    // Use this for initialization
    void Start () {
        _ray = new Ray();

    }
	
	// Update is called once per frame
	void FixedUpdate() {
        if(Target == Vector3.zero || (Target - transform.localPosition).magnitude < Planet.transform.localScale.x / 10f)
        {
            NewTarget();
        }

        if (Target != null)
        {
            Move();
        }

        UpdateRay();

        var hits = Physics.RaycastAll(_ray, 1, LayerMask.GetMask("Obstacle")).ToList();
        //hits.RemoveAll(h => h.collider.gameObject.tag != "Obstacle" && h.collider.gameObject.tag != "Planet");

        if (hits.Any())
        {
            transform.Rotate(0, 90, 0);

            UpdateRay();

            NewTarget();
        }

        //transform.Translate(_ray.direction * Time.deltaTime * 0.1f, Space.World);

        if (Target != Vector3.zero && Settings.Instance.Debug)
        {
            Debug.DrawLine(Target + Planet.transform.position, Target + Planet.transform.position + (Target - Vector3.zero).normalized * 0.5f, Color.cyan);
        }
    }

    private void UpdateRay()
    {
        _ray.origin = transform.position + transform.up * 0.05f;
        _ray.direction = transform.forward;

        if (Settings.Instance.Debug)
        {
            Debug.DrawRay(_ray.origin, _ray.direction * 0.2f, Color.yellow, Time.deltaTime * 2);
        }
    }

    private void Move()
    {
        Vector3 newPos = transform.localPosition;
        Vector3 travelDir = (Target - transform.localPosition).normalized;

        if (travelDir != Vector3.zero)
        {
            transform.up = -(Vector3.zero - transform.localPosition).normalized;
            //transform.localRotation = Quaternion.LookRotation(travelDir, transform.up);
        }

        //move forward by our speed
        newPos += transform.forward * (Time.fixedDeltaTime * Speed);

        //turning the position into a direction from the centre
        //newPos.Normalize();
        //newPos *= Planet.Rayon;

        //var previousPosition = transform.localPosition;
        transform.localPosition = newPos;

        //var movement = newPos - previousPosition;

        
    }

    private void NewTarget()
    {
        if (Planet != null)
        {
            Target = RandomSpherePoint(
                0,
                0,
                0,
                Planet.Rayon
                );
        }

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
