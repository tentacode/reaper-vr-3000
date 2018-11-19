using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GravityBody))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CharacterAI))]
public class Character : MonoBehaviour {

    public Planet Planet;
    public GravityBody GravityBody;
    public Rigidbody Rigidbody;
    public CharacterAI CharacterAI;

    // Use this for initialization
    void Start () {
        GravityBody = GetComponent<GravityBody>();
        Rigidbody = GetComponent<Rigidbody>();
        CharacterAI = GetComponent<CharacterAI>();

        //GravityBody.OnGrounded += (s, e) =>
        //{
        //    CharacterAI.enabled = true;
        //    Rigidbody.useGravity = false;

        //    transform.SetParent(Planet.transform);
        //};
    }
	
	// Update is called once per frame

    public void UnGripped()
    {
        GravityBody.enabled = true;
    }

    public void Gripped()
    {
        GravityBody.enabled = false;
        CharacterAI.enabled = false;

        //GravityBody.Grounded = false;

        //Just for debug
        //Rigidbody.useGravity = true;
    }
}
