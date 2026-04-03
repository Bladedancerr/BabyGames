using UnityEngine;

public abstract class BaseGameController<TData, TType> : BaseGameController where TData : GameData
{
    [SerializeField]
    protected TType _internalGameType;

    [SerializeField]
    protected TData _gameData;

    public override bool TryInit(GameData data)
    {
        if (data is TData specificData)
        {
            _gameData = specificData;
            Init();
            return true;
        }

        return false;
    }
}

public abstract class BaseGameController : MonoBehaviour, IGameInstance
{
    public GameType GameType;
    public abstract bool TryInit(GameData data);
    public abstract void Init();
    public abstract void StartGame();
    public abstract void FinishGame();
}
