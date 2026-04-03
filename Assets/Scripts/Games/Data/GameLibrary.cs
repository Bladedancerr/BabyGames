using UnityEngine;

[CreateAssetMenu(fileName = "GameLibrary", menuName = "Game/Library/General")]
public class GameLibrary : ScriptableObject
{
    [Header("main type of the game")]
    public GameType MainType;

    [Header("internal library per game type")]
    [Header("for example vocabulary/drawing")]
    public GameInternalLibrary[] GameInternalLibraries;
}
