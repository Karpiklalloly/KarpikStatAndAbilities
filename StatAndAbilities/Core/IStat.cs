namespace Karpik.StatAndAbilities
{
    public interface IStat
    {
        public void Init();
        public void DeInit();
    }

    public interface IRangeStat : IStat { }
}