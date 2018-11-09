using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Character))]
public class CharacterAI : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //transform.position = FindPoint(transform.position, 0.5f, Time.deltaTime/10f);

        var x = Random.Range(0f, 1f) * transform.forward;
        var y = Random.Range(0f, 1f) * transform.right;

        transform.Translate(transform.forward * Time.deltaTime * 0.1f, Space.World);
    }

    Vector3 FindPoint(Vector3 c, float r, float i)
    {
        var x = Random.Range(0f, 1f) * transform.forward;
        var y = Random.Range(0f, 1f) * transform.right;

        Debug.Log("============");
        Debug.Log(x);
        Debug.Log(y);

        return c + (transform.right);

        
    }
}
