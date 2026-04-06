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
    private IPointerInputProvider _inputProvider;
    private IPathGenerator<Vector2> _pathGenerator;

    private float _distanceCheckerSqr;
    private float _resetDistanceCheckerSqr;

    private GameObject _spawnedGame;

    private void Update()
    {
        if (_inputProvider == null)
        {
            return;
        }

        _inputProvider.Tick();
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

    private void ResetProgress()
    {
        _pointIndex = 0;
        _lineDrawer.ResetLine();
    }

    private void Progress(Vector2 pos)
    {
        _lineDrawer.AddPointToLine(pos);
        _pointIndex++;
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

    public override void Init()
    {
        _lineDrawer = new LineRendererDrawer(GetComponent<LineRenderer>());

#if UNITY_EDITOR
        _inputProvider = new MouseInputProvider(Camera.main);
#else
        _inputProvider = new TouchInputProvider(Camera.main);
#endif

        _inputProvider.OnPointerMove += HandlePointerMove;

        _pathGenerator = new CatmullRomPathGenerator(_maxDistanceBetweenPoints);

        _distanceCheckerSqr = _distanceChecker * _distanceChecker;
        _resetDistanceCheckerSqr = _resetDistanceChecker * _resetDistanceChecker;

        _smoothedPoints = _pathGenerator.GeneratePath(_gameData.PathData.Waypoints);

        _spawnedGame = Instantiate(_gameData.GamePrefab);
    }

    public override void StartGame()
    {
        Debug.Log($"{this.GetType()} game started");
    }

    public override void FinishGame()
    {
    }

    public override void ResetGame()
    {
        if (_spawnedGame != null)
        {
            Destroy(_spawnedGame);
        }
        _spawnedGame = null;
    }

    private void HandlePointerMove(Vector2 position)
    {
        CheckProgress(position);
        _touchPos = position;
    }

    private void OnDestroy()
    {
        if (_inputProvider != null)
        {
            _inputProvider.OnPointerMove -= HandlePointerMove;
        }
    }
}
