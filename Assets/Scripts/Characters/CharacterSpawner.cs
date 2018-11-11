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
    public CharacterColorCollection CharacterColorCollection;
    public Material TeeShirt;

    private List<CharacterSkinKey> _skinKeys;

    // Use this for initialization
    void Start () {
        _skinKeys = new List<CharacterSkinKey>();

        GenerateKeys();

        Debug.Log(_skinKeys.Count + " combinaisons générées");

        if(Nb > _skinKeys.Count)
        {
            Nb = _skinKeys.Count;

            Debug.Log("Trop de personnages par rapport au nombre de combinaisons générées");
        }

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
        var index = Random.Range(0, _skinKeys.Count);
        var key = _skinKeys[index];
        _skinKeys.RemoveAt(index);

        if(key.GlassesId != -1)
        {
            Instantiate(CharacterGlassesCollection.List[key.GlassesId], skin.transform);
        }

        if (key.HatId != -1)
        {
            Instantiate(CharacterHatCollection.List[key.HatId], skin.transform);
        }

        if (key.MouthId != -1)
        {
            Instantiate(CharacterMouthCollection.List[key.MouthId], skin.transform);
        }

        var material = Instantiate(TeeShirt);
        material.color = CharacterColorCollection.List[key.ColorId];
        skin.MeshRenderer.material = material;
    }

    private void GenerateKeys()
    {
        _skinKeys.Clear();

        for (int glasseIndex = -1; glasseIndex < CharacterGlassesCollection.List.Count; glasseIndex++)
        {
            for (int hatIndex = -1; hatIndex < CharacterHatCollection.List.Count; hatIndex++)
            {
                for (int mouthIndex = -1; mouthIndex < CharacterMouthCollection.List.Count; mouthIndex++)
                {
                    for (int colorIndex = 0; colorIndex < CharacterColorCollection.List.Count; colorIndex++)
                    {
                        //Si femme pas de moustache
                        if (hatIndex == 0 && mouthIndex == 0)
                        {
                            continue;
                        }

                        _skinKeys.Add(new CharacterSkinKey()
                        {
                            GlassesId = glasseIndex,
                            HatId = hatIndex,
                            MouthId = mouthIndex,
                            ColorId = colorIndex
                        });
                    }
                }
            }
        }
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
