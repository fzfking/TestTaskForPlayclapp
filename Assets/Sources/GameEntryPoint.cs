using System;
using Sources.Interfaces;
using UnityEngine;

namespace Sources
{
    public class GameEntryPoint: MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private GameObject SpawnPoint;
        [SerializeField] private UserSettings Settings;
        [SerializeField] private CubeModel CubeModelPrefab;

        private Spawner _spawner;
        private void Start()
        {
            _spawner = new Spawner(Settings, this, CubeModelPrefab, SpawnPoint.transform);
        }

        private void OnDestroy()
        {
            _spawner.Destroy();
        }
    }
}