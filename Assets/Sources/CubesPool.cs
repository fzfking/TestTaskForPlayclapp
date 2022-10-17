using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Sources
{
    public class CubesPool
    {
        private readonly CubeModel _cubePrefab;
        private readonly Transform _spawnPoint;
        private readonly List<CubeModel> _container;
        private const int DefaultPoolSize = 10;

        public CubesPool(CubeModel cubePrefab, Transform spawnPoint)
        {
            _cubePrefab = cubePrefab;
            _spawnPoint = spawnPoint;
            _container = new List<CubeModel>(DefaultPoolSize); 
            ExpandCubesPool();
        }

        private void ExpandCubesPool()
        {
            for (int i = 0; i < DefaultPoolSize; i++)
            {
                var cube = GameObject.Instantiate(_cubePrefab, _spawnPoint);
                cube.gameObject.SetActive(false);
                _container.Add(cube);
            }
        }
        
        public CubeModel GetFree()
        {
            while (true)
            {
                var cube = _container.FirstOrDefault(c => c.gameObject.activeSelf == false);
                if (cube == null)
                {
                    ExpandCubesPool();
                    continue;
                }

                cube.transform.position = _spawnPoint.position;
                return cube;
            }
        }
    }
}