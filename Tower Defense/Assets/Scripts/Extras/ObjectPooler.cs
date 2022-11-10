using System;
using System.Collections.Generic;
using UnityEngine;

namespace Extras
{
    public class ObjectPooler : MonoBehaviour
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private int _poolSize;

        private List<GameObject> _pool;
        private GameObject _poolContainer;

        private void Awake()
        {
            _pool = new List<GameObject>();
            _poolContainer = new GameObject($"Pool: {_prefab.name}");
            CreatePooler();
        }

        private void CreatePooler()
        {
            for (int i = 0; i < _poolSize; i++)
            {
                _pool.Add(CreateObject());
            }
        }

        private GameObject CreateObject()
        {
            var instance = Instantiate(_prefab, _poolContainer.transform);
            instance.SetActive(false);
            return instance;
        }

        public GameObject GetPoolObject()
        {
            foreach (var @object in _pool)
            {
                if (!@object.activeInHierarchy)
                {
                    return @object;
                }
            }

            return CreateObject();
        }
    }
}