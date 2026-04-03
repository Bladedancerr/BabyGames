using UnityEngine;

[CreateAssetMenu(fileName = "GameInternalLibrary", menuName = "Game/Library/Internal")]
public class GameInternalLibrary : ScriptableObject
{
    [Header("internal type of the game")]
    public GameTypeInternal GameTypeInternal;
    [Header("controller for this specific game")]
    [Header("for example vocabulary/drawing")]
    public BaseGameController BaseGameController;

    [Header("data for this specific game, must be sequential")]
    [Header("will lookup using index")]
    [Header("for example data 0, data 1, data 2")]
    public GameData[] GameDatas;
}