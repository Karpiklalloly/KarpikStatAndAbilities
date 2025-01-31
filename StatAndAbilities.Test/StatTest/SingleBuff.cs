namespace StatSystemTest.StatTest;

public class SingleBuff
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
    public void WhenCreateEffectWithSet_AndApplyToStat_ThenStatHasThisValue([Values(-10, 100, 0)] float value)
    {
        //Action
        Buff buff = new Buff(value, BuffType.Set);
        Effect effect = EffectBuilder.Start()
            .WithBuffs(buff)
            .Build();
        stat.BaseValue = 1;

        //Condition
        stat.ApplyEffect(effect);
        stat.ActualizeEffects();

        //Result
        Assert.That(stat.ModifiedValue, Is.EqualTo(value));
    }

    [Test]
    public void WhenCreateEffectWithAdd_AndApplyToStat_ThenStatHasDifferedValue([Values(-10, 100, 0)] float value)
    {
        //Action
        Buff buff = new Buff(value, BuffType.Add);
        Effect effect = EffectBuilder.Start()
            .WithBuffs(buff)
            .Build();
        stat.BaseValue = 100;

        //Condition
        stat.ApplyEffect(effect);
        stat.ActualizeEffects();

        //Result
        Assert.That(stat.ModifiedValue, Is.EqualTo(stat.BaseValue + value));
    }
    
    [Test]
    public void WhenCreateEffectWithMultiply_AndApplyToStat_ThenStatHasDifferedValue([Values(-10, 100, 0)] float value)
    {
        //Action
        Buff buff = new Buff(value, BuffType.Multiply);
        Effect effect = EffectBuilder.Start()
            .WithBuffs(buff)
            .Build();
        stat.BaseValue = 100;

        //Condition
        stat.ApplyEffect(effect);
        stat.ActualizeEffects();

        //Result
        Assert.That(stat.ModifiedValue, Is.EqualTo(stat.BaseValue * value));
    }
    
    [Test]
    public void WhenCreateEffectWithSetAndBase_AndApplyToStat_ThenStatHasThisValue([Values(-10, 100, 0)] float value)
    {
        //Action
        Buff buff = new Buff(value, BuffType.Set, true);
        Effect effect = EffectBuilder.Start()
            .WithBuffs(buff)
            .Build();
        stat.BaseValue = 1;

        //Condition
        stat.ApplyEffect(effect);
        stat.ActualizeEffects();

        //Result
        Assert.Multiple(() =>
        {
            Assert.That(stat.BaseValue, Is.EqualTo(value));
            Assert.That(stat.ModifiedValue, Is.EqualTo(value));
        });
    }

    [Test]
    public void WhenCreateEffectWithAddAndBase_AndApplyToStat_ThenStatHasDifferedValue([Values(-10, 100, 0)] float value)
    {
        //Action
        Buff buff = new Buff(value, BuffType.Add, true);
        Effect effect = EffectBuilder.Start()
            .WithBuffs(buff)
            .Build();
        var startValue = stat.BaseValue = 100;

        //Condition
        stat.ApplyEffect(effect);
        stat.ActualizeEffects();

        //Result
        Assert.Multiple(() =>
        {
            Assert.That(stat.BaseValue, Is.EqualTo(startValue + value));
            Assert.That(stat.ModifiedValue, Is.EqualTo(stat.BaseValue));
        });
    }
    
    [Test]
    public void WhenCreateEffectWithMultiplyAndBase_AndApplyToStat_ThenStatHasDifferedBaseValue([Values(-10, 100, 0)] float value)
    {
        //Action
        Buff buff = new Buff(value, BuffType.Multiply, true);
        Effect effect = EffectBuilder.Start()
            .WithBuffs(buff)
            .Build();
        var startValue = stat.BaseValue = 100;

        //Condition
        stat.ApplyEffect(effect);
        stat.ActualizeEffects();

        //Result
        Assert.Multiple(() =>
        {
            Assert.That(stat.BaseValue, Is.EqualTo(startValue * value));
            Assert.That(stat.ModifiedValue, Is.EqualTo(stat.BaseValue));
        });
    }
}