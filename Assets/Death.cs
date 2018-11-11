using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Death : MonoBehaviour {

	public GameObject particles;
	public GameObject renderer;

	// Use this for initialization
	void Start () {
		Invoke ("Die", 3.0f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Die()
	{
		GameObject blood = Instantiate (particles, transform);
		GetComponent<PlaySound> ().Play();
		renderer.SetActive (false);

		Destroy (blood, 2.0f);
		Destroy (gameObject, 2.0f);
	}
}
