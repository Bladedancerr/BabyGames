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
    private float _distanceChecker = 100f;

    private Vector2 _touchPos;

    private void Start()
    {
        // for (int i = 0; i < _points.Length; i++)
        // {
        //     _lineRenderer.SetPosition(i, _points[i].position);
        // }
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

    private void CalculateDistanceFromLastPoint(Vector2 pos)
    {
        if (_pointIndex >= _points.Length)
        {
            Debug.Log("screen touch sheivsooo kvelaaaaaa");
            return;
        }

        if (Vector2.Distance(_points[_pointIndex].position, pos) < _distanceChecker)
        {
            Debug.Log("screen touch axlosaaaaaaaaaaa");
            _lineRenderer.positionCount = _pointIndex + 1;
            _lineRenderer.SetPosition(_pointIndex, _points[_pointIndex].position);
            _pointIndex++;
        }
        else if (Vector2.Distance(_points[_pointIndex].position, pos) > 1f)
        {
            Debug.Log("screen touch shorsaaaaaa dzaliaaaan");
            _pointIndex = 0;
            _lineRenderer.positionCount = 0;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(_touchPos, _distanceChecker);
        foreach (var point in _points)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(point.position, 0.1f);
        }
    }
}
