using System.Collections.Generic;
using UnityEngine;

namespace PortfolioSamples.ObjectPool
{
    public interface IPoolable
    {
        void OnRent();
        void OnReturn();
    }

    public sealed class GenericObjectPool<T> where T : Component
    {
        private readonly T _prefab;
        private readonly Transform _root;
        private readonly Stack<T> _available = new();
        private readonly HashSet<T> _rented = new();

        public int AvailableCount => _available.Count;
        public int RentedCount => _rented.Count;

        public GenericObjectPool(T prefab, Transform root, int prewarmCount = 0)
        {
            _prefab = prefab;
            _root = root;

            for (var i = 0; i < prewarmCount; i++)
            {
                var instance = Create();
                instance.gameObject.SetActive(false);
                _available.Push(instance);
            }
        }

        public T Rent(Transform parent = null)
        {
            var instance = _available.Count > 0 ? _available.Pop() : Create();

            if (parent != null)
                instance.transform.SetParent(parent, false);

            if (!_rented.Add(instance))
                throw new System.InvalidOperationException("Object is already rented.");

            instance.gameObject.SetActive(true);

            if (instance is IPoolable poolable)
                poolable.OnRent();

            return instance;
        }

        public void Return(T instance)
        {
            if (instance == null || !_rented.Remove(instance))
                return;

            if (instance is IPoolable poolable)
                poolable.OnReturn();

            instance.transform.SetParent(_root, false);
            instance.gameObject.SetActive(false);
            _available.Push(instance);
        }

        private T Create()
        {
            return Object.Instantiate(_prefab, _root);
        }
    }
}
