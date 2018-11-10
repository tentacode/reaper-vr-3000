
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateEarth : MonoBehaviour
{
	Rigidbody rb;
	public float torqueSpeed;
	public GameObject hand;

	// Use this for initialization
	void Start()
	{
		rb = GetComponent<Rigidbody> ();
	}

	public void Rotate()
	{
		Debug.Log ("Foo");
		rb.AddTorque (transform.up * torqueSpeed);
	}

	void Update()
	{
		
	}
}
