
using System;
using UnityEngine;

[Serializable]
public class GameObjectReference : BaseTypeReference<GameObject>
{
    public GameObjectVariable Variable;
    public GameObject Value { get { return UseConstant ? ConstantValue : Variable.Value; } }
}
