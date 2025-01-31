namespace StatSystemTest.StatTest;

public class TwoEffectsWithOneBuff
{
    private DefaultStat stat;

    [OneTimeSetUp]
    public void Awake()
    {
        StatPool<DefaultStat>.Instance.ClearAll();
        stat = StatPool<DefaultStat>.Instance.Add(1);
    }
    
    [SetUp]
    public void Setup()
    {
        stat.ClearEffects();
    }
    
    [Test]
    public void WhenSetAndSet_AndApply_ThenLastSet(
        [Values(-10, 100, 0)] float value1,
        [Values(100, 0, -10)] float value2)
    {
        //Action
        Buff buff1 = new Buff(value1, BuffType.Set);
        Buff buff2 = new Buff(value2, BuffType.Set);
        Effect effect1 = EffectBuilder.Start()
            .WithBuffs(buff1)
            .Build();
        Effect effect2 = EffectBuilder.Start()
            .WithBuffs(buff2)
            .Build();
        var startValue = stat.BaseValue = 1;

        //Condition
        stat.ApplyEffect(effect1);
        stat.ApplyEffect(effect2);
        stat.ActualizeEffects();

        //Result
        Assert.Multiple(() =>
        {
            Assert.That(stat.BaseValue, Is.EqualTo(startValue));
            Assert.That(stat.ModifiedValue, Is.EqualTo(value2));
        });
    }
    
    [Test]
    public void WhenSetAndAdd_AndApply_ThenSetPlusAdd(
        [Values(-10, 100, 0)] float value1,
        [Values(100, 0, -10)] float value2)
    {
        //Action
        Buff buff1 = new Buff(value1, BuffType.Set);
        Buff buff2 = new Buff(value2, BuffType.Add);
        Effect effect1 = EffectBuilder.Start()
            .WithBuffs(buff1)
            .Build();
        Effect effect2 = EffectBuilder.Start()
            .WithBuffs(buff2)
            .Build();
        var startValue = stat.BaseValue = 1;

        //Condition
        stat.ApplyEffect(effect1);
        stat.ApplyEffect(effect2);
        stat.ActualizeEffects();

        //Result
        Assert.Multiple(() =>
        {
            Assert.That(stat.BaseValue, Is.EqualTo(startValue));
            Assert.That(stat.ModifiedValue, Is.EqualTo(value1 + value2));
        });
    }
    
    [Test]
    public void WhenSetAndMultiply_AndApply_ThenSetMultMultiply(
        [Values(-10, 100, 0)] float value1,
        [Values(100, 0, -10)] float value2)
    {
        //Action
        Buff buff1 = new Buff(value1, BuffType.Set);
        Buff buff2 = new Buff(value2, BuffType.Multiply);
        Effect effect1 = EffectBuilder.Start()
            .WithBuffs(buff1)
            .Build();
        Effect effect2 = EffectBuilder.Start()
            .WithBuffs(buff2)
            .Build();
         
        var startValue = stat.BaseValue = 1;

        //Condition
        stat.ApplyEffect(effect1);
        stat.ApplyEffect(effect2);
        stat.ActualizeEffects();

        //Result
        Assert.Multiple(() =>
        {
            Assert.That(stat.BaseValue, Is.EqualTo(startValue));
            Assert.That(stat.ModifiedValue, Is.EqualTo(value1 * value2));
        });
    }
    
    [Test]
    public void WhenAddAndSet_AndApply_ThenSet(
        [Values(-10, 100, 0)] float value1,
        [Values(100, 0, -10)] float value2)
    {
        //Action
        Buff buff1 = new Buff(value1, BuffType.Add);
        Buff buff2 = new Buff(value2, BuffType.Set);
        Effect effect1 = EffectBuilder.Start()
            .WithBuffs(buff1)
            .Build();
        Effect effect2 = EffectBuilder.Start()
            .WithBuffs(buff2)
            .Build();
         
        var startValue = stat.BaseValue = 1;

        //Condition
        stat.ApplyEffect(effect1);
        stat.ApplyEffect(effect2);
        stat.ActualizeEffects();

        //Result
        Assert.Multiple(() =>
        {
            Assert.That(stat.BaseValue, Is.EqualTo(startValue));
            Assert.That(stat.ModifiedValue, Is.EqualTo(value2));
        });
    }
    
