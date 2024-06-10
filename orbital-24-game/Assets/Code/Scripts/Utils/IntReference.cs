using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class IntReference
{
    [SerializeField] private bool useConstant = false;
    [SerializeField] private int constantValue;
    [SerializeField] private IntVariable variable;

    public int Value
    {
        get { return useConstant ? constantValue : variable.Value; }
        set { variable.Value = value; }
    }

}
