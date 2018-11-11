using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour {

    public Planet Planet;
    public Character CharacterPrefab;
    public int Nb;

    public CharacterGlassesCollection CharacterGlassesCollection;
    public CharacterHatCollection CharacterHatCollection;
    public CharacterMouthCollection CharacterMouthCollection;

    private List<string> _skinKeys;

    // Use this for initialization
    void Start () {
        _skinKeys = new List<string>();

        var points = PointsOnSphere(Nb);
        for (int i = 0; i < Nb; i++)
        {
            var c = Instantiate(CharacterPrefab, Planet.transform);
            c.transform.localPosition = points[i];
            c.transform.Rotate(0, Random.Range(0, 360), 0);

            var gravityScript = c.GetComponent<Gravity>();
            gravityScript.Planet = Planet;

            var aiScript = c.GetComponent<CharacterAI>();
            aiScript.Planet = Planet;

            var cScript = c.GetComponent<Character>();
            cScript.Planet = Planet;

            var sScript = c.GetComponent<CharacterSkin>();

            GenerateSkin(sScript);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void GenerateSkin(CharacterSkin skin)
    {
        var foundSkin = false;

        while(!foundSkin)
        {
            int glasses = Random.Range(-1, CharacterGlassesCollection.List.Count);
            int hat = Random.Range(-1, CharacterHatCollection.List.Count);
            int mouth = Random.Range(-1, CharacterMouthCollection.List.Count);

            //Si femme pas de moustache
            if(hat == 0 && mouth == 0)
            {
                continue;
            }

            var key = GenerateKey(glasses, hat, mouth);

            if(_skinKeys.Contains(key))
            {
                continue;
            }

            skin.Key = key;
            _skinKeys.Add(key);

            if(glasses != -1)
            {
                Instantiate(CharacterGlassesCollection.List[glasses], skin.transform);
            }

            if (hat != -1)
            {
                Instantiate(CharacterHatCollection.List[hat], skin.transform);
            }

            if (mouth != -1)
            {
                Instantiate(CharacterMouthCollection.List[mouth], skin.transform);
            }

            foundSkin = true;
        }
    }

    private string GenerateKey(int glasses, int hat, int mouth)
    {
        return glasses + "-" + hat + "-" + mouth;
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
