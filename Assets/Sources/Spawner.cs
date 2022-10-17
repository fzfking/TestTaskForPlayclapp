using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Sources.Interfaces;
using TMPro;
using UnityEngine;

namespace Sources
{
    public class Spawner
    {
        private float _speed;
        private float _interval;
        private float _distance;
        private bool _isEnabled = false;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly CubesPool _cubesPool;
        private readonly Coroutine _spawnerCoroutine;

        public Spawner(UserSettings settings, ICoroutineRunner coroutineRunner, CubeModel cubePrefab, Transform spawnPoint)
        {
            settings.SpeedText.onValueChanged.AddListener(t =>
            {
                float.TryParse(t, out var value);
                if (CheckInputForNegative(settings.SpeedText, value)) return;
                _speed = value;
            });
            settings.DistanceText.onValueChanged.AddListener(t =>
            {
                float.TryParse(t, out var value);
                if (CheckInputForNegative(settings.DistanceText, value)) return;
                _distance = value;
            });
            settings.SpawnIntervalText.onValueChanged.AddListener(t =>
            {
                float.TryParse(t, out var value);
                if (CheckInputForNegative(settings.SpawnIntervalText, value)) return;
                _interval = value;
            });
            settings.SpawnToggleButton.onClick.AddListener(() =>
            {
                
                _isEnabled = !_isEnabled;
                if (_isEnabled)
                {
                    settings.SpawnToggleButton.image.color = Color.green;
                    settings.SpawnToggleButton.GetComponentInChildren<TextMeshProUGUI>().text = "Enabled";
                }
                else
                {
                    settings.SpawnToggleButton.image.color = Color.red;
                    settings.SpawnToggleButton.GetComponentInChildren<TextMeshProUGUI>().text = "Disabled";
                }
            });
            _coroutineRunner = coroutineRunner;

            _cubesPool = new CubesPool(cubePrefab, spawnPoint);
            _spawnerCoroutine = coroutineRunner.StartCoroutine(SpawnNewCubes());
        }

        public void Destroy()
        {
            _coroutineRunner.StopCoroutine(_spawnerCoroutine);
        }

        private static bool CheckInputForNegative(TMP_InputField field, float value)
        {
            if (!(value < 0)) return false;
            field.text = (-value).ToString(CultureInfo.InvariantCulture);
            return true;
        }
        
        private IEnumerator SpawnNewCubes()
        {
            while (true)
            {
                if (_isEnabled)
                {
                    var cube = _cubesPool.GetFree();
                    cube.gameObject.SetActive(true);
                    if (_speed == 0f)
                    {
                        _speed = 0.1f;
                    }
                    cube.StartMove(_distance, _speed);
                    yield return new WaitForSeconds(_interval);
                }

                yield return null;
            }
        }
    }
}