using System.Collections.Generic;

namespace Karpik.StatAndAbilities.Codegen
{
    public static class EzRangeStatGenerator
    {
        public static List<(string, string)> Generate(string structName, string namespaceName, string accessibility = "public")
        {
            return new List<(string, string)>
            {
                GenerateEzRangeStat(structName, namespaceName, accessibility),
                GenerateEzRangeStatExtensions(structName, namespaceName)
            };
        }
        
        private static (string, string) GenerateEzRangeStat(string name, string namespaceName, string accessibility = "public")
        {
            var source = 
                $@"using Karpik.StatAndAbilities;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
                
namespace {namespaceName}
{{
    [Serializable]
    {accessibility} partial struct {name} : IEzRangeStat
    {{
        public float Value
        {{
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => ValueStat.BaseValue;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {{
                if (value < MinStat.ModifiedValue)
                {{
                    value = MinStat.ModifiedValue;
                }}
                else if (value > MaxStat.ModifiedValue)
                {{
                    value = MaxStat.ModifiedValue;
                }}
                ValueStat.BaseValue = value;
            }}
        }}

        private DefaultStat ValueStat;
        public DefaultStat MinStat;
        public DefaultStat MaxStat;
        
        public void Init()
        {{
            ValueStat.Init();
            MinStat.Init();
            MaxStat.Init();
        }}
        
        public void DeInit()
        {{
            ValueStat.DeInit();
            MinStat.DeInit();
            MaxStat.DeInit();
        }}
    }}
}}";
            
            return ($"{name}.EzRangeStat.g.cs", source);
        }
        
        private static (string, string) GenerateEzRangeStatExtensions(string name, string namespaceName)
        {
            var source = 
                $@"using Karpik.StatAndAbilities;
using System;
using System.Runtime.CompilerServices;

namespace {namespaceName}
{{
    public static partial class {name}Extensions
    {{
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ApplyEffect(ref this {name} stat, Effect effect, BuffEzRange buff)
        {{
            if (buff.Flagged(BuffEzRange.Min)) stat.MinStat.ApplyEffect(effect);
            if (buff.Flagged(BuffEzRange.Max)) stat.MaxStat.ApplyEffect(effect);
            stat.Value = stat.Value;
        }}
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ApplyBuffInstantly(ref this {name} stat, Buff buff, BuffEzRange buffRange)
        {{
            if (buffRange.Flagged(BuffEzRange.Min)) stat.MinStat.ApplyBuffInstantly(buff);
            if (buffRange.Flagged(BuffEzRange.Max)) stat.MaxStat.ApplyBuffInstantly(buff);
            stat.Value = stat.Value;
        }}
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RemoveEffect(ref this {name} stat, Effect effect, BuffEzRange buff = BuffEzRange.All)
        {{
            if (buff.Flagged(BuffEzRange.Min)) stat.MinStat.RemoveEffect(effect);
            if (buff.Flagged(BuffEzRange.Max)) stat.MaxStat.RemoveEffect(effect);
            stat.Value = stat.Value;
        }}
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RemoveEffect(ref this {name} stat, string name, BuffEzRange buff = BuffEzRange.All)
        {{
            if (buff.Flagged(BuffEzRange.Min)) stat.MinStat.RemoveEffect(name);
            if (buff.Flagged(BuffEzRange.Max)) stat.MaxStat.RemoveEffect(name);
            stat.Value = stat.Value;
        }}
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ClearEffects(ref this {name} stat)
        {{
            stat.MinStat.ClearEffects();
            stat.MaxStat.ClearEffects();
            stat.Value = stat.Value;
        }}
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Span<Buff> BuffsMin(ref this {name} stat) => stat.MinStat.Buffs();
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Span<Buff> BuffsMax(ref this {name} stat) => stat.MaxStat.Buffs();
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ActualizeEffects(ref this {name} stat)
        {{
            stat.MinStat.ActualizeEffects();
            stat.MaxStat.ActualizeEffects();
            stat.Value = stat.Value;
        }}
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Min(ref this {name} stat) => stat.MinStat.BaseValue;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Max(ref this {name} stat) => stat.MaxStat.BaseValue;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float MinModified(ref this {name} stat) => stat.MinStat.ModifiedValue;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float MaxModified(ref this {name} stat) => stat.MaxStat.ModifiedValue;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int IsOnTheEdge(ref this {name} stat)
        {{
            if (stat.Value == stat.MinStat.ModifiedValue) return -1;
            if (stat.Value == stat.MaxStat.ModifiedValue) return 1;
            return 0;
        }}
    }}
}}";
            return ($"{name}.EzRangeStat.Extensions.g.cs", source);
        }
    }
}