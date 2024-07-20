using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StringReference
{
    [SerializeField] private bool useConstant = false;
    [SerializeField] private string constantValue;
    [SerializeField] private StringVariable variable;

    public string Value
    {
        get { return useConstant ? constantValue : variable.Value; }
        set { variable.Value = value; }
    }

}
