
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RotateEarth : MonoBehaviour
{
	Rigidbody rb;
	Ray ray;
	public float torqueSpeed;
	public float minimumGrabDistance = 0.1f;
	public GameObject leftHand;
	public GameObject rightHand;

	GameObject grabbedBy;
	GameObject grabbed;
	Vector3 lastGrabbedPosition;

	// Use this for initialization
	void Start()
	{
		ray = new Ray ();
		rb = GetComponent<Rigidbody> ();
	}

	public void GrabLeft()
	{
		if (grabbed != null) {
			return;
		}

		UpdateRay (leftHand);

		if (Settings.Instance.Debug)
		{
			Debug.DrawRay(ray.origin, ray.direction * minimumGrabDistance, Color.magenta, 1.0f);
		}

		var hits = Physics.RaycastAll(ray, minimumGrabDistance, LayerMask.GetMask("Planet", "Character")).ToList();

		if (hits.Any())
		{
			Debug.Log ("Hit");
			RaycastHit hit = hits.OrderBy(h => h.distance).First();
			grabbed = hit.transform.gameObject;
			grabbedBy = leftHand;
			lastGrabbedPosition = grabbedBy.transform.position;
		}
	}

	public void DeGrabLeft()
	{
		if (grabbedBy == leftHand) {
			grabbed = null;
			grabbedBy = null;
		}
	}


	void UpdateRay(GameObject obj)
	{
		ray.origin = obj.transform.position;
		ray.direction = obj.transform.forward;
	}


	void Update()
	{
		if (grabbed && grabbed.layer == LayerMask.NameToLayer("Planet")) {
			var currentGrabbedPosition = grabbedBy.transform.position;
			var lastDirection = lastGrabbedPosition - this.transform.position;
			var currentDirection = currentGrabbedPosition - this.transform.position;

			float angleX_before = Vector3.Angle (lastDirection, Vector3.down);
			float angleY_before = Vector3.Angle (lastDirection, Vector3.right);

			float angleX_after = Vector3.Angle (currentDirection, Vector3.down);
			float angleY_after = Vector3.Angle (currentDirection, Vector3.right);

			var pivot = new Vector3 (
				angleX_after - angleX_before,
				angleY_after - angleY_before, 
				0
			);

			transform.Rotate (pivot, Space.World);

			lastGrabbedPosition = grabbedBy.transform.position;
		}
	}
}
