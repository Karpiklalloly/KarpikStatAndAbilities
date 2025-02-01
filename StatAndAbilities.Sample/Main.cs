using Karpik.StatAndAbilities;

namespace StatAndAbilities.Sample;

public class Main
{
    public void Run()
    {
        var player = new Entity(1);
        ref var damage = ref player.AddStat<Damage>();
        ref var health = ref player.AddStat<Health>();
        
        var damageBuff = EffectBuilder.Start()
            .WithBuffs(new Buff(10, BuffType.Add));
        var healthBuff = EffectBuilder.Start()
            .WithBuffs(new Buff(10, BuffType.Add));
        
        damage.BaseValue = 10;
        
        health.MinStat.BaseValue = 0;
        health.MaxStat.BaseValue = 100;
        health.ValueStat.BaseValue = 100;
        
        while (true)
        {
            UpdateDamage(player);
            UpdateHealth(player);
            
            var key = Console.ReadKey(true);
            switch (key.Key)
            {
                case ConsoleKey.Q:
                    return;
                case ConsoleKey.D:
                    damage.ApplyEffect(damageBuff.Build());
                    break;
                case ConsoleKey.H:
                    health.ApplyEffect(healthBuff.Build(), BuffRange.Value | BuffRange.Max);
                    break;
                case ConsoleKey.K:
                    health.ApplyBuffInstantly(new Buff(-damage.ModifiedValue, BuffType.Add), BuffRange.Value);
                    break;
            }
        }
    }

    private void UpdateDamage(Entity player)
    {
        ref var damage = ref player.GetStat<Damage>();
        damage.ActualizeEffects();
        
        Console.WriteLine($"Damage = {damage.ModifiedValue}");
    }
    
    private void UpdateHealth(Entity player)
    {
        ref var health = ref player.GetStat<Health>();
        health.ActualizeEffects();
        health.ToBounds();
        
        Console.WriteLine($"Health = {health.ValueModified()}/{health.MinModified()}/{health.MaxModified()}");
        var onEdge = health.IsOnTheEdge();
        if (onEdge == -1) Console.WriteLine("Is dead");
    }
}