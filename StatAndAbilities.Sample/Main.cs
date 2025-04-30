namespace Karpik.StatAndAbilities.Sample;

public class Main
{
    public void Run()
    {
        var player = new Entity(1);
        float healthModifier = 10;
        ref var damage = ref player.AddStat<Damage>();
        ref var health = ref player.AddStat<Health>();
        
        var damageBuff = EffectBuilder.Start()
            .WithBuffs(new Buff(healthModifier, BuffType.Add));
        var healthBuff = EffectBuilder.Start()
            .WithBuffs(new Buff(healthModifier, BuffType.Add));
        
        damage.BaseValue = 10;
        
        health.MinStat.BaseValue = 0;
        health.MaxStat.BaseValue = 100;
        health.Value = 100;
        
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
                    var buff = healthBuff.Build();
                    health.ApplyEffect(buff, BuffEzRange.Max);
                    health.Value += healthModifier;
                    break;
                case ConsoleKey.K:
                    health.Value -= damage.ModifiedValue;
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
        
        Console.WriteLine($"Health = {health.Value}/{health.MinModified()}/{health.MaxModified()}");
        var onEdge = health.IsOnTheEdge();
        if (onEdge == -1) Console.WriteLine("Is dead");
    }
}