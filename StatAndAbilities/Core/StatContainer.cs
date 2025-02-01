using System;

namespace Karpik.StatAndAbilities
{
    public class StatContainer
    {
        private readonly int _id;

        public StatContainer(int entityId)
        {
            _id = entityId;
        }

        public ref T Add<T>() where T : struct, IStat
        {
            if (Has<T>())
                throw new InvalidOperationException($"Stat of type {typeof(T).Name} already exists.");
            return ref StatPool<T>.Instance.Add(_id);
        }
    
        public ref T Get<T>() where T : struct, IStat
        {
            if (Has<T>())
            {
                return ref StatPool<T>.Instance.Get(_id);
            }

            throw new InvalidOperationException($"Stat of type {typeof(T).Name} is not found.");
        }
    
        public ref T GetUnsafe<T>() where T : struct, IStat => ref StatPool<T>.Instance.Get(_id);
    
        public bool Has<T>() where T : struct, IStat => StatPool<T>.Instance.Has(_id);
        public bool Has<T1, T2>() where T1 : struct, IStat where T2 : struct, IStat
            => StatPool<T1>.Instance.Has(_id)
               && StatPool<T2>.Instance.Has(_id);
        public bool Has<T1, T2, T3>() where T1 : struct, IStat where T2 : struct, IStat where T3 : struct, IStat
            => StatPool<T1>.Instance.Has(_id)
               && StatPool<T2>.Instance.Has(_id)
               && StatPool<T3>.Instance.Has(_id);
    }
}