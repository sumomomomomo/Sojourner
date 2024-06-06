using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FloatReference
{
    [SerializeField] private bool useConstant = true;
    [SerializeField] private float constantValue;
    [SerializeField] private FloatVariable variable;

    public float Value
    {
        get { return useConstant ? constantValue : variable.Value; }
        set { variable.Value = value; }
    }

}
