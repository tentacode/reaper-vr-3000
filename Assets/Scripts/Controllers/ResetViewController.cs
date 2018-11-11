using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetViewController : MonoBehaviour
{
	public GameObject world;
	public GameObject headset;
	public float distanceZ;
	public float distanceY;

	public void ResetView()
	{
		var direction = headset.transform.forward;
		Vector3 destination = (direction * distanceZ) + headset.transform.position;
		destination.y = headset.transform.position.y + distanceY;

		world.transform.position = destination;
	}
}
