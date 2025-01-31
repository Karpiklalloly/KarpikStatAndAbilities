using System;
using Karpik.StatAndAbilities.Codegen.Attributes;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MoveReplace;

namespace Karpik.StatAndAbilities.Codegen;

[Generator]
public class GenStatStruct : ISourceGenerator
{
    public const string Stat = StatAttribute.AttributeName;
    public const string RangeStat = RangeStatAttribute.AttributeName;
    
    public void Initialize(GeneratorInitializationContext context)
    {
        context.RegisterForSyntaxNotifications(() => new StructWithStatReceiver());
    }

    public void Execute(GeneratorExecutionContext context)
    {
        if (context.SyntaxReceiver is not StructWithStatReceiver receiver) return;

        Span<StructDeclarationSyntax> structs = receiver.Structs.ToArray();
        foreach (var s in structs)
        {
            if (s.HasAttribute(Stat))
            {
                StatGenerator.Generate(context, s);
            }
            else if (s.HasAttribute(RangeStat))
            {
                RangeStatGenerator.Generate(context, s);
            }
        }
    }
}