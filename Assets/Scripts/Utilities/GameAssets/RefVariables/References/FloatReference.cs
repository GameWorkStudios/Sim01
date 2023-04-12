using UnityEngine;
using System;
 
[Serializable]
public class FloatReference
{
    [SerializeField] private bool useConstant;
    [SerializeField] private float constantValue;
    [SerializeField] private FloatVariable variable;

    public float Value{
        get{
            return useConstant ? constantValue : variable.value;
        }
    }

}

