public interface IGameInstance
{
    public bool TryInit(GameData data);
    public void Init();
    public void StartGame();
    public void FinishGame();
    public void ResetGame();
}