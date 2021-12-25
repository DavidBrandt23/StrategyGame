
using System;

[Serializable]
public class FloatReference : BaseTypeReference<float>
{
    public FloatVariable Variable;
    public float Value { get { return UseConstant ? ConstantValue : Variable.Value; } }
}