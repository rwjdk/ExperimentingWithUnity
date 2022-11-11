using UnityEngine;

namespace WayPointSystem
{
    public class WayPoint : MonoBehaviour
    {
        [SerializeField] private Vector3[] _points;

        private bool _gameStarted;

        public Vector3[] Points => _points;
        public Vector3 CurrentPosition { get; private set; }

        public Vector3 GetWayPointPosition(int index)
        {
            if (index >= Points.Length)
            {
                return CurrentPosition;
            }

            return CurrentPosition + Points[index];
        }

        private void Start()
        {
            _gameStarted = true;
            CurrentPosition = transform.position;
        }

        //Editor Only Code
        private void OnDrawGizmos()
        {
            if (!_gameStarted && transform.hasChanged)
            {
                CurrentPosition = transform.position;
            }

            for (var index = 0; index < _points.Length; index++)
            {
                var currentPoint = _points[index] + CurrentPosition;

                //Draw Gizmo Point
                Gizmos.color = Color.green;
                Gizmos.DrawWireSphere(currentPoint, 0.5f);

                var lastIndex = _points.Length - 1;
                if (index != lastIndex)
                {
                    //Draw Gizmo Lines between points
                    Gizmos.color = Color.gray;
                    var nextPoint = _points[index + 1] + CurrentPosition;
                    Gizmos.DrawLine(currentPoint, nextPoint);
                }
            }
        }
    }
}