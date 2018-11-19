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
    public float TimeBetweenCollisionCheck = 0.5f;
    public float CurrentTimeBetweenCollisionCheck = 0f;

    private Ray _ray;
    private float _timer;
    private int _layerMask;

    // Use this for initialization
    void Start () {
        _ray = new Ray();
        _layerMask = LayerMask.GetMask("Obstacle");
    }
	
	// Update is called once per frame
	public void UpdateCollision() {
        _timer -= Time.deltaTime;

        if (_timer <= 0)
        {
            NewTarget();
        }

        CurrentTimeBetweenCollisionCheck -= Time.deltaTime;

        if(CurrentTimeBetweenCollisionCheck <= 0)
        {
            UpdateRay();

            var hit = Physics.Raycast(_ray, 0.1f, _layerMask);

            if (hit)
            {
                transform.Rotate(0, 90, 0);
            }

            CurrentTimeBetweenCollisionCheck = TimeBetweenCollisionCheck;
        }             
    }

    /*
    private void FixedUpdate()
    {
        Move(Time.deltaTime);
    }
    */

    private void UpdateRay()
    {
        _ray.origin = transform.position + transform.up * 0.05f;
        _ray.direction = transform.forward;

        if (Settings.Instance.Debug)
        {
            Debug.DrawRay(_ray.origin, _ray.direction * 0.1f, Color.yellow, Time.deltaTime * 2);
        }
    }

    public void Move(float time)
    {
        var newPos = transform.position + transform.forward * Speed * time;

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
