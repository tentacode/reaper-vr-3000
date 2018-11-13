using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Planet : MonoBehaviour {
    
    public float Rayon;
    public Transform CharactersRoot;

    private List<CharacterAI> _characters;

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        //transform.Rotate(0, Time.deltaTime * 80, 0);
    }

    public void AddCharacter(CharacterAI character)
    {
        if(_characters == null)
        {
            _characters = new List<CharacterAI>();
        }

        _characters.Add(character);
    }
}
