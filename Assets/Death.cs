using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Death : MonoBehaviour {

	public GameObject particles;

	// Use this for initialization
	void Start () {
		//Invoke ("Die", 3.0f);
	}
    void OnTriggerEnter(Collider collision)
    {
		if (collision.gameObject.tag == "Hand") {
			Die();
		}
    }

	void Die()
	{
		for (int i = 0; i < transform.childCount; i++) {
			Destroy (transform.GetChild (i).gameObject);
		}

		GameObject blood = Instantiate (particles, transform);
		GetComponent<PlaySound> ().Play();

		Destroy (blood, 2.0f);
		Destroy (gameObject, 2.0f);
	}
}
