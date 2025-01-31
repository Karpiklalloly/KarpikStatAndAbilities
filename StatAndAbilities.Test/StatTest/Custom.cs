namespace StatSystemTest.StatTest;

public class Custom
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
    
    [TestCase(
        100,
        2, BuffType.Multiply, false,
        -15, BuffType.Add, false,
        30, BuffType.Add, false,
        0, BuffType.Add, false,
        215)]
    public void WhenMultipleBuffs_AndApply_ThenMustBeCorrect1(
        float startValue,
        int value1, BuffType type1, bool changeBase1,
        int value2, BuffType type2, bool changeBase2,
        int value3, BuffType type3, bool changeBase3,
        int value4, BuffType type4, bool changeBase4,
        float finalValue)
    {
        //Action
        Buff buff1 = new Buff(value1, type1, changeBase1);
        Buff buff2 = new Buff(value2, type2, changeBase2);
        Buff buff3 = new Buff(value3, type3, changeBase3);
        Buff buff4 = new Buff(value4, type4, changeBase4);
        Effect effect = EffectBuilder.Start()
            .WithBuffs(buff1, buff2, buff3, buff4)
            .Build();
        stat.BaseValue = startValue;

        //Condition
        stat.ApplyEffect(effect);
        stat.ActualizeEffects();

        //Result
        Assert.That(stat.ModifiedValue, Is.EqualTo(finalValue));
    }
    
    [TestCase(
        100123123,
        20, BuffType.Multiply, false,
        -150, BuffType.Add, false,
        100, BuffType.Set, false,
        1, BuffType.Add, false,
        101)]
    public void WhenMultipleBuffs_AndApply_ThenMustBeCorrect2(
        float startValue,
        int value1, BuffType type1, bool changeBase1,
        int value2, BuffType type2, bool changeBase2,
        int value3, BuffType type3, bool changeBase3,
        int value4, BuffType type4, bool changeBase4,
        float finalValue)
    {
        //Action
        Buff buff1 = new Buff(value1, type1, changeBase1);
        Buff buff2 = new Buff(value2, type2, changeBase2);
        Buff buff3 = new Buff(value3, type3, changeBase3);
        Buff buff4 = new Buff(value4, type4, changeBase4);
        Effect effect = EffectBuilder.Start()
            .WithBuffs(buff1, buff2, buff3, buff4)
            .Build();
        stat.BaseValue = startValue;

        //Condition
        stat.ApplyEffect(effect);
        stat.ActualizeEffects();

        //Result
        Assert.That(stat.ModifiedValue, Is.EqualTo(finalValue));
    }
}