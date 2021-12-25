
using System;
using UnityEngine;

[Serializable]
public class Vector3Reference : BaseTypeReference<Vector3>
{
    public Vector3Variable Variable;
    public Vector3 Value { get { return UseConstant ? ConstantValue : Variable.Value; } }
}
