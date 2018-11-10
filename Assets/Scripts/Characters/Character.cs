using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Gravity))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CharacterAI))]
public class Character : MonoBehaviour {

    public Planet Planet;
    public Gravity Gravity;
    public Rigidbody Rigidbody;
    public CharacterAI CharacterAI;

    // Use this for initialization
    void Start () {
        Gravity = GetComponent<Gravity>();
        Rigidbody = GetComponent<Rigidbody>();
        CharacterAI = GetComponent<CharacterAI>();

        Gravity.OnGrounded += (s, e) =>
        {
            CharacterAI.enabled = true;
            Rigidbody.useGravity = false;

            transform.SetParent(Planet.transform);
        };
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UnGripped()
    {
        Gravity.enabled = true;
    }

    public void Gripped()
    {
        Gravity.enabled = false;
        CharacterAI.enabled = false;

        Gravity.Grounded = false;

        //Just for debug
        //Rigidbody.useGravity = true;
    }
}
