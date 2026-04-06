public interface IGameOrchestrator
{
    public void StartGame(GameType gameType, GameTypeInternal gameTypeInternal, int gameIndex);
    public void ResetGame();
}