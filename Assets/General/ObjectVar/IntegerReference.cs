using System;

[Serializable]
public class IntegerReference : BaseTypeReference<int>
{
    public IntegerVariable Variable;
    public int Value { get { return UseConstant ? ConstantValue : Variable.Value; } }
}