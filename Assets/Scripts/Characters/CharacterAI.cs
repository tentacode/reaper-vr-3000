using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Character))]
public class CharacterAI : MonoBehaviour {

    public float Speed;
    public Planet Planet;
    public Vector3 Target;
    public Rigidbody Rigidbody;

    private Ray _ray;
    private float _timer;

    // Use this for initialization
    void Start () {
        _ray = new Ray();

    }
	
	// Update is called once per frame
	public void Update() {
        _timer -= Time.deltaTime;

        if (_timer <= 0)
        {
            NewTarget();
        }

        Move();

        UpdateRay();


        var hit = Physics.Raycast(_ray, 0.1f, LayerMask.GetMask("Obstacle"));

        if (hit)
        {
            transform.Rotate(0, 90, 0);
        }
    }

    private void UpdateRay()
    {
        _ray.origin = transform.position + transform.up * 0.05f;
        _ray.direction = transform.forward;

        if (Settings.Instance.Debug)
        {
            Debug.DrawRay(_ray.origin, _ray.direction * 0.1f, Color.yellow, Time.deltaTime * 2);
        }
    }

    public void Move()
    {
        //Vector3 newPos = transform.localPosition;
        //Vector3 travelDir = (Target - transform.localPosition).normalized;

        //if (travelDir != Vector3.zero)
        //{
        //    transform.up = -(Vector3.zero - transform.localPosition).normalized;
        //    transform.rotation = Quaternion.LookRotation(transform.TransformDirection(travelDir), transform.up);
        //}

        ////move forward by our speed
        //newPos += transform.forward * (Time.fixedDeltaTime * Speed);

        ////turning the position into a direction from the centre
        ////newPos.Normalize();
        ////newPos *= Planet.Rayon;

        ////var previousPosition = transform.localPosition;
        //transform.localPosition = newPos;

        ////var movement = newPos - previousPosition;

        var newPos = transform.position + transform.forward * Speed * Time.deltaTime;

        Rigidbody.MovePosition(newPos);
    }

    private void NewTarget()
    {
        if (Planet != null)
        {
            //Target = RandomSpherePoint(
            //    0,
            //    0,
            //    0,
            //    Planet.Rayon
            //    );
        }

        _timer = Random.Range(5, 10);

        var y = Random.Range(0, 360);

        transform.Rotate(0, y, 0);
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
