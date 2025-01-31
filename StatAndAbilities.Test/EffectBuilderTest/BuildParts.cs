namespace StatSystemTest.EffectBuilderTest;

public class BuildParts
{
    [Test]
    public void WhenSetBuffs_AndBuild_ThenBuffsInTheSameOrder()
    {
        //Action
        var buffs = new[]
        {
            new Buff(10, BuffType.Add),
            new Buff(20, BuffType.Multiply),
            new Buff(30, BuffType.Set),
            new Buff(40, BuffType.Add, true),
            new Buff(50, BuffType.Multiply, true),
            new Buff(60, BuffType.Set, true)
        };
        
        var part = EffectBuilder.Start()
            .WithBuffs(buffs.ToArray());

        //Condition
        var effect = part.Build();

        //Result
        Assert.That(effect.Buffs, Is.EqualTo(buffs));
    }

    [Test]
    public void WhenSetName_AndBuild_ThenNameIsTheSame([Values("", "name", "Test MeThOd")] string name)
    {
        //Action
        var part = EffectBuilder.Start()
            .WithName(name);
        
        //Condition
        var effect = part.Build();

        //Result
        Assert.That(effect.Name, Is.EqualTo(name));
    }

    [Test]
    public void WhenSetOrder_AndBuild_ThenOrderIsTheSame([Values(-1000, -2, -1, 0, 1, 2, 1000)] int order)
    {
        //Action
        var part = EffectBuilder.Start()
            .WithOrder(order);

        //Condition
        var effect = part.Build();

        //Result
        Assert.That(effect.Order, Is.EqualTo(order));
    }

    [Test]
    public void WhenSetDuration_AndBuild_ThenDurationIsTheSame([Values(-1000, -2, -1, 1, 2, 1000)] float duration)
    {
        //Action
        var part = EffectBuilder.Start()
            .WithDuration(duration);

        //Condition
        var effect = part.Build();

        //Result
        Assert.That(effect.Duration, Is.EqualTo(duration));
    }

    [Test]
    public void WhenSetZeroDuration_AndBuild_ThenDurationIsMinusOne()
    {
        //Action
        float duration = 0;
        var part = EffectBuilder.Start()
            .WithDuration(duration);

        //Condition
        var effect = part.Build();

        //Result
        Assert.That(effect.Duration, Is.EqualTo(-1));
    }
    
    [Test]
    public void WhenSetZeroDuration_AndBuild_ThenIsPermanent()
    {
        //Action
        float duration = -1;
        var part = EffectBuilder.Start()
            .WithDuration(duration);

        //Condition
        var effect = part.Build();

        //Result
        Assert.That(effect.IsPermanent, Is.True);
    }

    [Test]
    public void WhenSetAllParts_AndBuild_ThenPartsAreTheSame(
        [Values(1, 2, 3, 4)] int buffsCount,
        [Values("", "name", "Test MeThOd")] string name,
        [Values(-1000, -2, -1, 0, 1, 2, 1000)] int order,
        [Values(-1000, -2, -1, 1, 2, 1000)] float duration)
    {
        //Action
        List<Buff> buffs = [];
        for (int i = 0; i < buffsCount; i++)
        {
            buffs.Add(new Buff(i, (BuffType)(i % 3), i % 2 == 0));
        }
        var part = EffectBuilder.Start()
            .WithBuffs(buffs.ToArray())
            .WithName(name)
            .WithOrder(order)
            .WithDuration(duration);
        
        //Condition
        var effect = part.Build();

        //Result
        Assert.Multiple(() =>
        {
            Assert.That(effect.Buffs, Is.EqualTo(buffs));
            Assert.That(effect.Name, Is.EqualTo(name));
            Assert.That(effect.Order, Is.EqualTo(order));
            Assert.That(effect.Duration, Is.EqualTo(duration));
        });
    }
}