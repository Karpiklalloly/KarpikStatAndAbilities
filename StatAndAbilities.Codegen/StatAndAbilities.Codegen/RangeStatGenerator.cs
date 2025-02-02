using System.Collections.Generic;

namespace Karpik.StatAndAbilities.Codegen
{
    public static class RangeStatGenerator
    {
        public static List<(string, string)> Generate(string structName, string namespaceName, string accessibility = "public")
        {
            return new List<(string, string)>
            {
                GenerateRangeStat(structName, namespaceName, accessibility),
                GenerateRangeStatExtensions(structName, namespaceName)
            };
        }
    
        private static (string, string) GenerateRangeStat(string name, string namespaceName, string accessibility = "public")
        {
            var source = 
                $@"using Karpik.StatAndAbilities;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
                
namespace {namespaceName}
{{
    [Serializable]
    {accessibility} partial struct {name} : IRangeStat
    {{
        public float BaseValue
        {{
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => ValueStat.BaseValue;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => ValueStat.BaseValue = value;
        }}

        public float ModifiedValue
        {{
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => ValueStat.ModifiedValue;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => ValueStat.ModifiedValue = value;
        }}

        public DefaultStat ValueStat;
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
            
            return ($"{name}.RangeStat.g.cs", source);
        }
    
        private static (string, string) GenerateRangeStatExtensions(string name, string namespaceName)
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
        public static void ApplyEffect(ref this {name} stat, Effect effect, BuffRange buff)
        {{
            if (buff.Flagged(BuffRange.Min)) stat.MinStat.ApplyEffect(effect);
            if (buff.Flagged(BuffRange.Max)) stat.MaxStat.ApplyEffect(effect);
            if (buff.Flagged(BuffRange.Value)) stat.ValueStat.ApplyEffect(effect);
        }}
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ApplyBuffInstantly(ref this {name} stat, Buff buff, BuffRange buffRange)
        {{
            if (buffRange.Flagged(BuffRange.Min)) stat.MinStat.ApplyBuffInstantly(buff);
            if (buffRange.Flagged(BuffRange.Max)) stat.MaxStat.ApplyBuffInstantly(buff);
            if (buffRange.Flagged(BuffRange.Value)) stat.ValueStat.ApplyBuffInstantly(buff);
        }}
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RemoveEffect(ref this {name} stat, Effect effect, BuffRange buff = BuffRange.All)
        {{
            if (buff.Flagged(BuffRange.Min)) stat.MinStat.RemoveEffect(effect);
            if (buff.Flagged(BuffRange.Max)) stat.MaxStat.RemoveEffect(effect);
            if (buff.Flagged(BuffRange.Value)) stat.ValueStat.RemoveEffect(effect);
        }}
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RemoveEffect(ref this {name} stat, string name, BuffRange buff = BuffRange.All)
        {{
            if (buff.Flagged(BuffRange.Min)) stat.MinStat.RemoveEffect(name);
            if (buff.Flagged(BuffRange.Max)) stat.MaxStat.RemoveEffect(name);
            if (buff.Flagged(BuffRange.Value)) stat.ValueStat.RemoveEffect(name);
        }}
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ClearEffects(ref this {name} stat)
        {{
            stat.MinStat.ClearEffects();
            stat.MaxStat.ClearEffects();
            stat.ValueStat.ClearEffects();
        }}
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Span<Buff> BuffsMin(ref this {name} stat) => stat.MinStat.Buffs();
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Span<Buff> BuffsMax(ref this {name} stat) => stat.MaxStat.Buffs();
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Span<Buff> BuffsValue(ref this {name} stat) => stat.ValueStat.Buffs();
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ActualizeEffects(ref this {name} stat)
        {{
            stat.MinStat.ActualizeEffects();
            stat.MaxStat.ActualizeEffects();
            stat.ValueStat.ActualizeEffects();
        }}
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Value(ref this {name} stat) => stat.ValueStat.BaseValue;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Min(ref this {name} stat) => stat.MinStat.BaseValue;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Max(ref this {name} stat) => stat.MaxStat.BaseValue;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float ValueModified(ref this {name} stat) => stat.ValueStat.ModifiedValue;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float MinModified(ref this {name} stat) => stat.MinStat.ModifiedValue;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float MaxModified(ref this {name} stat) => stat.MaxStat.ModifiedValue;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToBounds(ref this {name} stat)
        {{
            if (stat.ValueStat.ModifiedValue < stat.MinStat.ModifiedValue) stat.ValueStat.ModifiedValue = stat.MinStat.ModifiedValue;
            if (stat.ValueStat.ModifiedValue > stat.MaxStat.ModifiedValue) stat.ValueStat.ModifiedValue = stat.MaxStat.ModifiedValue;
        }}
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsOutOfBounds(ref this {name} stat)
        {{
            if (stat.ValueStat.ModifiedValue < stat.MinStat.ModifiedValue) return true;
            if (stat.ValueStat.ModifiedValue > stat.MaxStat.ModifiedValue) return true;
            return false;
        }}
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int IsOnTheEdge(ref this {name} stat)
        {{
            if (stat.ValueStat.ModifiedValue == stat.MinStat.ModifiedValue) return -1;
            if (stat.ValueStat.ModifiedValue == stat.MaxStat.ModifiedValue) return 1;
            return 0;
        }}
    }}
}}";
            return ($"{name}.Stat.Extensions.g.cs", source);
        }
    }
}