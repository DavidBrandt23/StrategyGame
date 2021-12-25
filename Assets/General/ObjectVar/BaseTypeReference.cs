using System;

[Serializable]
public class BaseTypeReference<T>
{
    public bool UseConstant = true;
    public T ConstantValue;
}
