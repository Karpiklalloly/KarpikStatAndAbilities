using System;

namespace Karpik.StatAndAbilities.Codegen.Attributes;

public class StatAttribute : Attribute
{
    public const string AttributeName = "Stat";
}

public class RangeStatAttribute : Attribute
{
    public const string AttributeName = "RangeStat";
}