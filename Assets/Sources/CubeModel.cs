using System;
using System.Collections;
using UnityEngine;

namespace Sources
{
    public class CubeModel : MonoBehaviour
    {
        private Vector3 _startPosition;
        private float _disappearDistance;
        private float _speed;
        private Coroutine _currentMovement;

        public void StartMove(float disappearDistance, float speed)
        {
            _disappearDistance = disappearDistance;
            _speed = speed;
            _currentMovement = StartCoroutine(MovingRoutine());
        }

        private void OnEnable()
        {
            _startPosition = transform.position;
        }

        private IEnumerator MovingRoutine()
        {
            while (transform.position.x - _startPosition.x < _disappearDistance)
            {
                var newPosition = transform.position;
                newPosition.x += _speed*Time.deltaTime;
                transform.position = newPosition;
                yield return null;
            }
            gameObject.SetActive(false);
        }
    }
}