    [Test]
    public void WhenAddAndAdd_AndApply_ThenBasePlusAddPlusAdd(
        [Values(-10, 100, 0)] float value1,
        [Values(100, 0, -10)] float value2)
    {
        //Action
        Buff buff1 = new Buff(value1, BuffType.Add);
        Buff buff2 = new Buff(value2, BuffType.Add);
        Effect effect1 = EffectBuilder.Start()
            .WithBuffs(buff1)
            .Build();
        Effect effect2 = EffectBuilder.Start()
            .WithBuffs(buff2)
            .Build();
         
        var startValue = stat.BaseValue = 1;

        //Condition
        stat.ApplyEffect(effect1);
        stat.ApplyEffect(effect2);
        stat.ActualizeEffects();

        //Result
        Assert.Multiple(() =>
        {
            Assert.That(stat.BaseValue, Is.EqualTo(startValue));
            Assert.That(stat.ModifiedValue, Is.EqualTo(stat.BaseValue + value1 + value2));
        });
    }
    
    [Test]
    public void WhenAddAndMultiply_AndApply_ThenBasePlusAddMultMultiply(
        [Values(-10, 100, 0)] float value1,
        [Values(100, 0, -10)] float value2)
    {
        //Action
        Buff buff1 = new Buff(value1, BuffType.Add);
        Buff buff2 = new Buff(value2, BuffType.Multiply);
        Effect effect1 = EffectBuilder.Start()
            .WithBuffs(buff1)
            .Build();
        Effect effect2 = EffectBuilder.Start()
            .WithBuffs(buff2)
            .Build();
         
        var startValue = stat.BaseValue = 1;

        //Condition
        stat.ApplyEffect(effect1);
        stat.ApplyEffect(effect2);
        stat.ActualizeEffects();

        //Result
        Assert.Multiple(() =>
        {
            Assert.That(stat.BaseValue, Is.EqualTo(startValue));
            Assert.That(stat.ModifiedValue, Is.EqualTo((stat.BaseValue + value1) * value2));
        });
    }
    
    [Test]
    public void WhenMultiplyAndSet_AndApply_ThenSet(
        [Values(-10, 100, 0)] float value1,
        [Values(100, 0, -10)] float value2)
    {
        //Action
        Buff buff1 = new Buff(value1, BuffType.Multiply);
        Buff buff2 = new Buff(value2, BuffType.Set);
        Effect effect1 = EffectBuilder.Start()
            .WithBuffs(buff1)
            .Build();
        Effect effect2 = EffectBuilder.Start()
            .WithBuffs(buff2)
            .Build();
         
        var startValue = stat.BaseValue = 1;

        //Condition
        stat.ApplyEffect(effect1);
        stat.ApplyEffect(effect2);
        stat.ActualizeEffects();

        //Result
        Assert.Multiple(() =>
        {
            Assert.That(stat.BaseValue, Is.EqualTo(startValue));
            Assert.That(stat.ModifiedValue, Is.EqualTo(value2));
        });
    }
    
    [Test]
    public void WhenMultiplyAndAdd_AndApply_ThenBasMultMultiplyPlusAdd(
        [Values(-10, 100, 0)] float value1,
        [Values(100, 0, -10)] float value2)
    {
        //Action
        Buff buff1 = new Buff(value1, BuffType.Multiply);
        Buff buff2 = new Buff(value2, BuffType.Add);
        Effect effect1 = EffectBuilder.Start()
            .WithBuffs(buff1)
            .Build();
        Effect effect2 = EffectBuilder.Start()
            .WithBuffs(buff2)
            .Build();
         
        var startValue = stat.BaseValue = 1;

        //Condition
        stat.ApplyEffect(effect1);
        stat.ApplyEffect(effect2);
        stat.ActualizeEffects();

        //Result
        Assert.Multiple(() =>
        {
            Assert.That(stat.BaseValue, Is.EqualTo(startValue));
            Assert.That(stat.ModifiedValue, Is.EqualTo(stat.BaseValue * value1 + value2));
        });
    }
    
    [Test]
    public void WhenMultiplyAndMultiply_AndApply_ThenBasMultMultiplyMultMultiply(
        [Values(-10, 100, 0)] float value1,
        [Values(100, 0, -10)] float value2)
    {
        //Action
        Buff buff1 = new Buff(value1, BuffType.Multiply);
        Buff buff2 = new Buff(value2, BuffType.Multiply);
        Effect effect1 = EffectBuilder.Start()
            .WithBuffs(buff1)
            .Build();
        Effect effect2 = EffectBuilder.Start()
            .WithBuffs(buff2)
            .Build();
         
        var startValue = stat.BaseValue = 1;

        //Condition
        stat.ApplyEffect(effect1);
        stat.ApplyEffect(effect2);
        stat.ActualizeEffects();

        //Result
        Assert.Multiple(() =>
        {
            Assert.That(stat.BaseValue, Is.EqualTo(startValue));
            Assert.That(stat.ModifiedValue, Is.EqualTo(stat.BaseValue * value1 * value2));
        });
    }   
}