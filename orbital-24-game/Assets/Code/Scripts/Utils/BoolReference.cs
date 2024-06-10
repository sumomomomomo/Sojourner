using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BoolReference
{
    [SerializeField] private bool useConstant = false;
    [SerializeField] private bool constantValue;
    [SerializeField] private BoolVariable variable;

    public bool Value
    {
        get { return useConstant ? constantValue : variable.Value; }
        set { variable.Value = value; }
    }

}
