using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Planet : MonoBehaviour {
    
    public float Rayon;
    public Transform CharactersRoot;

    private CharacterAI[] _characterAIs;
    private GravityBody[] _characterBodies;
    private int _nb;
    private int _index;

    // Use this for initialization
    void Start () {
        Physics.autoSyncTransforms = false;
    }
	
	// Update is called once per frame
	void Update () {
        if(_characterAIs != null)
        {
            for(int i = 0; i < _index; i++)
            {
                _characterBodies[i].UpdatePhysics();
                _characterAIs[i].UpdateCollision();
            }
        }
    }

    void FixedUpdate()
    {
        if (_characterAIs != null)
        {
            for (int i = 0; i < _index; i++)
            {
                _characterBodies[i].UpdateGravity();
                _characterAIs[i].Move(Time.deltaTime);
            }
        }
    }

    public void SetNbCharacter(int nb)
    {
        _nb = nb;
        _characterAIs = new CharacterAI[nb];
        _characterBodies = new GravityBody[nb];
    }

    public void AddCharacter(CharacterAI character, GravityBody gb)
    {
        _characterAIs[_index] = character;
        _characterBodies[_index] = gb;
        _index++;
    }
}
