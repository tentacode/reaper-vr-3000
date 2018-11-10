using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Position : MonoBehaviour {

    public Vector3 PreviousPosition;
    public Vector3 CurrentPosition;

    // Use this for initialization
    void Start () {
        PreviousPosition = transform.position;
        CurrentPosition = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        PreviousPosition = CurrentPosition;
        CurrentPosition = transform.position;
    }
}
