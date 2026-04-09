using UnityEngine;

[CreateAssetMenu(fileName = "GameLibrary", menuName = "Game/Library/Generall")]
public class GameLibrary : ScriptableObject
{
    [Header("main type of the game")]
    public GameType MainType;
    [Header("sequence of games, for example")]
    [Header("for vocabulary: ")]
    [Header("drawing->finding->findingall->soundbased")]
    public GamesSequence GamesSequence;

    [Header("internal library per game type")]
    [Header("for example vocabulary/drawing")]
    public GameInternalLibrary[] GameInternalLibraries;
}
