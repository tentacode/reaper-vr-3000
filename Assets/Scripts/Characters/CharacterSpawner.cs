using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour {

    public Planet Planet;
    public Character CharacterPrefab;
    public int Nb;

    // Use this for initialization
    void Start () {
        var points = PointsOnSphere(Nb);
        for (int i = 0; i < Nb; i++)
        {
            var c = Instantiate(CharacterPrefab, Planet.transform);
            c.transform.position = points[i];
            c.transform.Rotate(0, Random.Range(0, 360), 0);

            var gravityScript = c.GetComponent<Gravity>();
            gravityScript.Planet = Planet;

            var aiScript = c.GetComponent<CharacterAI>();
            aiScript.Planet = Planet;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private List<Vector3> PointsOnSphere(int n)
    {
        List<Vector3> upts = new List<Vector3>();
        float inc = Mathf.PI * (3 - Mathf.Sqrt(5));
        float off = 2.0f / n;
        float x = 0;
        float y = 0;
        float z = 0;
        float r = Planet.Rayon;
        float phi = 0;

        for (var k = 0; k < n; k++)
        {
            y = k * off - 1 + (off / 2);
            r = Mathf.Sqrt(1 - y * y);
            phi = k * inc;
            x = Mathf.Cos(phi) * r;
            z = Mathf.Sin(phi) * r;

            upts.Add(new Vector3(x, y, z));
        }
        return upts;
    }
}
