namespace StatSystemTest.RangeStatTest;

public class SingleBuff
{
    private DefaultRangeStat stat;

    [OneTimeSetUp]
    public void Awake()
    {
        StatPool<DefaultStat>.Instance.ClearAll();
        stat = StatPool<DefaultRangeStat>.Instance.Add(1);
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
        stat.MinStat.BaseValue = 1;
        stat.MaxStat.BaseValue = 1;
        stat.ValueStat.BaseValue = 1;

        //Condition
        stat.ApplyEffect(effect, BuffRange.All);
        stat.ActualizeEffects();

        //Result
        Assert.Multiple(() =>
        {
            Assert.That(stat.MinStat.ModifiedValue, Is.EqualTo(value));
            Assert.That(stat.MaxStat.ModifiedValue, Is.EqualTo(value));
            Assert.That(stat.ValueStat.ModifiedValue, Is.EqualTo(value));
        });
    }
    
    [Test]
    public void WhenCreateEffectWithSet_AndApplyToStat_ThenStatHasThisValue(
        [Values(-10, 100, 0)] float value,
        [Values(BuffRange.Min, BuffRange.Max, BuffRange.Value)] BuffRange buffRange)
    {
        //Action
        Buff buff = new Buff(value, BuffType.Set);
        Effect effect = EffectBuilder.Start()
            .WithBuffs(buff)
            .Build();
        stat.MinStat.BaseValue = 1;
        stat.MaxStat.BaseValue = 1;
        stat.ValueStat.BaseValue = 1;

        //Condition
        stat.ApplyEffect(effect, buffRange);
        stat.ActualizeEffects();

        //Result
        Assert.Multiple(() =>
        {
            if (buffRange.Flagged(BuffRange.Min))
            {
                Assert.That(stat.MinStat.ModifiedValue, Is.EqualTo(value));
                Assert.That(stat.MaxStat.ModifiedValue, Is.EqualTo(stat.MaxStat.BaseValue));
                Assert.That(stat.ValueStat.ModifiedValue, Is.EqualTo(stat.ValueStat.BaseValue));
            }

            if (buffRange.Flagged(BuffRange.Max))
            {
                Assert.That(stat.MinStat.ModifiedValue, Is.EqualTo(stat.MinStat.BaseValue));
                Assert.That(stat.MaxStat.ModifiedValue, Is.EqualTo(value));
                Assert.That(stat.ValueStat.ModifiedValue, Is.EqualTo(stat.ValueStat.BaseValue));
                
            }

            if (buffRange.Flagged(BuffRange.Value))
            {
                Assert.That(stat.MinStat.ModifiedValue, Is.EqualTo(stat.MinStat.BaseValue));
                Assert.That(stat.MaxStat.ModifiedValue, Is.EqualTo(stat.MaxStat.BaseValue));
                Assert.That(stat.ValueStat.ModifiedValue, Is.EqualTo(value));
            }
        });
    }
    
    [Test]
    public void WhenCreateEffectWithAdd_AndApplyToStat_ThenStatHasThisValue(
        [Values(-10, 100, 0)] float value,
        [Values(BuffRange.Min, BuffRange.Max, BuffRange.Value)] BuffRange buffRange)
    {
        //Action
        Buff buff = new Buff(value, BuffType.Add);
        Effect effect = EffectBuilder.Start()
            .WithBuffs(buff)
            .Build();
        stat.MinStat.BaseValue = 1;
        stat.MaxStat.BaseValue = 1;
        stat.ValueStat.BaseValue = 1;

        //Condition
        stat.ApplyEffect(effect, buffRange);
        stat.ActualizeEffects();

        //Result
        Assert.Multiple(() =>
        {
            if (buffRange.Flagged(BuffRange.Min))
            {
                Assert.That(stat.MinStat.ModifiedValue, Is.EqualTo(stat.MaxStat.BaseValue + value));
                Assert.That(stat.MaxStat.ModifiedValue, Is.EqualTo(stat.MaxStat.BaseValue));
                Assert.That(stat.ValueStat.ModifiedValue, Is.EqualTo(stat.ValueStat.BaseValue));
            }

            if (buffRange.Flagged(BuffRange.Max))
            {
                Assert.That(stat.MinStat.ModifiedValue, Is.EqualTo(stat.MinStat.BaseValue));
                Assert.That(stat.MaxStat.ModifiedValue, Is.EqualTo(stat.MaxStat.BaseValue + value));
                Assert.That(stat.ValueStat.ModifiedValue, Is.EqualTo(stat.ValueStat.BaseValue));
                
            }

            if (buffRange.Flagged(BuffRange.Value))
            {
                Assert.That(stat.MinStat.ModifiedValue, Is.EqualTo(stat.MinStat.BaseValue));
                Assert.That(stat.MaxStat.ModifiedValue, Is.EqualTo(stat.MaxStat.BaseValue));
                Assert.That(stat.ValueStat.ModifiedValue, Is.EqualTo(stat.MaxStat.BaseValue + value));
            }
        });
    }
    
    [Test]
    public void WhenCreateEffectWithMultiply_AndApplyToStat_ThenStatHasThisValue(
        [Values(-10, 100, 0)] float value,
        [Values(BuffRange.Min, BuffRange.Max, BuffRange.Value)] BuffRange buffRange)
    {
        //Action
        Buff buff = new Buff(value, BuffType.Multiply);
        Effect effect = EffectBuilder.Start()
            .WithBuffs(buff)
            .Build();
        stat.MinStat.BaseValue = 1;
        stat.MaxStat.BaseValue = 1;
        stat.ValueStat.BaseValue = 1;

        //Condition
        stat.ApplyEffect(effect, buffRange);
        stat.ActualizeEffects();

        //Result
        Assert.Multiple(() =>
        {
            if (buffRange.Flagged(BuffRange.Min))
            {
                Assert.That(stat.MinStat.ModifiedValue, Is.EqualTo(stat.MaxStat.BaseValue * value));
                Assert.That(stat.MaxStat.ModifiedValue, Is.EqualTo(stat.MaxStat.BaseValue));
                Assert.That(stat.ValueStat.ModifiedValue, Is.EqualTo(stat.ValueStat.BaseValue));
            }

            if (buffRange.Flagged(BuffRange.Max))
            {
                Assert.That(stat.MinStat.ModifiedValue, Is.EqualTo(stat.MinStat.BaseValue));
                Assert.That(stat.MaxStat.ModifiedValue, Is.EqualTo(stat.MaxStat.BaseValue * value));
                Assert.That(stat.ValueStat.ModifiedValue, Is.EqualTo(stat.ValueStat.BaseValue));
                
            }

            if (buffRange.Flagged(BuffRange.Value))
            {
                Assert.That(stat.MinStat.ModifiedValue, Is.EqualTo(stat.MinStat.BaseValue));
                Assert.That(stat.MaxStat.ModifiedValue, Is.EqualTo(stat.MaxStat.BaseValue));
                Assert.That(stat.ValueStat.ModifiedValue, Is.EqualTo(stat.MaxStat.BaseValue * value));
            }
        });
    }
}