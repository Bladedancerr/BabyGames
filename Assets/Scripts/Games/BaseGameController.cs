using UnityEngine;

public abstract class BaseGameController<T> : MonoBehaviour, IGameInstance where T : GameData
{
    [SerializeField]
    private T _gameData;

    public abstract void Init();
    public abstract void StartGame();
    public abstract void FinishGame();
}
