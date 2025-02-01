namespace Karpik.StatAndAbilities.Sample
{
    public class Entity
    {
        public int Id { get; }
    
        private readonly StatContainer _statContainer;

        public Entity(int id)
        {
            Id = id;
            _statContainer = new StatContainer(id);
        }
    
        public ref T AddStat<T>() where T : struct, IStat => ref _statContainer.Add<T>();
        public ref T GetStat<T>() where T : struct, IStat => ref _statContainer.Get<T>();
        public bool HasStat<T>() where T : struct, IStat => _statContainer.Has<T>();
    }
}