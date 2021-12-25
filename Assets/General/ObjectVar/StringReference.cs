
using System;

[Serializable]
public class StringReference : BaseTypeReference<string>
{
    public StringVariable Variable;
    public string Value { get { return UseConstant ? ConstantValue : Variable.Value; } }
}