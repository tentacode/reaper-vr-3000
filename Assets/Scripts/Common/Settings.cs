using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class Settings : MonoBehaviour
{
    private static Settings _instance;

    public static Settings Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = (Settings)FindObjectOfType(typeof(Settings));
            }

            return _instance;
        }
    }

    public bool Debug;
}

