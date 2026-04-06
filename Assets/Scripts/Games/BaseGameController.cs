using UnityEngine;

public abstract class BaseGameController<TData> : BaseGameController where TData : GameData
{
    protected TData _gameData;

    public override bool TryInit(GameData data)
    {
        if (data is TData specificData)
        {
            _gameData = specificData;
            Init();
            return true;
        }

        Debug.LogError($"incorrect data assigned {data} to {this}");
        return false;
    }
}

public abstract class BaseGameController : MonoBehaviour, IGameInstance
{
    public GameType GameType;
    public GameTypeInternal InternalGameType;

    public abstract bool TryInit(GameData data);
    public abstract void Init();
    public abstract void StartGame();
    public abstract void FinishGame();
    public abstract void ResetGame();
}
