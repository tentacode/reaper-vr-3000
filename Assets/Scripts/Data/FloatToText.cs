using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class FloatToText : MonoBehaviour
{
    public Text Text;

    public FloatVariable Variable;

    public bool AlwaysUpdate;

    private void OnEnable()
    {
        Text.text = Variable.Value.ToString();
    }

    private void Update()
    {
        if (AlwaysUpdate)
        {
            Text.text = Variable.Value.ToString();
        }
    }
}
