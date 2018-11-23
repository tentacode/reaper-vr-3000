﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{

	void Awake()
	{
		GameObject[] objs = GameObject.FindGameObjectsWithTag("VR");

		if (objs.Length > 1)
		{
			Destroy(this.gameObject);
		}

		DontDestroyOnLoad(this.gameObject);
	}
}
