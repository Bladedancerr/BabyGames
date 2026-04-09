using System;
using UnityEngine;

public abstract class BaseGameController<TData> : BaseGameController where TData : GameData
{
    protected TData _gameData;

    protected void Update()
    {
        if (_inputProvider == null)
        {
            return;
        }

        _inputProvider.Tick();
    }

    public override bool TryInit(GameData data)
    {
        if (data is TData specificData)
        {
            _gameData = specificData;
            Init();
            return true;
        }

        Debug.LogError($"incorrect data assigned {data.GetType()} to {this.GetType()}");
        return false;
    }

    public override void Init()
    {

        _spawnedGame = Instantiate(_gameData.GamePrefab);

#if UNITY_EDITOR
        _inputProvider = new MouseInputProvider(Camera.main);
        Debug.Log($"unityeditor: input provider set to {_inputProvider.GetType()}");
#else
        _inputProvider = new TouchInputProvider(Camera.main);
#endif

        AttachInputProvider();
    }

    public override void ResetGame()
    {
        if (_spawnedGame != null)
        {
            Destroy(_spawnedGame);
        }

        _spawnedGame = null;

        ReleaseInputProvider();
    }

    public override void FinishGame()
    {
        NotifyGameFinished();
    }

    private void AttachInputProvider()
    {
        if (_inputProvider == null)
        {
            return;
        }

        _inputProvider.OnPointerDown += HandlePointerDown;
        _inputProvider.OnPointerUp += HandlePointerUp;
        _inputProvider.OnPointerMove += HandlePointerMove;
    }

    private void ReleaseInputProvider()
    {
        if (_inputProvider == null)
        {
            return;
        }

        _inputProvider.OnPointerDown -= HandlePointerDown;
        _inputProvider.OnPointerUp -= HandlePointerUp;
        _inputProvider.OnPointerMove -= HandlePointerMove;
    }

    public override void HandlePointerDown(Vector2 touchWorldPos)
    {
    }

    public override void HandlePointerMove(Vector2 touchWorldPos)
    {
    }

    public override void HandlePointerUp(Vector2 touchWorldPos)
    {
    }
}

public abstract class BaseGameController : MonoBehaviour, IGameInstance
{
    protected IPointerInputProvider _inputProvider;
    protected GameObject _spawnedGame;
    public event Action OnGameFinished;

    public abstract bool TryInit(GameData data);
    public abstract void Init();
    public abstract void StartGame();
    public abstract void FinishGame();
    public abstract void ResetGame();

    // input handlers
    public abstract void HandlePointerDown(Vector2 touchWorldPos);
    public abstract void HandlePointerMove(Vector2 touchWorldPos);
    public abstract void HandlePointerUp(Vector2 touchWorldPos);

    protected void NotifyGameFinished()
    {
        OnGameFinished?.Invoke();
    }
}
