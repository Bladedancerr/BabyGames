using System.Collections.Generic;
using UnityEngine;

public class DrawingGameController : BaseGameController<DrawingVocabularyGameData>
{
    [SerializeField]
    private float _distanceChecker = 0;

    [SerializeField]
    private float _resetDistanceChecker = 0;

    [SerializeField]
    private float _maxDistanceBetweenPoints = 0;

    private Vector2 _touchPos;
    private int _pointIndex;
    [SerializeField]
    private List<Vector2> _smoothedPoints;
    private ILineDrawer _lineDrawer;
    private IPathGenerator<Vector2> _pathGenerator;

    private float _distanceCheckerSqr;
    private float _resetDistanceCheckerSqr;

    public override void Init()
    {
        base.Init();

        _lineDrawer = new LineRendererDrawer(GetComponent<LineRenderer>());

        _pathGenerator = new CatmullRomPathGenerator(_maxDistanceBetweenPoints);

        _distanceCheckerSqr = _distanceChecker * _distanceChecker;
        _resetDistanceCheckerSqr = _resetDistanceChecker * _resetDistanceChecker;

        _smoothedPoints = _pathGenerator.GeneratePath(_gameData.PathData.Waypoints);
    }

    public override void StartGame()
    {
    }

    public override void FinishGame()
    {
        base.FinishGame();
    }

    public override void ResetGame()
    {
        base.ResetGame();
    }

    private void CheckProgress(Vector2 pos)
    {
        // if all points are done
        if (_pointIndex >= _smoothedPoints.Count)
        {
            return;
        }

        float distToTarget = (_smoothedPoints[_pointIndex] - pos).sqrMagnitude;

        //too far, reset
        if (distToTarget > _resetDistanceCheckerSqr)
        {
            Debug.Log("meeeeh called");
            ResetProgress();
            return;
        }

        //success
        if (distToTarget < _distanceCheckerSqr)
        {
            Debug.Log("success called");
            Progress(_smoothedPoints[_pointIndex]);
        }
    }

    private void Progress(Vector2 pos)
    {
        _lineDrawer.AddPointToLine(pos);
        _pointIndex++;
        if (_pointIndex >= _smoothedPoints.Count)
        {
            FinishGame();
        }
    }

    private void ResetProgress()
    {
        _pointIndex = 0;
        _lineDrawer.ResetLine();
    }

    public override void HandlePointerMove(Vector2 position)
    {
        CheckProgress(position);
        _touchPos = position;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(_touchPos, _distanceChecker);

        if (_gameData == null)
        {
            return;
        }

        if (_gameData.PathData == null)
        {
            return;
        }

        Gizmos.color = Color.yellow;
        foreach (var point in _gameData.PathData.Waypoints)
        {
            Gizmos.DrawWireSphere(point, 0.2f);
        }

        if (_smoothedPoints == null)
        {
            return;
        }
        Gizmos.color = Color.purple;
        foreach (var point in _smoothedPoints)
        {
            Gizmos.DrawWireSphere(point, 0.1f);
        }
    }
}
