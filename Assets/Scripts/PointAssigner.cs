using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PointAssigner : MonoBehaviour
{
    [SerializeField]
    private LineRenderer _lineRenderer;

    [SerializeField]
    private Transform[] _points;

    [SerializeField]
    private int _pointIndex;

    [SerializeField]
    private float _distanceChecker = 0;
    [SerializeField]
    private float _resetDistanceChecker = 0;

    [SerializeField]
    private float _maxDistanceBetweenPoints = 0;

    [SerializeField]
    private List<Vector2> _smoothedPoints;

    private Vector2 _touchPos;

    private void Start()
    {
        GenerateSmoothPoints();
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
            Debug.Log(touchPos);
            CalculateDistanceFromLastPoint(touchPos);
            _touchPos = touchPos;
        }
    }

    private void GenerateSmoothPoints()
    {
        _smoothedPoints = new List<Vector2>();

        for (int i = 0; i < _points.Length - 1; i++)
        {
            var start = _points[i].position;
            var end = _points[i + 1].position;

            var distance = Vector2.Distance(start, end);

            int addedPointsAmount = Mathf.CeilToInt(distance / _maxDistanceBetweenPoints);

            for (int j = 0; j < addedPointsAmount; j++)
            {
                float t = (float)j / addedPointsAmount;
                Vector2 newPoint = Vector2.Lerp(start, end, t);
                _smoothedPoints.Add(newPoint);
            }
        }

        _smoothedPoints.Add(_points[_points.Length - 1].position);
    }

    private void CalculateDistanceFromLastPoint(Vector2 pos)
    {
        // if all points are done
        if (_pointIndex >= _smoothedPoints.Count)
        {
            Debug.Log("screen touch sheivsooo kvelaaaaaa");
            return;
        }

        float distToTarget = Vector2.Distance(_smoothedPoints[_pointIndex], pos);

        //finger too far away from last point
        if (distToTarget > _resetDistanceChecker)
        {
            Debug.Log("screen touch shorsaaaaaa dzaliaaaan");
            ResetLine();
            return;
        }

        //success
        if (distToTarget < _distanceChecker)
        {
            // Snap the line exactly to your predefined Transform position
            AddPointToLine(_smoothedPoints[_pointIndex]);
            _pointIndex++;
        }
    }

    private void AddPointToLine(Vector3 position)
    {
        _lineRenderer.positionCount++;
        _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, position);
    }

    private void ResetLine()
    {
        _pointIndex = 0;
        _lineRenderer.positionCount = 0;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(_touchPos, _distanceChecker);
        if (_smoothedPoints == null)
        {
            return;
        }

        foreach (var point in _smoothedPoints)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(point, 0.1f);
        }
    }
}
