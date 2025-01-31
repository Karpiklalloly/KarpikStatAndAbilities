namespace StatSystemTest.StatTest;

public class Order
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
    public void WhenSetOrder_AndApply_ThenEffectsAreOrdered(
        [Values(1, 5, -3)] int order1,
        [Values(2, 4, -2)] int order2)
    {
        //Action
        var buff1 = new Buff(10, BuffType.Set);
        var buff2 = new Buff(-10, BuffType.Set);
        
        var effect1 = EffectBuilder.Start()
            .WithBuffs(buff1)
            .WithOrder(order1)
            .Build();
        var effect2 = EffectBuilder.Start()
            .WithBuffs(buff2)
            .WithOrder(order2)
            .Build();
        var finalValue = order1 > order2
            ? buff1.Value
            : buff2.Value;

        //Condition
        stat.ApplyEffect(effect1);
        stat.ApplyEffect(effect2);
        stat.ActualizeEffects();

        //Result
        Assert.That(stat.ModifiedValue, Is.EqualTo(finalValue));
    }
}